namespace Acquired.Services.Http;

public interface IAcquiredHttpClient
{
    Task<T> GetAsync<T>(string path, Dictionary<string, string>? queryParams = null);
    Task<T> PostAsync<T>(string path, object? body = null);
    Task<T> PutAsync<T>(string path, object body);
    Task<T> DeleteAsync<T>(string path);
    Task PostAsync(string path, object? body = null);
}
