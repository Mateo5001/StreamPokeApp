using System.Collections.Concurrent;
using System.Net;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using PokeStream.EntityData.Data;
using PokeStream.Game;
using PokeStream.Resources.Bot;
using TwitchLib.EventSub.Core.EventArgs.Channel;
using TwitchLib.EventSub.Websockets;

namespace PokeStream.Bot;

/// <summary>Keeps one EventSub websocket subscribed to the chat of every linked streamer and answers commands.</summary>
public class TwitchBotService : BackgroundService
{
    private static readonly TimeSpan SyncInterval = TimeSpan.FromMinutes(1);
    private static readonly TimeSpan RetryDelay = TimeSpan.FromSeconds(10);
    private static readonly TimeSpan ChannelCooldown = TimeSpan.FromSeconds(5);

    private readonly TwitchBotApi _api;
    private readonly IServiceScopeFactory _scopes;
    private readonly ILoggerFactory _loggers;
    private readonly ILogger<TwitchBotService> _logger;
    private readonly SemaphoreSlim _wake = new(0);
    private readonly ConcurrentDictionary<string, bool> _subscribed = new();
    private readonly ConcurrentDictionary<string, DateTime> _lastReply = new();

    private readonly ChatSender _chat;
    private readonly ChannelGame _game;
    private readonly Uri? _eventSubUrl = null; // null = Twitch; only replaced by tests
    private EventSubWebsocketClient? _client;
    private volatile bool _welcomed;
    private volatile bool _dropped;

    public TwitchBotService(TwitchBotApi api, ChatSender chat, ChannelGame game, IServiceScopeFactory scopes, ILoggerFactory loggers)
    {
        _chat = chat;
        _game = game;
        _api = api;
        _scopes = scopes;
        _loggers = loggers;
        _logger = loggers.CreateLogger<TwitchBotService>();
    }

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        await Task.Yield(); // never delay the host startup

        while (!stoppingToken.IsCancellationRequested)
        {
            try
            {
                if (_dropped)
                {
                    await DisconnectAsync();
                    await Task.Delay(RetryDelay, stoppingToken);
                }
                await SyncAsync(stoppingToken);
            }
            catch (Exception ex) when (!stoppingToken.IsCancellationRequested)
            {
                _logger.LogError(ex, "Twitch bot failed, retrying");
                _dropped = _client is not null;
            }

            // After a failure the full interval applies, so a bad token does not hammer Twitch
            try { await _wake.WaitAsync(SyncInterval, stoppingToken); }
            catch (OperationCanceledException) { break; }
        }

        await DisconnectAsync();
    }

    /// <summary>Connects when there is a channel to listen to, and subscribes the ones still missing.</summary>
    private async Task SyncAsync(CancellationToken cancellationToken)
    {
        List<string> channels;
        using (var scope = _scopes.CreateScope())
        {
            var db = scope.ServiceProvider.GetRequiredService<PokeStreamDbContext>();
            // shortcut: one websocket allows about 100 chats; beyond that move to conduits (needs channel:bot).
            channels = await db.Users
                .Where(u => u.IsActive && u.IsStreamer && u.TwitchUserId != null)
                .Select(u => u.TwitchUserId!)
                .ToListAsync(cancellationToken);
        }

        // A channel that is gone: start a clean session without it
        if (_subscribed.Keys.Except(channels).Any())
            await DisconnectAsync();

        if (_client is null)
        {
            // Twitch closes a connection without subscriptions, so wait until there is a channel
            if (channels.Count > 0)
                await ConnectAsync(cancellationToken);
            return; // the welcome message wakes the loop to subscribe
        }

        if (!_welcomed)
            return;

        foreach (var channel in channels.Where(c => !_subscribed.ContainsKey(c)))
        {
            using var response = await _api.SubscribeToChatAsync(channel, _client.SessionId, cancellationToken);
            if (response.IsSuccessStatusCode || response.StatusCode == HttpStatusCode.Conflict)
            {
                _logger.LogInformation("Listening to the chat of Twitch channel {Channel}", channel);
            }
            else
            {
                var body = await response.Content.ReadAsStringAsync(cancellationToken);
                _logger.LogWarning("Cannot listen to Twitch channel {Channel}: {Status} {Body}", channel, (int)response.StatusCode, body);
                // 403 = banned or not allowed: retrying every minute will not help
                if (response.StatusCode != HttpStatusCode.Forbidden)
                    continue;
            }
            _subscribed[channel] = true;
        }
    }

    private async Task ConnectAsync(CancellationToken cancellationToken)
    {
        // Token first: Twitch allows only 10 seconds between the welcome and the first subscription
        await _api.GetAccessTokenAsync(cancellationToken);

        var client = new EventSubWebsocketClient(_loggers);
        client.WebsocketConnected += (_, e) =>
        {
            // A reconnect requested by Twitch keeps the subscriptions
            if (client == _client && !e.IsRequestedReconnect)
            {
                _welcomed = true;
                _wake.Release();
            }
            return Task.CompletedTask;
        };
        client.WebsocketDisconnected += (_, _) =>
        {
            if (client == _client)
            {
                _logger.LogWarning("Twitch websocket disconnected");
                _dropped = true;
                _wake.Release();
            }
            return Task.CompletedTask;
        };
        client.ErrorOccurred += (_, e) =>
        {
            _logger.LogWarning(e.Exception, "Twitch websocket error: {Message}", e.Message);
            return Task.CompletedTask;
        };
        client.Revocation += (_, e) =>
        {
            var subscription = e.Payload.Subscription;
            _logger.LogWarning("Twitch revoked a chat subscription ({Status}) for channel {Channel}",
                subscription.Status, subscription.Condition.GetValueOrDefault("broadcaster_user_id"));
            return Task.CompletedTask;
        };
        client.ChannelChatMessage += (_, e) =>
        {
            _ = ReplyAsync(e, cancellationToken); // do not hold the receive loop while answering
            return Task.CompletedTask;
        };

        _client = client;
        if (!await client.ConnectAsync(_eventSubUrl!))
            throw new InvalidOperationException("Could not connect to Twitch EventSub");
    }

    private async Task DisconnectAsync()
    {
        var client = _client;
        _client = null;
        _welcomed = false;
        _dropped = false;
        _subscribed.Clear();

        if (client is null)
            return;
        try { await client.DisconnectAsync(); }
        catch (Exception ex) { _logger.LogDebug(ex, "Error closing the Twitch websocket"); }
    }

    private async Task ReplyAsync(ChannelChatMessageArgs args, CancellationToken cancellationToken)
    {
        try
        {
            var message = args.Payload.Event;

            // Shared chat repeats a message in every participating channel: answer only where it was written
            if (message.SourceBroadcasterUserId is { Length: > 0 } source && source != message.BroadcasterUserId)
                return;

            var command = ChatCommands.Parse(message.Message.Text, message.ChatterUserId, _api.BotUserId, message.BroadcasterUserId);
            if (command == ChatCommand.None)
                return;

            if (command != ChatCommand.PokeStream)
            {
                var viewer = new Viewer(message.ChatterUserId, message.ChatterUserLogin, message.ChatterUserName);
                await _game.HandleAsync(command, message.BroadcasterUserId, viewer, message.MessageId, cancellationToken);
                return;
            }

            var now = DateTime.UtcNow;
            if (_lastReply.TryGetValue(message.BroadcasterUserId, out var last) && now - last < ChannelCooldown)
                return;
            _lastReply[message.BroadcasterUserId] = now;

            await _chat.SendAsync(message.BroadcasterUserId, BotResource.PokeStreamReply, message.MessageId, cancellationToken);
        }
        catch (Exception ex) when (!cancellationToken.IsCancellationRequested)
        {
            _logger.LogError(ex, "Error answering a Twitch chat message");
        }
    }

    public override void Dispose()
    {
        _wake.Dispose();
        base.Dispose();
    }
}
