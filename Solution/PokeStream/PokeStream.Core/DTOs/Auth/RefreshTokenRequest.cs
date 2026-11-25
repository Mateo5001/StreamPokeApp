using System.ComponentModel.DataAnnotations;

namespace PokeStream.Core.DTOs.Auth;

/// <summary>
/// Request para renovar access token usando refresh token
/// </summary>
public class RefreshTokenRequest
{
    [Required]
    [MaxLength(500)]
    public string RefreshToken { get; set; } = string.Empty;
}