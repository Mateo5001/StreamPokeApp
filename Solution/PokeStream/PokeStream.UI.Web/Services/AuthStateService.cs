using System.Net.Http.Json;
using System.Text.Json;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.JSInterop;
using PokeStream.Core.DTOs;

namespace PokeStream.UI.Web.Services;

/// <summary>
/// Servicio de autenticación para la aplicación Blazor (maneja tokens en localStorage)
/// </summary>
public class AuthStateService : AuthenticationStateProvider
{
    private readonly HttpClient _httpClient;
    private readonly IJSRuntime _jsRuntime;
    private readonly NavigationManager _navigation;
    private AuthenticationState? _cachedState;

    private const string AccessTokenKey = "pokestream_access_token";
    private const string RefreshTokenKey = "pokestream_refresh_token";
    private const string UserKey = "pokestream_user";

    public AuthStateService(HttpClient httpClient, IJSRuntime jsRuntime, NavigationManager navigation)
    {
        _httpClient = httpClient;
        _jsRuntime = jsRuntime;
        _navigation = navigation;
    }

    public override async Task<AuthenticationState> GetAuthenticationStateAsync()
    {
        if (_cachedState != null)
            return _cachedState;

        string? token = null;
        try
        {
            token = await GetAccessTokenAsync();
        }
        catch (InvalidOperationException ex) when (ex.Message.Contains("prerendering") || ex.Message.Contains("statically rendered"))
        {
            // During prerendering, JS interop is not available - return unauthenticated state
            // The actual auth state will be resolved after interactive render
        }

        if (string.IsNullOrEmpty(token))
        {
            _cachedState = new AuthenticationState(new System.Security.Claims.ClaimsPrincipal(new System.Security.Claims.ClaimsIdentity()));
            return _cachedState;
        }

        var identity = new System.Security.Claims.ClaimsIdentity(ParseClaimsFromJwt(token), "jwt");
        var user = new System.Security.Claims.ClaimsPrincipal(identity);
        _cachedState = new AuthenticationState(user);
        return _cachedState;
    }

    public async Task<AuthResponse?> LoginAsync(LoginRequest request)
    {
        var response = await _httpClient.PostAsJsonAsync("api/v1/auth/login", request);

        if (!response.IsSuccessStatusCode)
            return null;

        var authResponse = await response.Content.ReadFromJsonAsync<AuthResponse>();
        if (authResponse != null)
        {
            await StoreTokensAsync(authResponse);
            NotifyAuthenticationStateChanged(GetAuthenticationStateAsync());
        }

        return authResponse;
    }

    public async Task<AuthResponse?> RegisterAsync(RegisterRequest request)
    {
        var response = await _httpClient.PostAsJsonAsync("api/v1/auth/register", request);

        if (!response.IsSuccessStatusCode)
            return null;

        var authResponse = await response.Content.ReadFromJsonAsync<AuthResponse>();
        if (authResponse != null)
        {
            await StoreTokensAsync(authResponse);
            NotifyAuthenticationStateChanged(GetAuthenticationStateAsync());
        }

        return authResponse;
    }

    public async Task<bool> LogoutAsync()
    {
        var refreshToken = await GetRefreshTokenAsync();
        if (!string.IsNullOrEmpty(refreshToken))
        {
            try
            {
                await _httpClient.PostAsJsonAsync("api/v1/auth/logout", new RefreshTokenRequest { RefreshToken = refreshToken });
            }
            catch
            {
                // Ignorar errores de red en logout
            }
        }

        await ClearTokensAsync();
        _cachedState = new AuthenticationState(new System.Security.Claims.ClaimsPrincipal(new System.Security.Claims.ClaimsIdentity()));
        NotifyAuthenticationStateChanged(Task.FromResult(_cachedState));
        return true;
    }

    public async Task<bool> RefreshTokenAsync()
    {
        var refreshToken = await GetRefreshTokenAsync();
        if (string.IsNullOrEmpty(refreshToken))
            return false;

        var response = await _httpClient.PostAsJsonAsync("api/v1/auth/refresh", new RefreshTokenRequest { RefreshToken = refreshToken });

        if (!response.IsSuccessStatusCode)
        {
            await ClearTokensAsync();
            _cachedState = new AuthenticationState(new System.Security.Claims.ClaimsPrincipal(new System.Security.Claims.ClaimsIdentity()));
            NotifyAuthenticationStateChanged(Task.FromResult(_cachedState));
            return false;
        }

        var authResponse = await response.Content.ReadFromJsonAsync<AuthResponse>();
        if (authResponse != null)
        {
            await StoreTokensAsync(authResponse);
            NotifyAuthenticationStateChanged(GetAuthenticationStateAsync());
            return true;
        }

        return false;
    }

    public async Task<UserInfoDto?> GetCurrentUserAsync()
    {
        var response = await _httpClient.GetAsync("api/v1/auth/me");

        if (!response.IsSuccessStatusCode)
            return null;

        return await response.Content.ReadFromJsonAsync<UserInfoDto>();
    }

    public async Task<string?> GetAccessTokenAsync()
    {
        try
        {
            return await _jsRuntime.InvokeAsync<string>("localStorage.getItem", AccessTokenKey);
        }
        catch (InvalidOperationException ex) when (ex.Message.Contains("prerendering") || ex.Message.Contains("statically rendered"))
        {
            return null; // During prerendering, return null - will be resolved after interactive render
        }
    }

    public async Task<string?> GetRefreshTokenAsync()
    {
        try
        {
            return await _jsRuntime.InvokeAsync<string>("localStorage.getItem", RefreshTokenKey);
        }
        catch (InvalidOperationException ex) when (ex.Message.Contains("prerendering") || ex.Message.Contains("statically rendered"))
        {
            return null; // During prerendering, return null - will be resolved after interactive render
        }
    }

    private async Task StoreTokensAsync(AuthResponse authResponse)
    {
        await _jsRuntime.InvokeVoidAsync("localStorage.setItem", AccessTokenKey, authResponse.AccessToken);
        await _jsRuntime.InvokeVoidAsync("localStorage.setItem", RefreshTokenKey, authResponse.RefreshToken);
        await _jsRuntime.InvokeVoidAsync("localStorage.setItem", UserKey, JsonSerializer.Serialize(authResponse.User));
    }

    private async Task ClearTokensAsync()
    {
        await _jsRuntime.InvokeVoidAsync("localStorage.removeItem", AccessTokenKey);
        await _jsRuntime.InvokeVoidAsync("localStorage.removeItem", RefreshTokenKey);
        await _jsRuntime.InvokeVoidAsync("localStorage.removeItem", UserKey);
    }

    public void NotifyUserAuthentication(AuthResponse authResponse)
    {
        var identity = new System.Security.Claims.ClaimsIdentity(ParseClaimsFromJwt(authResponse.AccessToken), "jwt");
        var user = new System.Security.Claims.ClaimsPrincipal(identity);
        _cachedState = new AuthenticationState(user);
        NotifyAuthenticationStateChanged(Task.FromResult(_cachedState));
    }

    private static IEnumerable<System.Security.Claims.Claim> ParseClaimsFromJwt(string jwt)
    {
        var payload = jwt.Split('.')[1];
        var jsonBytes = ParseBase64WithoutPadding(payload);
        var keyValuePairs = JsonSerializer.Deserialize<Dictionary<string, object>>(jsonBytes);

        return keyValuePairs?.Select(kvp => new System.Security.Claims.Claim(kvp.Key, kvp.Value?.ToString() ?? string.Empty))
               ?? Enumerable.Empty<System.Security.Claims.Claim>();
    }

    private static byte[] ParseBase64WithoutPadding(string base64)
    {
        switch (base64.Length % 4)
        {
            case 2: base64 += "=="; break;
            case 3: base64 += "="; break;
        }
        return Convert.FromBase64String(base64);
    }
}