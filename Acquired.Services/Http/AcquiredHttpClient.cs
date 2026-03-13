using System.Net.Http.Headers;
using System.Text;
using Acquired.Services.Auth;
using Acquired.Services.Exceptions;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;

namespace Acquired.Services.Http;

public class AcquiredHttpClient : IAcquiredHttpClient
{
    private readonly HttpClient _httpClient;
    private readonly ITokenService _tokenService;
    private readonly IHttpContextAccessor _httpContextAccessor;
    private readonly ILogger<AcquiredHttpClient> _logger;

    private static readonly Dictionary<string, string> PropagatedHeaders = new()
    {
        ["Company-Id"] = "Company-Id",
        ["Mid"] = "Mid",
        ["CorrelationId"] = "X-Correlation-Id"
    };

    public AcquiredHttpClient(
        IHttpClientFactory httpClientFactory,
        ITokenService tokenService,
        IHttpContextAccessor httpContextAccessor,
        ILogger<AcquiredHttpClient> logger)
    {
        _httpClient = httpClientFactory.CreateClient("AcquiredApi");
        _tokenService = tokenService;
        _httpContextAccessor = httpContextAccessor;
        _logger = logger;
    }

    public async Task<T> GetAsync<T>(string path, Dictionary<string, string>? queryParams = null)
    {
        var url = BuildUrl(path, queryParams);
        var request = new HttpRequestMessage(HttpMethod.Get, url);
        return await SendAsync<T>(request);
    }

    public async Task<T> PostAsync<T>(string path, object? body = null)
    {
        var request = new HttpRequestMessage(HttpMethod.Post, path);
        if (body is not null)
        {
            request.Content = Serialize(body);
        }
        return await SendAsync<T>(request);
    }

    public async Task<T> PutAsync<T>(string path, object body)
    {
        var request = new HttpRequestMessage(HttpMethod.Put, path);
        request.Content = Serialize(body);
        return await SendAsync<T>(request);
    }

    public async Task<T> DeleteAsync<T>(string path)
    {
        var request = new HttpRequestMessage(HttpMethod.Delete, path);
        return await SendAsync<T>(request);
    }

    public async Task PostAsync(string path, object? body = null)
    {
        var request = new HttpRequestMessage(HttpMethod.Post, path);
        if (body is not null)
        {
            request.Content = Serialize(body);
        }
        await SendAsync(request);
    }

    private async Task<T> SendAsync<T>(HttpRequestMessage request)
    {
        await PrepareRequest(request);

        _logger.LogDebug("Sending {Method} {Uri}", request.Method, request.RequestUri);

        var response = await _httpClient.SendAsync(request);
        var content = await response.Content.ReadAsStringAsync();

        if (!response.IsSuccessStatusCode)
        {
            _logger.LogWarning("Acquired API error {StatusCode} for {Method} {Uri}: {Content}",
                (int)response.StatusCode, request.Method, request.RequestUri, content);
            throw new AcquiredException(response, content);
        }

        return JsonConvert.DeserializeObject<T>(content)!;
    }

    private async Task SendAsync(HttpRequestMessage request)
    {
        await PrepareRequest(request);

        _logger.LogDebug("Sending {Method} {Uri}", request.Method, request.RequestUri);

        var response = await _httpClient.SendAsync(request);

        if (!response.IsSuccessStatusCode)
        {
            var content = await response.Content.ReadAsStringAsync();
            _logger.LogWarning("Acquired API error {StatusCode} for {Method} {Uri}: {Content}",
                (int)response.StatusCode, request.Method, request.RequestUri, content);
            throw new AcquiredException(response, content);
        }
    }

    private async Task PrepareRequest(HttpRequestMessage request)
    {
        var token = await _tokenService.GetTokenAsync();
        request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", token);

        PropagateHeaders(request);
    }

    private void PropagateHeaders(HttpRequestMessage request)
    {
        var httpContext = _httpContextAccessor.HttpContext;
        if (httpContext is null) return;

        foreach (var (itemKey, headerName) in PropagatedHeaders)
        {
            if (httpContext.Items.TryGetValue(itemKey, out var value) && value is string strValue)
            {
                request.Headers.TryAddWithoutValidation(headerName, strValue);
            }
        }
    }

    private static string BuildUrl(string path, Dictionary<string, string>? queryParams)
    {
        if (queryParams is null || queryParams.Count == 0)
        {
            return path;
        }

        var queryParts = queryParams
            .Where(kvp => kvp.Value is not null)
            .Select(kvp => $"{Uri.EscapeDataString(kvp.Key)}={Uri.EscapeDataString(kvp.Value)}");

        return $"{path}?{string.Join("&", queryParts)}";
    }

    private static StringContent Serialize(object body)
    {
        var json = JsonConvert.SerializeObject(body);
        return new StringContent(json, Encoding.UTF8, "application/json");
    }
}
