using System.Net.Http.Headers;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text.Json.Serialization;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.DataProtection;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.WebUtilities;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using PokeStream.Core.DTOs.Common;
using PokeStream.Core.DTOs.Twitch;
using PokeStream.Core.Settings;
using PokeStream.EntityData.Data;
using PokeStream.Resources.Creator;

namespace PokeStream.API.Controllers;

/// <summary>
/// Vincula la cuenta de Twitch de un streamer (OAuth Authorization Code). Solo guarda la identidad, no tokens.
/// </summary>
[ApiController]
[Route("api/v1/twitch")]
[Authorize(Roles = "Streamer")]
[Produces("application/json")]
public class TwitchController : ControllerBase
{
    private static readonly TimeSpan StateLifetime = TimeSpan.FromMinutes(10);

    private readonly PokeStreamDbContext _dbContext;
    private readonly IHttpClientFactory _httpClients;
    private readonly ITimeLimitedDataProtector _state;
    private readonly TwitchSettings _settings;
    private readonly ILogger<TwitchController> _logger;

    public TwitchController(
        PokeStreamDbContext dbContext,
        IHttpClientFactory httpClients,
        IDataProtectionProvider dataProtection,
        IOptions<TwitchSettings> settings,
        ILogger<TwitchController> logger)
    {
        _dbContext = dbContext;
        _httpClients = httpClients;
        _state = dataProtection.CreateProtector("Twitch.OAuthState").ToTimeLimitedDataProtector();
        _settings = settings.Value;
        _logger = logger;
    }

    private record TokenResponse([property: JsonPropertyName("access_token")] string AccessToken);
    private record UsersResponse([property: JsonPropertyName("data")] TwitchUser[] Data);
    private record TwitchUser([property: JsonPropertyName("id")] string Id, [property: JsonPropertyName("login")] string Login);

    /// <summary>Devuelve la URL de autorización de Twitch.</summary>
    [HttpPost("connect")]
    [ProducesResponseType<TwitchConnectResponse>(StatusCodes.Status200OK)]
    [ProducesResponseType<ErrorResponse>(StatusCodes.Status503ServiceUnavailable)]
    public IActionResult Connect()
    {
        if (string.IsNullOrWhiteSpace(_settings.ClientId) || string.IsNullOrWhiteSpace(_settings.ClientSecret)
            || string.IsNullOrWhiteSpace(_settings.RedirectUri))
        {
            return Error(StatusCodes.Status503ServiceUnavailable, CreatorResource.TwitchNotConfigured);
        }

        var url = QueryHelpers.AddQueryString("https://id.twitch.tv/oauth2/authorize", new Dictionary<string, string?>
        {
            ["response_type"] = "code",
            ["client_id"] = _settings.ClientId,
            ["redirect_uri"] = _settings.RedirectUri,
            ["scope"] = string.Empty, // identity only
            ["force_verify"] = "true", // let the streamer pick the account: there is no disconnect yet
            ["state"] = _state.Protect(UserId(), StateLifetime)
        });

        return Ok(new TwitchConnectResponse(url));
    }

    /// <summary>Canjea el código de Twitch y guarda la identidad en la cuenta actual.</summary>
    [HttpPost("link")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType<ErrorResponse>(StatusCodes.Status400BadRequest)]
    [ProducesResponseType<ErrorResponse>(StatusCodes.Status409Conflict)]
    [ProducesResponseType<ErrorResponse>(StatusCodes.Status502BadGateway)]
    public async Task<IActionResult> Link([FromBody] TwitchLinkRequest request)
    {
        // The state must have been issued to this same user, otherwise someone else started the flow.
        if (!IsOwnState(request.State))
            return Error(StatusCodes.Status400BadRequest, CreatorResource.TwitchLinkExpired);

        var user = Guid.TryParse(UserId(), out var id) ? await _dbContext.Users.FindAsync(id) : null;
        if (user is null)
            return Unauthorized();

        var twitch = await GetTwitchUserAsync(request.Code);
        if (twitch is null)
            return Error(StatusCodes.Status502BadGateway, CreatorResource.TwitchFailed);

        if (await _dbContext.Users.AnyAsync(u => u.TwitchUserId == twitch.Id && u.Id != user.Id))
            return Error(StatusCodes.Status409Conflict, CreatorResource.TwitchTaken);

        user.TwitchUserId = twitch.Id;
        user.TwitchUsername = twitch.Login;
        user.UpdatedAt = DateTime.UtcNow;
        await _dbContext.SaveChangesAsync();

        _logger.LogInformation("Twitch {Login} vinculado al usuario {UserId}", twitch.Login, user.Id);
        return NoContent();
    }

    private string UserId() => User.FindFirst(ClaimTypes.NameIdentifier)?.Value ?? string.Empty;

    private bool IsOwnState(string state)
    {
        try { return _state.Unprotect(state) == UserId(); }
        catch (CryptographicException) { return false; } // tampered or expired
    }

    /// <summary>Exchanges the code and asks Twitch who authorized it. Null when Twitch rejects or is unreachable.</summary>
    private async Task<TwitchUser?> GetTwitchUserAsync(string code)
    {
        var http = _httpClients.CreateClient();
        try
        {
            using var tokenResponse = await http.PostAsync("https://id.twitch.tv/oauth2/token",
                new FormUrlEncodedContent(new Dictionary<string, string>
                {
                    ["client_id"] = _settings.ClientId,
                    ["client_secret"] = _settings.ClientSecret,
                    ["code"] = code,
                    ["grant_type"] = "authorization_code",
                    ["redirect_uri"] = _settings.RedirectUri
                }));
            tokenResponse.EnsureSuccessStatusCode();
            var token = await tokenResponse.Content.ReadFromJsonAsync<TokenResponse>();

            using var request = new HttpRequestMessage(HttpMethod.Get, "https://api.twitch.tv/helix/users");
            request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", token!.AccessToken);
            request.Headers.Add("Client-Id", _settings.ClientId);

            using var usersResponse = await http.SendAsync(request);
            usersResponse.EnsureSuccessStatusCode();
            var users = await usersResponse.Content.ReadFromJsonAsync<UsersResponse>();
            return users?.Data.FirstOrDefault();
        }
        catch (Exception ex) when (ex is HttpRequestException or TaskCanceledException)
        {
            _logger.LogWarning(ex, "Twitch no respondió al vincular la cuenta");
            return null;
        }
    }

    private ObjectResult Error(int status, string message) => StatusCode(status, new ErrorResponse { Message = message });
}
