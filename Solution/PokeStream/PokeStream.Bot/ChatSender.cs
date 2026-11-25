using System.Threading.RateLimiting;
using Microsoft.Extensions.Logging;

namespace PokeStream.Bot;

/// <summary>Every message the bot writes goes through here, so they share Twitch's rate limit.</summary>
public sealed class ChatSender : IDisposable
{
    private readonly TwitchBotApi _api;
    private readonly ILogger<ChatSender> _logger;

    // shortcut: Twitch's limit for a regular account across all channels, extra messages are dropped;
    // moderators get 100, raise it or queue when that matters.
    private readonly SlidingWindowRateLimiter _limit = new(new SlidingWindowRateLimiterOptions
    {
        PermitLimit = 20, Window = TimeSpan.FromSeconds(30), SegmentsPerWindow = 6, QueueLimit = 0
    });

    public ChatSender(TwitchBotApi api, ILogger<ChatSender> logger)
    {
        _api = api;
        _logger = logger;
    }

    public async Task SendAsync(string broadcasterId, string message, string? replyToId = null, CancellationToken cancellationToken = default)
    {
        using var lease = _limit.AttemptAcquire();
        if (!lease.IsAcquired)
        {
            _logger.LogWarning("Message to Twitch channel {Channel} dropped: rate limit", broadcasterId);
            return;
        }

        using var response = await _api.SendMessageAsync(broadcasterId, message, replyToId, cancellationToken);
        if (!response.IsSuccessStatusCode)
        {
            var body = await response.Content.ReadAsStringAsync(cancellationToken);
            _logger.LogWarning("Twitch did not accept the message: {Status} {Body}", (int)response.StatusCode, body);
        }
    }

    public void Dispose() => _limit.Dispose();
}
