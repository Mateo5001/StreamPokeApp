using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PokeStream.Core.DTOs.User;
using PokeStream.EntityData.Data;

namespace PokeStream.API.Controllers;

/// <summary>Opciones del panel de creadores.</summary>
[ApiController]
[Route("api/v1/creator")]
[Authorize(Roles = "Streamer")]
public class CreatorController : ControllerBase
{
    private readonly PokeStreamDbContext _dbContext;

    public CreatorController(PokeStreamDbContext dbContext) => _dbContext = dbContext;

    /// <summary>Activa o desactiva las apariciones en el canal del streamer.</summary>
    [HttpPut("spawns")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    public async Task<IActionResult> SetSpawns([FromBody] SpawnSettingsRequest request)
    {
        var user = Guid.TryParse(User.FindFirst(ClaimTypes.NameIdentifier)?.Value, out var id)
            ? await _dbContext.Users.FindAsync(id)
            : null;
        if (user is null)
            return Unauthorized();

        user.SpawnsEnabled = request.Enabled;
        user.UpdatedAt = DateTime.UtcNow;
        await _dbContext.SaveChangesAsync();
        return NoContent();
    }
}
