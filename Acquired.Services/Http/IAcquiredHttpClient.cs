namespace Acquired.Services.Http;

public interface IAcquiredHttpClient
{
    Task<TResponse> SendAsync<TResponse>(HttpMethod method, string path, object? body, CancellationToken ct = default);
    Task<TResponse> GetAsync<TResponse>(string path, Dictionary<string, string?>? queryParams = null, CancellationToken ct = default);
    Task SendWithoutResponseAsync(HttpMethod method, string path, object? body, CancellationToken ct = default);
}
