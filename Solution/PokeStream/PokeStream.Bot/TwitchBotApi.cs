using System.Net;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using System.Security.Cryptography;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;
using Microsoft.AspNetCore.DataProtection;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using PokeStream.EntityData.Data;
using PokeStream.EntityData.Models;

namespace PokeStream.Bot;

/// <summary>Calls Twitch as the bot account and keeps its token alive. Singleton: tokens are refreshed one at a time.</summary>
public class TwitchBotApi
{
    private const string Platform = "Twitch";

    private readonly IHttpClientFactory _httpClients;
    private readonly IServiceScopeFactory _scopes;
    private readonly IDataProtector _protector;
    private readonly TwitchBotSettings _settings;
    private readonly SemaphoreSlim _tokenLock = new(1, 1);

    private string? _accessToken;
    private DateTime _expiresAt;
    private string? _latestRefreshToken; // survives a failed database write

    public TwitchBotApi(IHttpClientFactory httpClients, IServiceScopeFactory scopes,
        IDataProtectionProvider dataProtection, IOptions<TwitchBotSettings> settings)
    {
        _httpClients = httpClients;
        _scopes = scopes;
        _protector = dataProtection.CreateProtector("Twitch.BotRefreshToken");
        _settings = settings.Value;
    }

    /// <summary>Twitch id of the bot account; known after the first token request.</summary>
    public string BotUserId { get; private set; } = string.Empty;

    private record TokenResponse(
        [property: JsonPropertyName("access_token")] string AccessToken,
        [property: JsonPropertyName("refresh_token")] string RefreshToken,
        [property: JsonPropertyName("expires_in")] int ExpiresIn);

    private record ValidateResponse([property: JsonPropertyName("user_id")] string UserId);

    public async Task<string> GetAccessTokenAsync(CancellationToken cancellationToken = default)
    {
        await _tokenLock.WaitAsync(cancellationToken);
        try
        {
            if (_accessToken is null || DateTime.UtcNow >= _expiresAt)
                await RefreshAsync(cancellationToken);
            return _accessToken!;
        }
        finally { _tokenLock.Release(); }
    }

    /// <summary>Subscribes the websocket session to a channel's chat.</summary>
    public Task<HttpResponseMessage> SubscribeToChatAsync(string broadcasterId, string sessionId, CancellationToken cancellationToken = default)
        => PostAsync("eventsub/subscriptions", () => new
        {
            type = "channel.chat.message",
            version = "1",
            condition = new { broadcaster_user_id = broadcasterId, user_id = BotUserId },
            transport = new { method = "websocket", session_id = sessionId }
        }, cancellationToken);

    public Task<HttpResponseMessage> SendMessageAsync(string broadcasterId, string message, string? replyToId, CancellationToken cancellationToken = default)
        => PostAsync("chat/messages", () => new
        {
            broadcaster_id = broadcasterId,
            sender_id = BotUserId,
            message,
            reply_parent_message_id = replyToId
        }, cancellationToken);

    private record StreamsResponse([property: JsonPropertyName("data")] LiveStream[] Data);
    private record LiveStream([property: JsonPropertyName("user_id")] string UserId);

    /// <summary>Which of these channels are streaming right now.</summary>
    public async Task<HashSet<string>> GetLiveChannelsAsync(IReadOnlyList<string> broadcasterIds, CancellationToken cancellationToken = default)
    {
        var live = new HashSet<string>();
        foreach (var batch in broadcasterIds.Chunk(100)) // Helix maximum per request
        {
            var query = "streams?first=100&" + string.Join('&', batch.Select(id => "user_id=" + Uri.EscapeDataString(id)));
            using var response = await SendAsync(HttpMethod.Get, query, null, cancellationToken);
            response.EnsureSuccessStatusCode();
            var streams = await response.Content.ReadFromJsonAsync<StreamsResponse>(cancellationToken);
            live.UnionWith(streams!.Data.Select(s => s.UserId));
        }
        return live;
    }

    private Task<HttpResponseMessage> PostAsync(string path, Func<object> body, CancellationToken cancellationToken)
        => SendAsync(HttpMethod.Post, path, body, cancellationToken);

    private static readonly JsonSerializerOptions SkipNulls = new() { DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull };

    private async Task<HttpResponseMessage> SendAsync(HttpMethod method, string path, Func<object>? body, CancellationToken cancellationToken)
    {
        for (var attempt = 1; ; attempt++)
        {
            var token = await GetAccessTokenAsync(cancellationToken);
            using var request = new HttpRequestMessage(method, "https://api.twitch.tv/helix/" + path);
            request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", token);
            request.Headers.Add("Client-Id", _settings.ClientId);
            if (body is not null) // built after the token request: BotUserId comes from it
                request.Content = JsonContent.Create(body(), options: SkipNulls);

            var response = await _httpClients.CreateClient().SendAsync(request, cancellationToken);
            if (response.StatusCode != HttpStatusCode.Unauthorized || attempt > 1)
                return response;

            // Revoked before its expiry: drop it (unless another caller already did) and try once more
            response.Dispose();
            if (_accessToken == token)
                _accessToken = null;
        }
    }

    private async Task RefreshAsync(CancellationToken cancellationToken)
    {
        using var scope = _scopes.CreateScope();
        var db = scope.ServiceProvider.GetRequiredService<PokeStreamDbContext>();
        var credential = await db.BotCredentials.FindAsync([Platform], cancellationToken);

        var seedHash = Convert.ToBase64String(SHA256.HashData(Encoding.UTF8.GetBytes(_settings.RefreshToken)));
        var refreshToken = _latestRefreshToken
            ?? (credential?.SeedHash == seedHash ? Unprotect(credential.RefreshToken) : null)
            ?? _settings.RefreshToken;

        var http = _httpClients.CreateClient();
        using var response = await http.PostAsync("https://id.twitch.tv/oauth2/token", new FormUrlEncodedContent(new Dictionary<string, string>
        {
            ["grant_type"] = "refresh_token",
            ["refresh_token"] = refreshToken,
            ["client_id"] = _settings.ClientId,
            ["client_secret"] = _settings.ClientSecret
        }), cancellationToken);
        if (!response.IsSuccessStatusCode)
        {
            _latestRefreshToken = null;
            throw new InvalidOperationException(
                $"Twitch rejected the bot refresh token ({(int)response.StatusCode}). Check Twitch:ClientId/ClientSecret or authorize the bot again and update Bot:RefreshToken.");
        }
        var token = (await response.Content.ReadFromJsonAsync<TokenResponse>(cancellationToken))!;
        _latestRefreshToken = token.RefreshToken;

        if (credential is null)
            db.BotCredentials.Add(credential = new BotCredential { Platform = Platform });
        credential.RefreshToken = _protector.Protect(token.RefreshToken);
        credential.SeedHash = seedHash;
        credential.UpdatedAt = DateTime.UtcNow;
        await db.SaveChangesAsync(cancellationToken);

        using var validate = new HttpRequestMessage(HttpMethod.Get, "https://id.twitch.tv/oauth2/validate");
        validate.Headers.Authorization = new AuthenticationHeaderValue("OAuth", token.AccessToken);
        using var validated = await http.SendAsync(validate, cancellationToken);
        validated.EnsureSuccessStatusCode();
        BotUserId = (await validated.Content.ReadFromJsonAsync<ValidateResponse>(cancellationToken))!.UserId;

        _accessToken = token.AccessToken;
        _expiresAt = DateTime.UtcNow.AddSeconds(token.ExpiresIn).AddMinutes(-5);
    }

    private string? Unprotect(string value)
    {
        try { return _protector.Unprotect(value); }
        catch (CryptographicException) { return null; } // encryption keys changed: fall back to the configured token
    }
}
