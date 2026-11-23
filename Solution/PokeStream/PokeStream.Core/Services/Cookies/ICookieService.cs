namespace PokeStream.Core.Services.Cookies;

/// <summary>
/// Browser cookie storage for Blazor Server. Names come from <see cref="CookieKeys"/>.
/// Two flavors:
/// <list type="bullet">
/// <item>Plain (<c>SetAsync/GetAsync/RemoveAsync</c>): written with <c>document.cookie</c>, readable anytime from JS interop. For theme and remembered user.</item>
/// <item>Secure (<c>*SecureAsync</c>): HttpOnly cookie issued by the server, unreadable by JS (XSS-safe). For session tokens. Only <see cref="CookieKeys.Session"/> is allowed.</item>
/// </list>
/// All JS-backed calls must run after the first render (OnAfterRenderAsync), never during prerender.
/// <c>days</c> = null creates a session cookie (cleared when the browser closes).
/// </summary>
public interface ICookieService
{
    Task SetAsync(string name, string value, int? days = null);

    Task<string?> GetAsync(string name);

    Task RemoveAsync(string name);

    /// <summary>Stores <paramref name="value"/> in the HttpOnly <see cref="CookieKeys.Session"/> cookie.</summary>
    Task SetSecureAsync(string value, int? days = null);

    /// <summary>
    /// Reads the HttpOnly session cookie from the current HTTP request. Only available on the initial page load
    /// (prerender / first request); returns null inside an established interactive circuit.
    /// </summary>
    string? GetSecure();

    Task RemoveSecureAsync();
}
