using System.Net.Http.Json;
using Microsoft.Extensions.Configuration;

namespace PokeStream.Core.Services.Http;

/// <summary><see cref="IHttpClient"/> over a plain <see cref="HttpClient"/>; base URL comes from <c>ApiBaseUrl</c>.</summary>
public sealed class ApiHttpClient : IHttpClient
{
    private const string DefaultBaseUrl = "http://localhost:5130/";

    private readonly HttpClient _client;

    public Uri BaseAddress { get; }

    public ApiHttpClient(HttpClient client, IConfiguration configuration)
    {
        _client = client;

        var baseUrl = configuration["ApiBaseUrl"];
        if (string.IsNullOrWhiteSpace(baseUrl))
            baseUrl = DefaultBaseUrl;

        // Trailing slash required, otherwise the last path segment of the base is dropped when resolving.
        BaseAddress = new Uri(baseUrl.EndsWith('/') ? baseUrl : baseUrl + "/");
    }

    public Task<HttpResponseMessage> GetAsync(string url, CancellationToken cancellationToken = default)
        => _client.GetAsync(BuildUri(url), cancellationToken);

    public Task<HttpResponseMessage> PostAsync(string url, HttpContent? content, CancellationToken cancellationToken = default)
        => _client.PostAsync(BuildUri(url), content, cancellationToken);

    public Task<HttpResponseMessage> PutAsync(string url, HttpContent? content, CancellationToken cancellationToken = default)
        => _client.PutAsync(BuildUri(url), content, cancellationToken);

    public Task<HttpResponseMessage> DeleteAsync(string url, CancellationToken cancellationToken = default)
        => _client.DeleteAsync(BuildUri(url), cancellationToken);

    public Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken = default)
    {
        request.RequestUri = request.RequestUri is null
            ? BaseAddress
            : BuildUri(request.RequestUri.OriginalString);
        return _client.SendAsync(request, cancellationToken);
    }

    public Task<T?> GetFromJsonAsync<T>(string url, CancellationToken cancellationToken = default)
        => _client.GetFromJsonAsync<T>(BuildUri(url), cancellationToken);

    public Task<HttpResponseMessage> PostAsJsonAsync<T>(string url, T value, CancellationToken cancellationToken = default)
        => _client.PostAsJsonAsync(BuildUri(url), value, cancellationToken);

    public Task<HttpResponseMessage> PutAsJsonAsync<T>(string url, T value, CancellationToken cancellationToken = default)
        => _client.PutAsJsonAsync(BuildUri(url), value, cancellationToken);

    /// <summary>Absolute URLs pass through; relative ones are appended to <see cref="BaseAddress"/>.</summary>
    private Uri BuildUri(string url)
        => Uri.TryCreate(url, UriKind.Absolute, out var absolute)
            ? absolute
            : new Uri(BaseAddress, url.TrimStart('/'));
}
