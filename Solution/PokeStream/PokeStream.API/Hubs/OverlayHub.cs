using Microsoft.AspNetCore.SignalR;
using Microsoft.EntityFrameworkCore;
using PokeStream.Core.DTOs.Overlay;
using PokeStream.EntityData.Data;
using PokeStream.Game;

namespace PokeStream.API.Hubs;

/// <summary>
/// Overlays connect without a login: the secret token in their URL is the credential. The group is the token itself,
/// so a regenerated token leaves the old connections without events.
/// </summary>
public class OverlayHub : Hub
{
    private readonly PokeStreamDbContext _dbContext;
    private readonly GameService _game;

    public OverlayHub(PokeStreamDbContext dbContext, GameService game)
    {
        _dbContext = dbContext;
        _game = game;
    }

    [HubMethodName(OverlayHubContract.Join)]
    public async Task<OverlayWild?> JoinAsync(string token)
    {
        var streamerId = string.IsNullOrWhiteSpace(token)
            ? Guid.Empty
            : await _dbContext.Users.Where(u => u.OverlayToken == token && u.IsActive && u.IsStreamer)
                .Select(u => u.Id).FirstOrDefaultAsync(Context.ConnectionAborted);
        if (streamerId == Guid.Empty)
            throw new HubException("invalid-token");

        await Groups.AddToGroupAsync(Context.ConnectionId, token, Context.ConnectionAborted);
        var wild = await _game.GetActiveAsync(streamerId, Context.ConnectionAborted);
        return wild is null ? null : ToOverlay(wild);
    }

    public static OverlayWild ToOverlay(WildPokemon wild)
        => new(wild.SpeciesName, wild.Level, wild.IsShiny, wild.SpriteUrl,
            Math.Max(0, (int)Math.Ceiling((wild.ExpiresAt - DateTime.UtcNow).TotalSeconds)));
}
