using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using PokeStream.Core.DTOs;
using PokeStream.Core.Settings;
using PokeStream.EntityData.Data;
using PokeStream.EntityData.Models;

namespace PokeStream.Core.Services;

/// <summary>
/// Servicio de autenticación con JWT y Refresh Tokens
/// </summary>
public class AuthService : IAuthService
{
    private readonly PokeStreamDbContext _dbContext;
    private readonly JwtSettings _jwtSettings;
    private readonly ILogger<AuthService> _logger;

    public AuthService(
        PokeStreamDbContext dbContext,
        IOptions<JwtSettings> jwtSettings,
        ILogger<AuthService> logger)
    {
        _dbContext = dbContext;
        _jwtSettings = jwtSettings.Value;
        _logger = logger;
    }

    public async Task<AuthResponse> RegisterAsync(RegisterRequest request, string ipAddress, string? userAgent)
    {
        // Verificar si username o email ya existen
        var existingUser = await _dbContext.Users
            .FirstOrDefaultAsync(u => u.Username == request.Username || u.Email == request.Email);

        if (existingUser != null)
        {
            throw new InvalidOperationException(existingUser.Username == request.Username
                ? "El nombre de usuario ya está en uso"
                : "El email ya está registrado");
        }

        // Crear usuario
        var user = new User
        {
            Username = request.Username,
            Email = request.Email,
            DisplayName = request.DisplayName ?? request.Username,
            PasswordHash = HashPassword(request.Password),
            TwitchUserId = request.TwitchUserId,
            TwitchUsername = request.TwitchUsername,
            YouTubeChannelId = request.YouTubeChannelId,
            YouTubeUsername = request.YouTubeUsername,
            KickUserId = request.KickUserId,
            KickUsername = request.KickUsername,
            DiscordUserId = request.DiscordUserId,
            DiscordUsername = request.DiscordUsername,
            CreatedAt = DateTime.UtcNow,
            UpdatedAt = DateTime.UtcNow,
            IsActive = true
        };

        _dbContext.Users.Add(user);
        await _dbContext.SaveChangesAsync();

        _logger.LogInformation("Usuario registrado: {Username} ({Email})", user.Username, user.Email);

        // Generar tokens
        return await GenerateAuthResponseAsync(user, ipAddress, userAgent);
    }

    public async Task<AuthResponse> LoginAsync(LoginRequest request, string ipAddress, string? userAgent)
    {
        var user = await GetUserByUsernameOrEmailAsync(request.UsernameOrEmail);

        if (user == null || !await ValidatePasswordAsync(user, request.Password))
        {
            _logger.LogWarning("Intento de login fallido para: {UsernameOrEmail}", request.UsernameOrEmail);
            throw new UnauthorizedAccessException("Credenciales inválidas");
        }

        if (!user.IsActive)
        {
            throw new UnauthorizedAccessException("La cuenta está desactivada");
        }

        // Actualizar última actividad
        user.LastActiveAt = DateTime.UtcNow;
        user.UpdatedAt = DateTime.UtcNow;
        await _dbContext.SaveChangesAsync();

        _logger.LogInformation("Usuario logueado: {Username}", user.Username);

        return await GenerateAuthResponseAsync(user, ipAddress, userAgent);
    }

    public async Task<AuthResponse> RefreshTokenAsync(RefreshTokenRequest request, string ipAddress, string? userAgent)
    {
        var session = await _dbContext.UserSessions
            .Include(s => s.User)
            .FirstOrDefaultAsync(s => s.RefreshToken == request.RefreshToken);

        if (session == null || !session.IsActive || session.ExpiresAt < DateTime.UtcNow)
        {
            throw new UnauthorizedAccessException("Refresh token inválido o expirado");
        }

        // Revocar el token actual (rotación de refresh tokens)
        session.RevokedAt = DateTime.UtcNow;
        session.RevokedReason = "Rotado por nuevo token";
        session.UpdatedAt = DateTime.UtcNow;

        // Generar nuevos tokens
        var authResponse = await GenerateAuthResponseAsync(session.User, ipAddress, userAgent);

        await _dbContext.SaveChangesAsync();

        _logger.LogInformation("Tokens renovados para usuario: {Username}", session.User.Username);

        return authResponse;
    }

    public async Task<bool> RevokeTokenAsync(string refreshToken, string ipAddress, string? userAgent)
    {
        var session = await _dbContext.UserSessions
            .FirstOrDefaultAsync(s => s.RefreshToken == refreshToken);

        if (session == null)
            return false;

        session.RevokedAt = DateTime.UtcNow;
        session.RevokedReason = "Revocado por usuario (logout)";
        session.UpdatedAt = DateTime.UtcNow;

        await _dbContext.SaveChangesAsync();

        _logger.LogInformation("Refresh token revocado para usuario: {UserId}", session.UserId);
        return true;
    }

    public async Task<User?> GetUserByIdAsync(Guid userId)
    {
        return await _dbContext.Users.FirstOrDefaultAsync(u => u.Id == userId);
    }

    public async Task<User?> GetUserByUsernameOrEmailAsync(string usernameOrEmail)
    {
        return await _dbContext.Users
            .FirstOrDefaultAsync(u => u.Username == usernameOrEmail || u.Email == usernameOrEmail);
    }

    public async Task<bool> ValidatePasswordAsync(User user, string password)
    {
        if (string.IsNullOrEmpty(user.PasswordHash))
            return false;

        return VerifyPassword(password, user.PasswordHash);
    }

    private async Task<AuthResponse> GenerateAuthResponseAsync(User user, string ipAddress, string? userAgent)
    {
        var accessToken = GenerateAccessToken(user);
        var refreshToken = GenerateRefreshToken();
        var accessTokenExpiresAt = DateTime.UtcNow.AddMinutes(_jwtSettings.AccessTokenExpiryMinutes);
        var refreshTokenExpiresAt = DateTime.UtcNow.AddDays(_jwtSettings.RefreshTokenExpiryDays);

        // Guardar sesión con refresh token
        var session = new UserSession
        {
            UserId = user.Id,
            RefreshToken = HashRefreshToken(refreshToken),
            CreatedAt = DateTime.UtcNow,
            ExpiresAt = refreshTokenExpiresAt,
            DeviceInfo = userAgent,
            IpAddress = ipAddress,
            UserAgent = userAgent,
            IsActive = true
        };

        _dbContext.UserSessions.Add(session);
        await _dbContext.SaveChangesAsync();

        return new AuthResponse
        {
            AccessToken = accessToken,
            RefreshToken = refreshToken,
            AccessTokenExpiresAt = accessTokenExpiresAt,
            RefreshTokenExpiresAt = refreshTokenExpiresAt,
            User = new UserInfoDto
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
            }
        };
    }

    private string GenerateAccessToken(User user)
    {
        var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwtSettings.Key));
        var credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

        var claims = new List<Claim>
        {
            new(ClaimTypes.NameIdentifier, user.Id.ToString()),
            new(ClaimTypes.Name, user.Username),
            new(ClaimTypes.Email, user.Email ?? string.Empty),
            new("displayName", user.DisplayName ?? user.Username),
            new(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
            new(JwtRegisteredClaimNames.Iat, DateTimeOffset.UtcNow.ToUnixTimeSeconds().ToString(), ClaimValueTypes.Integer64)
        };

        var token = new JwtSecurityToken(
            issuer: _jwtSettings.Issuer,
            audience: _jwtSettings.Audience,
            claims: claims,
            expires: DateTime.UtcNow.AddMinutes(_jwtSettings.AccessTokenExpiryMinutes),
            signingCredentials: credentials
        );

        return new JwtSecurityTokenHandler().WriteToken(token);
    }

    private string GenerateRefreshToken()
    {
        var randomBytes = new byte[64];
        using var rng = RandomNumberGenerator.Create();
        rng.GetBytes(randomBytes);
        return Convert.ToBase64String(randomBytes);
    }

    private static string HashRefreshToken(string refreshToken)
    {
        using var sha256 = SHA256.Create();
        var bytes = Encoding.UTF8.GetBytes(refreshToken);
        var hash = sha256.ComputeHash(bytes);
        return Convert.ToBase64String(hash);
    }

    private static string HashPassword(string password)
    {
        // Usar BCrypt via ASP.NET Core's built-in PasswordHasher
        var hasher = new Microsoft.AspNetCore.Identity.PasswordHasher<User>();
        return hasher.HashPassword(new User(), password);
    }

    private static bool VerifyPassword(string password, string passwordHash)
    {
        var hasher = new Microsoft.AspNetCore.Identity.PasswordHasher<User>();
        var result = hasher.VerifyHashedPassword(new User(), passwordHash, password);
        return result == Microsoft.AspNetCore.Identity.PasswordVerificationResult.Success;
    }
}