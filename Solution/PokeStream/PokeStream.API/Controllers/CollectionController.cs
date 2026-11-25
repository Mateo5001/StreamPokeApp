using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PokeStream.Core.DTOs.User;
using PokeStream.Game;

namespace PokeStream.API.Controllers;

/// <summary>Colección del usuario autenticado.</summary>
[ApiController]
[Route("api/v1/collection")]
[Authorize]
[Produces("application/json")]
public class CollectionController : ControllerBase
{
    private readonly GameService _game;

    public CollectionController(GameService game) => _game = game;

    /// <summary>Especies capturadas y equipo.</summary>
    [HttpGet]
    [ProducesResponseType<CollectionDto>(StatusCodes.Status200OK)]
    public async Task<IActionResult> Get()
    {
        // Only ever the caller's own: the id comes from the token, never from the request
        if (!Guid.TryParse(User.FindFirst(ClaimTypes.NameIdentifier)?.Value, out var userId))
            return Unauthorized();

        var collection = await _game.GetCollectionAsync(userId, HttpContext.RequestAborted);
        return Ok(new CollectionDto(collection.SpeciesIds,
            collection.Team.Select(p => new TeamMemberDto(p.Name, p.Level, p.IsShiny, p.SpriteUrl)).ToList()));
    }
}
