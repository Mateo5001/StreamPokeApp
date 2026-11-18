using System.ComponentModel.DataAnnotations;

namespace PokeStream.Core.DTOs.Auth;

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