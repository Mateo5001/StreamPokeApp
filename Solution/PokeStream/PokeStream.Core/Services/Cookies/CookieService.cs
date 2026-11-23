using Microsoft.AspNetCore.Http;
using Microsoft.JSInterop;

namespace PokeStream.Core.Services.Cookies;

/// <summary>
/// <see cref="ICookieService"/> backed by <c>wwwroot/js/cookies.js</c> (UI.Web) and the HttpOnly endpoint
/// <see cref="CookieKeys.SessionEndpoint"/>. Register as scoped.
/// </summary>
public sealed class CookieService : ICookieService, IAsyncDisposable
{
    private readonly IJSRuntime _js;
    private readonly IHttpContextAccessor _http;
    private IJSObjectReference? _module;

    public CookieService(IJSRuntime js, IHttpContextAccessor http)
    {
        _js = js;
        _http = http;
    }

    public async Task SetAsync(string name, string value, int? days = null)
        => await (await Module()).InvokeVoidAsync("set", name, value, days);

    public async Task<string?> GetAsync(string name)
        => await (await Module()).InvokeAsync<string?>("get", name);

    public async Task RemoveAsync(string name)
        => await (await Module()).InvokeVoidAsync("remove", name);

    public async Task SetSecureAsync(string value, int? days = null)
    {
        if (value.Length > CookieKeys.MaxValueLength)
            throw new ArgumentException($"Cookie value exceeds {CookieKeys.MaxValueLength} characters.", nameof(value));

        await (await Module()).InvokeVoidAsync("send", "POST", CookieKeys.SessionEndpoint, new SessionCookieRequest(value, days));
    }

    public string? GetSecure() => _http.HttpContext?.Request.Cookies[CookieKeys.Session];

    public async Task RemoveSecureAsync()
        => await (await Module()).InvokeVoidAsync("send", "DELETE", CookieKeys.SessionEndpoint, null);

    private async Task<IJSObjectReference> Module()
        => _module ??= await _js.InvokeAsync<IJSObjectReference>("import", "./js/cookies.js");

    public async ValueTask DisposeAsync()
    {
        if (_module is null) return;
        try { await _module.DisposeAsync(); }
        catch (JSDisconnectedException) { /* circuit already gone */ }
    }
}
