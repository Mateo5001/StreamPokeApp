using System.ComponentModel.DataAnnotations;

namespace PokeStream.EntityData.Models;

/// <summary>Token of a chat bot account, one row per platform.</summary>
public class BotCredential
{
    [Key]
    [MaxLength(20)]
    public string Platform { get; set; } = string.Empty;

    /// <summary>Latest refresh token, encrypted with data protection.</summary>
    [Required]
    [MaxLength(2000)]
    public string RefreshToken { get; set; } = string.Empty;

    /// <summary>Hash of the configured token it came from; a different configured token replaces it.</summary>
    [MaxLength(100)]
    public string SeedHash { get; set; } = string.Empty;

    public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;
}
