using System.Security.Claims;
using System.Security.Cryptography;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.WebUtilities;
using PokeStream.Core.DTOs.Overlay;
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
        var user = await CurrentUserAsync();
        if (user is null)
            return Unauthorized();

        user.SpawnsEnabled = request.Enabled;
        user.UpdatedAt = DateTime.UtcNow;
        await _dbContext.SaveChangesAsync();
        return NoContent();
    }

    /// <summary>Token de la URL del overlay; se crea la primera vez.</summary>
    [HttpGet("overlay")]
    [ProducesResponseType<OverlayTokenResponse>(StatusCodes.Status200OK)]
    public Task<IActionResult> GetOverlayToken() => OverlayTokenAsync(regenerate: false);

    /// <summary>Cambia el token: la URL anterior deja de funcionar.</summary>
    [HttpPost("overlay/regenerate")]
    [ProducesResponseType<OverlayTokenResponse>(StatusCodes.Status200OK)]
    public Task<IActionResult> RegenerateOverlayToken() => OverlayTokenAsync(regenerate: true);

    private async Task<IActionResult> OverlayTokenAsync(bool regenerate)
    {
        var user = await CurrentUserAsync();
        if (user is null)
            return Unauthorized();

        if (regenerate || user.OverlayToken is null)
        {
            user.OverlayToken = WebEncoders.Base64UrlEncode(RandomNumberGenerator.GetBytes(32));
            user.UpdatedAt = DateTime.UtcNow;
            await _dbContext.SaveChangesAsync();
        }
        return Ok(new OverlayTokenResponse(user.OverlayToken));
    }

    private async Task<EntityData.Models.User?> CurrentUserAsync()
        => Guid.TryParse(User.FindFirst(ClaimTypes.NameIdentifier)?.Value, out var id)
            ? await _dbContext.Users.FindAsync(id)
            : null;
}
