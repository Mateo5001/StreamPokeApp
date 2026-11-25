using PokeStream.Core.DTOs.User;

namespace PokeStream.Core.DTOs.Auth;

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