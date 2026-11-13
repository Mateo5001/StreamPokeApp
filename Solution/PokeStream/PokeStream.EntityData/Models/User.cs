using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PokeStream.EntityData.Models;

/// <summary>
/// Represents a user for authentication and profile
/// </summary>
public class User
{
    [Key]
    public Guid Id { get; set; } = Guid.NewGuid();

    [Required]
    [MaxLength(100)]
    public string Username { get; set; } = string.Empty;

    [MaxLength(200)]
    public string? DisplayName { get; set; }

    [MaxLength(320)]
    public string? Email { get; set; }

    [MaxLength(500)]
    public string? AvatarUrl { get; set; }

    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;
    public DateTime? LastActiveAt { get; set; }

    public bool IsActive { get; set; } = true;

    // Password hash (for local auth)
    [MaxLength(500)]
    public string? PasswordHash { get; set; }

    // Platform-specific identifiers (nullable since user may not be on all platforms)
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

    // OAuth tokens (encrypted in production)
    [MaxLength(1000)]
    public string? TwitchAccessToken { get; set; }

    [MaxLength(1000)]
    public string? TwitchRefreshToken { get; set; }

    public DateTime? TwitchTokenExpiresAt { get; set; }

    [MaxLength(1000)]
    public string? YouTubeAccessToken { get; set; }

    [MaxLength(1000)]
    public string? YouTubeRefreshToken { get; set; }

    public DateTime? YouTubeTokenExpiresAt { get; set; }

    [MaxLength(1000)]
    public string? KickAccessToken { get; set; }

    [MaxLength(1000)]
    public string? KickRefreshToken { get; set; }

    public DateTime? KickTokenExpiresAt { get; set; }

    // Navigation properties
    public virtual ICollection<UserSession> Sessions { get; set; } = new List<UserSession>();
}