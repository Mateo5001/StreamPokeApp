using System.Net.Http.Json;
using System.Text.Json;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.JSInterop;
using PokeStream.Core.DTOs.Auth;
using PokeStream.Core.DTOs.User;
using PokeStream.Core.Services.Cookies;
using PokeStream.Core.Services.Http;

namespace PokeStream.Core.Services.AuthServices;

/// <summary>
/// Servicio de autenticación para la aplicación Blazor. Los tokens viven en una cookie HttpOnly
/// (<see cref="CookieKeys.Session"/>) y se mantienen en memoria durante el circuito.
/// Flujo: la carga inicial lee la cookie (HttpContext) → <see cref="PersistentComponentState"/> la pasa al circuito
/// interactivo (donde no hay HttpContext) → los cambios se escriben de vuelta a la cookie vía <see cref="ICookieService"/>.
/// </summary>
public class AuthStateService : AuthenticationStateProvider, IDisposable
{
    private sealed record StoredSession(string AccessToken, string RefreshToken);

    private const string SessionStateKey = "ps_session_state";

    private readonly IHttpClient _httpClient;
    private readonly ICookieService _cookies;
    private readonly PersistentComponentState _componentState;
    private readonly NavigationManager _navigation;
    private readonly PersistingComponentStateSubscription _persisting;
    private AuthenticationState? _cachedState;
    private Task<bool>? _refreshing;

    private StoredSession? _session;
    private bool _sessionLoaded;
    private bool _fromHttpRequest; // true while running in the prerender pass: no JS, so cookies cannot be written

    public AuthStateService(IHttpClient httpClient, ICookieService cookies, PersistentComponentState componentState, NavigationManager navigation)
    {
        _httpClient = httpClient;
        _cookies = cookies;
        _componentState = componentState;
        _navigation = navigation;
        _persisting = componentState.RegisterOnPersisting(() =>
        {
            if (_session != null)
                _componentState.PersistAsJson(SessionStateKey, _session);
            return Task.CompletedTask;
        });
    }

    public void Dispose() => _persisting.Dispose();

    private StoredSession? LoadSession()
    {
        if (_sessionLoaded)
            return _session;
        _sessionLoaded = true;

        // Interactive circuit: the prerender pass handed the session over.
        if (_componentState.TryTakeFromJson<StoredSession>(SessionStateKey, out var restored) && restored != null)
            return _session = restored;

        // Initial HTTP request (prerender): read the HttpOnly cookie.
        var raw = _cookies.GetSecure();
        if (!string.IsNullOrEmpty(raw))
        {
            try
            {
                _session = JsonSerializer.Deserialize<StoredSession>(raw);
                _fromHttpRequest = _session != null;
            }
            catch (JsonException)
            {
                // Corrupted cookie: treat as signed out
            }
        }
        return _session;
    }

    private static AuthenticationState Anonymous() =>
        new(new System.Security.Claims.ClaimsPrincipal(new System.Security.Claims.ClaimsIdentity()));

    public override async Task<AuthenticationState> GetAuthenticationStateAsync()
    {
        var token = LoadSession()?.AccessToken;

        // The cache is only good while the access token it came from is.
        if (_cachedState != null &&
            (string.IsNullOrEmpty(token) || !TryParseClaims(token, out _, out var cachedExpiry) || cachedExpiry > DateTimeOffset.UtcNow))
            return _cachedState;

        if (string.IsNullOrEmpty(token))
            return _cachedState = Anonymous();

        if (!TryParseClaims(token, out var claims, out var expiresAt))
        {
            // Corrupted token in the cookie: discard it so the app does not get stuck
            await TryClearTokensAsync();
            return _cachedState = Anonymous();
        }

        if (expiresAt <= DateTimeOffset.UtcNow)
        {
            // Prerender pass cannot write cookies, and refresh rotates the token: leave it for the interactive circuit.
            if (_fromHttpRequest)
                return Anonymous();

            // Expired access token: try to renew it. On network failure do not cache, so it is retried later.
            return await RefreshCoreAsync() ? _cachedState! : Anonymous();
        }

        return _cachedState = BuildState(claims);
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
            NotifyUserAuthentication(authResponse);
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
            NotifyUserAuthentication(authResponse);
        }

        return authResponse;
    }

    public async Task<bool> LogoutAsync()
    {
        // The logout endpoint needs a valid access token; renewing also rotates the refresh token, so read it afterwards.
        await GetAuthenticationStateAsync();
        var refreshToken = await GetRefreshTokenAsync();
        if (!string.IsNullOrEmpty(refreshToken))
        {
            try
            {
                using var request = await CreateAuthorizedRequestAsync(HttpMethod.Post, "api/v1/auth/logout");
                request.Content = JsonContent.Create(new RefreshTokenRequest { RefreshToken = refreshToken });
                await _httpClient.SendAsync(request);
            }
            catch
            {
                // Ignorar errores de red en logout
            }
        }

        await TryClearTokensAsync();
        _cachedState = Anonymous();
        NotifyAuthenticationStateChanged(Task.FromResult(_cachedState));
        return true;
    }

    public async Task<bool> RefreshTokenAsync()
    {
        var refreshed = await RefreshCoreAsync();
        if (_cachedState != null)
            NotifyAuthenticationStateChanged(Task.FromResult(_cachedState));
        return refreshed;
    }

    /// <summary>
    /// Renews the tokens. Success: stores them and caches the authenticated state.
    /// Rejected by the API: clears tokens and caches the anonymous state.
    /// Network error: changes nothing (can be retried).
    /// </summary>
    // Concurrent callers share one request: the API rotates the token, so a second one would be rejected and sign us out.
    // shortcut: only per circuit; two tabs on the same session can still sign each other out, fix when it bites.
    private async Task<bool> RefreshCoreAsync()
    {
        var refreshing = _refreshing ??= SendRefreshAsync();
        try { return await refreshing; }
        finally { _refreshing = null; }
    }

    private async Task<bool> SendRefreshAsync()
    {
        var refreshToken = await GetRefreshTokenAsync();
        if (string.IsNullOrEmpty(refreshToken))
        {
            await TryClearTokensAsync();
            _cachedState = Anonymous();
            return false;
        }

        HttpResponseMessage response;
        try
        {
            response = await _httpClient.PostAsJsonAsync("api/v1/auth/refresh", new RefreshTokenRequest { RefreshToken = refreshToken });
        }
        catch (HttpRequestException)
        {
            return false;
        }

        var authResponse = response.IsSuccessStatusCode
            ? await response.Content.ReadFromJsonAsync<AuthResponse>()
            : null;

        if (authResponse == null)
        {
            await TryClearTokensAsync();
            _cachedState = Anonymous();
            return false;
        }

        await StoreTokensAsync(authResponse);
        _cachedState = TryParseClaims(authResponse.AccessToken, out var claims, out _)
            ? BuildState(claims)
            : Anonymous();
        return true;
    }

    /// <summary>Calls the API as the signed-in user.</summary>
    public async Task<HttpResponseMessage> SendAuthorizedAsync(HttpMethod method, string url, HttpContent? content = null)
    {
        // Makes sure the access token is valid (renews it if expired) before using it
        await GetAuthenticationStateAsync();

        using var request = await CreateAuthorizedRequestAsync(method, url);
        request.Content = content;
        return await _httpClient.SendAsync(request);
    }

    public async Task<UserInfoDto?> GetCurrentUserAsync()
    {
        var response = await SendAuthorizedAsync(HttpMethod.Get, "api/v1/auth/me");

        if (!response.IsSuccessStatusCode)
            return null;

        return await response.Content.ReadFromJsonAsync<UserInfoDto>();
    }

    // Per-request header: the HttpClient is shared inside the scope, so no DefaultRequestHeaders
    private async Task<HttpRequestMessage> CreateAuthorizedRequestAsync(HttpMethod method, string url)
    {
        var request = new HttpRequestMessage(method, url);
        var token = await GetAccessTokenAsync();
        if (!string.IsNullOrEmpty(token))
            request.Headers.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", token);
        return request;
    }

    public Task<string?> GetAccessTokenAsync() => Task.FromResult(LoadSession()?.AccessToken);

    public Task<string?> GetRefreshTokenAsync() => Task.FromResult(LoadSession()?.RefreshToken);

    private async Task StoreTokensAsync(AuthResponse authResponse)
    {
        _session = new StoredSession(authResponse.AccessToken, authResponse.RefreshToken);
        _sessionLoaded = true;
        _fromHttpRequest = false;

        var days = Math.Max(1, (int)Math.Ceiling((authResponse.RefreshTokenExpiresAt - DateTime.UtcNow).TotalDays));
        await _cookies.SetSecureAsync(JsonSerializer.Serialize(_session), days);
    }

    private async Task TryClearTokensAsync()
    {
        _session = null;
        _sessionLoaded = true;
        try
        {
            await _cookies.RemoveSecureAsync();
        }
        catch (Exception ex) when (ex is InvalidOperationException or JSException or JSDisconnectedException)
        {
            // No JS available (prerendering / disconnected circuit): nothing to clear
        }
    }

    public void NotifyUserAuthentication(AuthResponse authResponse)
    {
        _cachedState = TryParseClaims(authResponse.AccessToken, out var claims, out _)
            ? BuildState(claims)
            : Anonymous();
        NotifyAuthenticationStateChanged(Task.FromResult(_cachedState));
    }

    private static AuthenticationState BuildState(IEnumerable<System.Security.Claims.Claim> claims) =>
        new(new System.Security.Claims.ClaimsPrincipal(new System.Security.Claims.ClaimsIdentity(claims, "jwt")));

    /// <summary>Parses a JWT payload. Returns false for any malformed token. expiresAt = MaxValue when there is no exp claim.</summary>
    private static bool TryParseClaims(string jwt, out List<System.Security.Claims.Claim> claims, out DateTimeOffset expiresAt)
    {
        claims = new();
        expiresAt = DateTimeOffset.MaxValue;

        try
        {
            var parts = jwt.Split('.');
            if (parts.Length < 2)
                return false;

            var pairs = JsonSerializer.Deserialize<Dictionary<string, object>>(ParseBase64WithoutPadding(parts[1]));
            if (pairs == null)
                return false;

            claims = pairs.Select(kvp => new System.Security.Claims.Claim(kvp.Key, kvp.Value?.ToString() ?? string.Empty)).ToList();

            if (pairs.TryGetValue("exp", out var exp) && long.TryParse(exp?.ToString(), out var seconds))
                expiresAt = DateTimeOffset.FromUnixTimeSeconds(seconds);

            return true;
        }
        catch (Exception ex) when (ex is FormatException or JsonException or ArgumentOutOfRangeException)
        {
            return false;
        }
    }

    private static byte[] ParseBase64WithoutPadding(string base64)
    {
        base64 = base64.Replace('-', '+').Replace('_', '/');
        switch (base64.Length % 4)
        {
            case 2: base64 += "=="; break;
            case 3: base64 += "="; break;
        }
        return Convert.FromBase64String(base64);
    }
}
