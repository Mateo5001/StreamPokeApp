using PokeStream.Core.DTOs;
using PokeStream.EntityData.Models;

namespace PokeStream.Core.Services;

/// <summary>
/// Interfaz del servicio de autenticación
/// </summary>
public interface IAuthService
{
    Task<AuthResponse> RegisterAsync(RegisterRequest request, string ipAddress, string? userAgent);
    Task<AuthResponse> LoginAsync(LoginRequest request, string ipAddress, string? userAgent);
    Task<AuthResponse> RefreshTokenAsync(RefreshTokenRequest request, string ipAddress, string? userAgent);
    Task<bool> RevokeTokenAsync(string refreshToken, string ipAddress, string? userAgent);
    Task<User?> GetUserByIdAsync(Guid userId);
    Task<User?> GetUserByUsernameOrEmailAsync(string usernameOrEmail);
    Task<bool> ValidatePasswordAsync(User user, string password);
}