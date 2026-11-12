using System.ComponentModel.DataAnnotations;

namespace PokeStream.Core.DTOs;

/// <summary>
/// Request para registro de nuevo usuario
/// </summary>
public class RegisterRequest
{
    [Required]
    [MaxLength(100)]
    [MinLength(3)]
    public string Username { get; set; } = string.Empty;

    [Required]
    [EmailAddress]
    [MaxLength(320)]
    public string Email { get; set; } = string.Empty;

    [Required]
    [MinLength(8)]
    [MaxLength(100)]
    public string Password { get; set; } = string.Empty;

    [MaxLength(200)]
    public string? DisplayName { get; set; }

    // Opcional: vincular cuenta de plataforma al registrar
    [MaxLength(100)]
    public string? TwitchUserId { get; set; }

    [MaxLength(100)]
    public string? TwitchUsername { get; set; }

    [MaxLength(100)]
    public string? YouTubeChannelId { get; set; }

    [MaxLength(100)]
    public string? YouTubeUsername { get; set; }

    [MaxLength(100)]
    public string? KickUserId { get; set; }

    [MaxLength(100)]
    public string? KickUsername { get; set; }

    [MaxLength(100)]
    public string? DiscordUserId { get; set; }

    [MaxLength(100)]
    public string? DiscordUsername { get; set; }
}

/// <summary>
/// Request para login
/// </summary>
public class LoginRequest
{
    [Required]
    [MaxLength(100)]
    public string UsernameOrEmail { get; set; } = string.Empty;

    [Required]
    [MaxLength(100)]
    public string Password { get; set; } = string.Empty;

    [MaxLength(200)]
    public string? DeviceInfo { get; set; }

    [MaxLength(500)]
    public string? UserAgent { get; set; }

    [MaxLength(100)]
    public string? IpAddress { get; set; }
}

/// <summary>
/// Request para renovar access token usando refresh token
/// </summary>
public class RefreshTokenRequest
{
    [Required]
    [MaxLength(500)]
    public string RefreshToken { get; set; } = string.Empty;
}

/// <summary>
/// Response con tokens de autenticación
/// </summary>
public class AuthResponse
{
    public string AccessToken { get; set; } = string.Empty;
    public string RefreshToken { get; set; } = string.Empty;
    public DateTime AccessTokenExpiresAt { get; set; }
    public DateTime RefreshTokenExpiresAt { get; set; }
    public UserInfoDto User { get; set; } = null!;
}

/// <summary>
/// Info básica del usuario en responses
/// </summary>
public class UserInfoDto
{
    public Guid Id { get; set; }
    public string Username { get; set; } = string.Empty;
    public string? DisplayName { get; set; }
    public string? Email { get; set; }
    public string? AvatarUrl { get; set; }
    public DateTime CreatedAt { get; set; }
    public bool HasPassword { get; set; }
    public bool HasTwitchLinked { get; set; }
    public bool HasYouTubeLinked { get; set; }
    public bool HasKickLinked { get; set; }
    public bool HasDiscordLinked { get; set; }
}

/// <summary>
/// Response genérico de error
/// </summary>
public class ErrorResponse
{
    public string Message { get; set; } = string.Empty;
    public Dictionary<string, string[]>? Errors { get; set; }
}