using System.ComponentModel.DataAnnotations;

namespace PokeStream.Core.DTOs.Auth;

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