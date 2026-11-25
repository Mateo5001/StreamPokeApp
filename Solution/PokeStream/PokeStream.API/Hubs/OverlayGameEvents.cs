using Microsoft.AspNetCore.SignalR;
using Microsoft.EntityFrameworkCore;
using PokeStream.Core.DTOs.Overlay;
using PokeStream.EntityData.Data;
using PokeStream.Game;

namespace PokeStream.API.Hubs;

/// <summary>Sends what happens in a channel to its overlays. A failure here never breaks the game.</summary>
public class OverlayGameEvents : IGameEvents
{
    private readonly IHubContext<OverlayHub> _hub;
    private readonly IServiceScopeFactory _scopes;
    private readonly ILogger<OverlayGameEvents> _logger;

    public OverlayGameEvents(IHubContext<OverlayHub> hub, IServiceScopeFactory scopes, ILogger<OverlayGameEvents> logger)
    {
        _hub = hub;
        _scopes = scopes;
        _logger = logger;
    }

    public Task SpawnedAsync(Guid streamerId, WildPokemon wild)
        => SendAsync(streamerId, overlay => overlay.SendAsync(OverlayHubContract.Spawned, OverlayHub.ToOverlay(wild)));

    public Task CapturedAsync(Guid streamerId, string speciesName, string trainerName)
        => SendAsync(streamerId, overlay => overlay.SendAsync(OverlayHubContract.Captured, speciesName, trainerName));

    public Task FledAsync(Guid streamerId, string speciesName)
        => SendAsync(streamerId, overlay => overlay.SendAsync(OverlayHubContract.Fled, speciesName));

    private async Task SendAsync(Guid streamerId, Func<IClientProxy, Task> send)
    {
        try
        {
            using var scope = _scopes.CreateScope();
            var db = scope.ServiceProvider.GetRequiredService<PokeStreamDbContext>();
            var token = await db.Users.Where(u => u.Id == streamerId).Select(u => u.OverlayToken).FirstOrDefaultAsync();
            if (token is not null)
                await send(_hub.Clients.Group(token));
        }
        catch (Exception ex)
        {
            _logger.LogWarning(ex, "Could not notify the overlay of streamer {StreamerId}", streamerId);
        }
    }
}
