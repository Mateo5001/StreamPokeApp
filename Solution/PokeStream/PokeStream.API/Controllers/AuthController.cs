using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PokeStream.Core.DTOs;
using PokeStream.Core.Services;

namespace PokeStream.API.Controllers;

/// <summary>
/// Endpoints de autenticación: registro, login, refresh token, logout
/// </summary>
[ApiController]
[Route("api/v1/auth")]
[Produces("application/json")]
public class AuthController : ControllerBase
{
    private readonly IAuthService _authService;
    private readonly ILogger<AuthController> _logger;

    public AuthController(IAuthService authService, ILogger<AuthController> logger)
    {
        _authService = authService;
        _logger = logger;
    }

    /// <summary>
    /// Registra un nuevo usuario
    /// </summary>
    /// <param name="request">Datos de registro</param>
    /// <returns>Tokens de acceso y refresh + info de usuario</returns>
    [HttpPost("register")]
    [ProducesResponseType<AuthResponse>(StatusCodes.Status201Created)]
    [ProducesResponseType<ErrorResponse>(StatusCodes.Status400BadRequest)]
    [ProducesResponseType<ErrorResponse>(StatusCodes.Status409Conflict)]
    public async Task<IActionResult> Register([FromBody] RegisterRequest request)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(new ErrorResponse
            {
                Message = "Datos de registro inválidos",
                Errors = ModelState.Where(e => e.Value?.Errors.Count > 0)
                    .ToDictionary(e => e.Key, e => e.Value!.Errors.Select(err => err.ErrorMessage).ToArray())
            });
        }

        try
        {
            var ipAddress = GetClientIpAddress();
            var userAgent = Request.Headers.UserAgent.ToString();

            var response = await _authService.RegisterAsync(request, ipAddress, userAgent);

            return CreatedAtAction(nameof(Register), response);
        }
        catch (InvalidOperationException ex)
        {
            _logger.LogWarning(ex, "Error en registro: {Message}", ex.Message);
            return Conflict(new ErrorResponse { Message = ex.Message });
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error inesperado en registro");
            return StatusCode(500, new ErrorResponse { Message = "Error interno del servidor" });
        }
    }

    /// <summary>
    /// Inicia sesión de usuario
    /// </summary>
    /// <param name="request">Credenciales de login</param>
    /// <returns>Tokens de acceso y refresh + info de usuario</returns>
    [HttpPost("login")]
    [ProducesResponseType<AuthResponse>(StatusCodes.Status200OK)]
    [ProducesResponseType<ErrorResponse>(StatusCodes.Status400BadRequest)]
    [ProducesResponseType<ErrorResponse>(StatusCodes.Status401Unauthorized)]
    public async Task<IActionResult> Login([FromBody] LoginRequest request)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(new ErrorResponse
            {
                Message = "Datos de login inválidos",
                Errors = ModelState.Where(e => e.Value?.Errors.Count > 0)
                    .ToDictionary(e => e.Key, e => e.Value!.Errors.Select(err => err.ErrorMessage).ToArray())
            });
        }

        try
        {
            var ipAddress = GetClientIpAddress();
            var userAgent = Request.Headers.UserAgent.ToString();

            var response = await _authService.LoginAsync(request, ipAddress, userAgent);

            return Ok(response);
        }
        catch (UnauthorizedAccessException ex)
        {
            _logger.LogWarning(ex, "Login fallido: {Message}", ex.Message);
            return Unauthorized(new ErrorResponse { Message = ex.Message });
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error inesperado en login");
            return StatusCode(500, new ErrorResponse { Message = "Error interno del servidor" });
        }
    }

    /// <summary>
    /// Renueva el access token usando refresh token
    /// </summary>
    /// <param name="request">Refresh token</param>
    /// <returns>Nuevos tokens de acceso y refresh</returns>
    [HttpPost("refresh")]
    [ProducesResponseType<AuthResponse>(StatusCodes.Status200OK)]
    [ProducesResponseType<ErrorResponse>(StatusCodes.Status400BadRequest)]
    [ProducesResponseType<ErrorResponse>(StatusCodes.Status401Unauthorized)]
    public async Task<IActionResult> RefreshToken([FromBody] RefreshTokenRequest request)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(new ErrorResponse
            {
                Message = "Refresh token requerido",
                Errors = ModelState.Where(e => e.Value?.Errors.Count > 0)
                    .ToDictionary(e => e.Key, e => e.Value!.Errors.Select(err => err.ErrorMessage).ToArray())
            });
        }

        try
        {
            var ipAddress = GetClientIpAddress();
            var userAgent = Request.Headers.UserAgent.ToString();

            var response = await _authService.RefreshTokenAsync(request, ipAddress, userAgent);

            return Ok(response);
        }
        catch (UnauthorizedAccessException ex)
        {
            _logger.LogWarning(ex, "Refresh token inválido: {Message}", ex.Message);
            return Unauthorized(new ErrorResponse { Message = ex.Message });
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error inesperado en refresh token");
            return StatusCode(500, new ErrorResponse { Message = "Error interno del servidor" });
        }
    }

    /// <summary>
    /// Cierra sesión (revoca el refresh token)
    /// </summary>
    /// <param name="request">Refresh token a revocar</param>
    /// <returns>Confirmación</returns>
    [HttpPost("logout")]
    [Authorize]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType<ErrorResponse>(StatusCodes.Status400BadRequest)]
    [ProducesResponseType<ErrorResponse>(StatusCodes.Status401Unauthorized)]
    public async Task<IActionResult> Logout([FromBody] RefreshTokenRequest request)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(new ErrorResponse
            {
                Message = "Refresh token requerido",
                Errors = ModelState.Where(e => e.Value?.Errors.Count > 0)
                    .ToDictionary(e => e.Key, e => e.Value!.Errors.Select(err => err.ErrorMessage).ToArray())
            });
        }

        try
        {
            var ipAddress = GetClientIpAddress();
            var userAgent = Request.Headers.UserAgent.ToString();

            var revoked = await _authService.RevokeTokenAsync(request.RefreshToken, ipAddress, userAgent);

            if (!revoked)
            {
                return NotFound(new ErrorResponse { Message = "Sesión no encontrada" });
            }

            return Ok(new { Message = "Sesión cerrada correctamente" });
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error inesperado en logout");
            return StatusCode(500, new ErrorResponse { Message = "Error interno del servidor" });
        }
    }

    /// <summary>
    /// Obtiene información del usuario autenticado
    /// </summary>
    /// <returns>Info del usuario</returns>
    [HttpGet("me")]
    [Authorize]
    [ProducesResponseType<UserInfoDto>(StatusCodes.Status200OK)]
    [ProducesResponseType<ErrorResponse>(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType<ErrorResponse>(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> GetCurrentUser()
    {
        var userIdClaim = User.FindFirst(System.Security.Claims.ClaimTypes.NameIdentifier)?.Value;

        if (!Guid.TryParse(userIdClaim, out var userId))
        {
            return Unauthorized(new ErrorResponse { Message = "Token inválido" });
        }

        var user = await _authService.GetUserByIdAsync(userId);

        if (user == null)
        {
            return NotFound(new ErrorResponse { Message = "Usuario no encontrado" });
        }

        return Ok(new UserInfoDto
        {
            Id = user.Id,
            Username = user.Username,
            DisplayName = user.DisplayName,
            Email = user.Email,
            AvatarUrl = user.AvatarUrl,
            CreatedAt = user.CreatedAt,
            HasPassword = !string.IsNullOrEmpty(user.PasswordHash),
            HasTwitchLinked = !string.IsNullOrEmpty(user.TwitchUserId),
            HasYouTubeLinked = !string.IsNullOrEmpty(user.YouTubeChannelId),
            HasKickLinked = !string.IsNullOrEmpty(user.KickUserId),
            HasDiscordLinked = !string.IsNullOrEmpty(user.DiscordUserId)
        });
    }

    private string GetClientIpAddress()
    {
        // Manejar proxies/load balancers
        if (Request.Headers.ContainsKey("X-Forwarded-For"))
        {
            var forwardedFor = Request.Headers["X-Forwarded-For"].FirstOrDefault();
            if (!string.IsNullOrEmpty(forwardedFor))
                return forwardedFor.Split(',')[0].Trim();
        }

        if (Request.Headers.ContainsKey("X-Real-IP"))
        {
            return Request.Headers["X-Real-IP"].FirstOrDefault() ?? string.Empty;
        }

        return HttpContext.Connection.RemoteIpAddress?.ToString() ?? "unknown";
    }
}