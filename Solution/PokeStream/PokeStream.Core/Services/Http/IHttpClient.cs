namespace PokeStream.Core.Services.Http;

/// <summary>
/// HttpClient wrapper that prefixes every relative URL with the API base address (<c>ApiBaseUrl</c> in appsettings).
/// Pass paths like <c>"api/v1/auth/login"</c>; absolute URLs are sent untouched.
/// </summary>
public interface IHttpClient
{
    /// <summary>Resolved API base address (always ends with '/').</summary>
    Uri BaseAddress { get; }

    Task<HttpResponseMessage> GetAsync(string url, CancellationToken cancellationToken = default);
    Task<HttpResponseMessage> PostAsync(string url, HttpContent? content, CancellationToken cancellationToken = default);
    Task<HttpResponseMessage> PutAsync(string url, HttpContent? content, CancellationToken cancellationToken = default);
    Task<HttpResponseMessage> DeleteAsync(string url, CancellationToken cancellationToken = default);

    /// <summary>Sends a custom request. A relative <c>RequestUri</c> is resolved against <see cref="BaseAddress"/>.</summary>
    Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken = default);

    Task<T?> GetFromJsonAsync<T>(string url, CancellationToken cancellationToken = default);
    Task<HttpResponseMessage> PostAsJsonAsync<T>(string url, T value, CancellationToken cancellationToken = default);
    Task<HttpResponseMessage> PutAsJsonAsync<T>(string url, T value, CancellationToken cancellationToken = default);
}
