using System.Collections.Concurrent;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using PokeStream.EntityData.Data;
using PokeStream.Game;
using PokeStream.Resources.Bot;

namespace PokeStream.Bot;

/// <summary>Plays the game in Twitch chats: spawns on a timer while live, flees, and the game commands.</summary>
public class ChannelGame : BackgroundService
{
    private static readonly TimeSpan Tick = TimeSpan.FromSeconds(5);
    private static readonly TimeSpan MinSpawnGap = TimeSpan.FromMinutes(3);
    private static readonly TimeSpan MaxSpawnGap = TimeSpan.FromMinutes(7);

    private readonly IServiceScopeFactory _scopes;
    private readonly TwitchBotApi _api;
    private readonly ChatSender _chat;
    private readonly ILogger<ChannelGame> _logger;
    private readonly ConcurrentDictionary<Guid, DateTime> _nextSpawn = new();
    private readonly SemaphoreSlim _spawnLock = new(1, 1); // the timer and !aparecer must not spawn together

    public ChannelGame(IServiceScopeFactory scopes, TwitchBotApi api, ChatSender chat, ILogger<ChannelGame> logger)
    {
        _scopes = scopes;
        _api = api;
        _chat = chat;
        _logger = logger;
    }

    private record Channel(Guid StreamerId, string TwitchId);

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        await Task.Yield();
        using var timer = new PeriodicTimer(Tick);
        try
        {
            while (await timer.WaitForNextTickAsync(stoppingToken))
            {
                try { await TickAsync(stoppingToken); }
                catch (Exception ex) when (!stoppingToken.IsCancellationRequested)
                {
                    _logger.LogError(ex, "Game tick failed");
                }
            }
        }
        catch (OperationCanceledException) { /* stopping */ }
    }

    /// <summary>Announces what fled and spawns in the live channels whose time has come.</summary>
    public async Task TickAsync(CancellationToken cancellationToken = default)
    {
        List<Channel> channels;
        IReadOnlyList<Departed> fled;
        Dictionary<Guid, string> fledChannels;
        using (var scope = _scopes.CreateScope())
        {
            var db = scope.ServiceProvider.GetRequiredService<PokeStreamDbContext>();
            channels = await Enabled(db).Select(u => new Channel(u.Id, u.TwitchUserId!)).ToListAsync(cancellationToken);

            fled = await scope.ServiceProvider.GetRequiredService<GameService>().FleeExpiredAsync(cancellationToken);
            var ids = fled.Select(f => f.StreamerId).ToList();
            fledChannels = await db.Users.Where(u => ids.Contains(u.Id) && u.TwitchUserId != null)
                .ToDictionaryAsync(u => u.Id, u => u.TwitchUserId!, cancellationToken);
        }

        foreach (var departed in fled)
            if (fledChannels.TryGetValue(departed.StreamerId, out var channelId))
                await _chat.SendAsync(channelId, string.Format(BotResource.Fled, departed.SpeciesName), null, cancellationToken);

        // The first spawn of a channel waits a full gap too, so restarting the bot does not spam
        var now = DateTime.UtcNow;
        var due = channels.Where(c => _nextSpawn.GetOrAdd(c.StreamerId, _ => now + RandomGap()) <= now).ToList();
        if (due.Count == 0)
            return;

        var live = await _api.GetLiveChannelsAsync(due.Select(c => c.TwitchId).ToList(), cancellationToken);
        foreach (var channel in due)
        {
            _nextSpawn[channel.StreamerId] = now + RandomGap();
            if (live.Contains(channel.TwitchId))
                await SpawnAsync(channel, cancellationToken);
        }
    }

    /// <summary>Runs <c>!capturar</c> or <c>!aparecer</c>. Does nothing in channels with spawns turned off.</summary>
    public async Task HandleAsync(ChatCommand command, string broadcasterId, Viewer viewer, string messageId, CancellationToken cancellationToken = default)
    {
        Guid streamerId;
        using (var scope = _scopes.CreateScope())
        {
            var db = scope.ServiceProvider.GetRequiredService<PokeStreamDbContext>();
            streamerId = await Enabled(db).Where(u => u.TwitchUserId == broadcasterId).Select(u => u.Id).FirstOrDefaultAsync(cancellationToken);
            if (streamerId == Guid.Empty)
                return;

            if (command == ChatCommand.Capture)
            {
                var captured = await scope.ServiceProvider.GetRequiredService<GameService>().CaptureAsync(streamerId, viewer, cancellationToken);
                if (captured is not null)
                    await _chat.SendAsync(broadcasterId, string.Format(BotResource.Captured, viewer.DisplayName, captured.SpeciesName), messageId, cancellationToken);
                return;
            }
        }

        if (command == ChatCommand.Spawn)
        {
            _nextSpawn[streamerId] = DateTime.UtcNow + RandomGap();
            await SpawnAsync(new Channel(streamerId, broadcasterId), cancellationToken);
        }
    }

    private async Task SpawnAsync(Channel channel, CancellationToken cancellationToken)
    {
        WildPokemon? spawned;
        await _spawnLock.WaitAsync(cancellationToken);
        try
        {
            using var scope = _scopes.CreateScope();
            spawned = await scope.ServiceProvider.GetRequiredService<GameService>().SpawnAsync(channel.StreamerId, cancellationToken);
        }
        finally { _spawnLock.Release(); }

        if (spawned is null)
            return;
        var name = spawned.IsShiny ? string.Format(BotResource.ShinyName, spawned.SpeciesName) : spawned.SpeciesName;
        await _chat.SendAsync(channel.TwitchId, string.Format(BotResource.Appeared, name, spawned.Level), null, cancellationToken);
    }

    private static IQueryable<EntityData.Models.User> Enabled(PokeStreamDbContext db)
        => db.Users.Where(u => u.IsActive && u.IsStreamer && u.SpawnsEnabled && u.TwitchUserId != null);

    private static TimeSpan RandomGap()
        => MinSpawnGap + (MaxSpawnGap - MinSpawnGap) * Random.Shared.NextDouble();

    public override void Dispose()
    {
        _spawnLock.Dispose();
        base.Dispose();
    }
}
