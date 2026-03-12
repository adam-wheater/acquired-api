using Acquired.Services.Auth;
using Acquired.Services.Configuration;
using Acquired.Services.Exceptions;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Text;
using System.Web;

namespace Acquired.Services.Http;

public class AcquiredHttpClient : IAcquiredHttpClient
{
    private readonly IHttpClientFactory _httpClientFactory;
    private readonly IAcquiredTokenService _tokenService;
    private readonly AcquiredOptions _options;
    private readonly IHttpContextAccessor _httpContextAccessor;
    private readonly ILogger<AcquiredHttpClient> _logger;

    public AcquiredHttpClient(
        IHttpClientFactory httpClientFactory,
        IAcquiredTokenService tokenService,
        IOptions<AcquiredOptions> options,
        IHttpContextAccessor httpContextAccessor,
        ILogger<AcquiredHttpClient> logger)
    {
        _httpClientFactory = httpClientFactory;
        _tokenService = tokenService;
        _options = options.Value;
        _httpContextAccessor = httpContextAccessor;
        _logger = logger;
    }

    public async Task<TResponse> SendAsync<TResponse>(HttpMethod method, string path, object? body, CancellationToken ct = default)
    {
        var request = await CreateRequest(method, path, ct);

        if (body is not null)
        {
            var json = JsonConvert.SerializeObject(body, new JsonSerializerSettings
            {
                NullValueHandling = NullValueHandling.Ignore
            });
            request.Content = new StringContent(json, Encoding.UTF8, "application/json");
        }

        return await ExecuteAsync<TResponse>(request, ct);
    }

    public async Task<TResponse> GetAsync<TResponse>(string path, Dictionary<string, string?>? queryParams = null, CancellationToken ct = default)
    {
        var fullPath = BuildQueryString(path, queryParams);
        var request = await CreateRequest(HttpMethod.Get, fullPath, ct);
        return await ExecuteAsync<TResponse>(request, ct);
    }

    public async Task SendWithoutResponseAsync(HttpMethod method, string path, object? body, CancellationToken ct = default)
    {
        var request = await CreateRequest(method, path, ct);

        if (body is not null)
        {
            var json = JsonConvert.SerializeObject(body, new JsonSerializerSettings
            {
                NullValueHandling = NullValueHandling.Ignore
            });
            request.Content = new StringContent(json, Encoding.UTF8, "application/json");
        }

        var client = _httpClientFactory.CreateClient("Acquired");
        var response = await client.SendAsync(request, ct);
        var responseBody = await response.Content.ReadAsStringAsync(ct);

        if (!response.IsSuccessStatusCode)
        {
            HandleErrorResponse((int)response.StatusCode, responseBody);
        }
    }

    private async Task<HttpRequestMessage> CreateRequest(HttpMethod method, string path, CancellationToken ct)
    {
        var token = await _tokenService.GetTokenAsync(ct);
        var request = new HttpRequestMessage(method, path);

        request.Headers.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", token);

        var context = _httpContextAccessor.HttpContext;
        if (context is not null)
        {
            if (context.Items.TryGetValue("CompanyId", out var companyId) && companyId is string cid)
            {
                request.Headers.Add("Company-Id", cid);
            }

            if (context.Items.TryGetValue("Mid", out var mid) && mid is string midValue && !string.IsNullOrEmpty(midValue))
            {
                request.Headers.Add("Mid", midValue);
            }

            if (context.Items.TryGetValue("CorrelationId", out var correlationId) && correlationId is string corrId)
            {
                request.Headers.Add("X-Correlation-Id", corrId);
            }
        }

        return request;
    }

    private async Task<TResponse> ExecuteAsync<TResponse>(HttpRequestMessage request, CancellationToken ct)
    {
        var client = _httpClientFactory.CreateClient("Acquired");

        _logger.LogDebug("Sending {Method} {Path}", request.Method, request.RequestUri);

        var response = await client.SendAsync(request, ct);
        var responseBody = await response.Content.ReadAsStringAsync(ct);

        if (!response.IsSuccessStatusCode)
        {
            HandleErrorResponse((int)response.StatusCode, responseBody);
        }

        // Check for declined payment status
        InspectPaymentStatus(responseBody);

        var result = JsonConvert.DeserializeObject<TResponse>(responseBody);
        return result ?? throw new InvalidOperationException(
            $"Failed to deserialize response to {typeof(TResponse).Name}");
    }

    private void InspectPaymentStatus(string responseBody)
    {
        try
        {
            var json = JObject.Parse(responseBody);
            var status = json.SelectToken("status")?.Value<string>();

            if (status is "declined" or "blocked")
            {
                var transactionId = json.SelectToken("transaction_id")?.Value<string>();
                throw new AcquiredDeclinedException($"Payment {status}")
                {
                    Status = status,
                    TransactionId = transactionId,
                    DeclineReason = status
                };
            }
        }
        catch (JsonReaderException)
        {
            // Not JSON, ignore
        }
        catch (AcquiredDeclinedException)
        {
            throw;
        }
    }

    private void HandleErrorResponse(int statusCode, string responseBody)
    {
        _logger.LogWarning("Acquired.com API error: {StatusCode} {Body}", statusCode, responseBody);

        string? errorType = null;
        string? title = null;
        string? instance = null;

        try
        {
            var json = JObject.Parse(responseBody);
            errorType = json.SelectToken("error_type")?.Value<string>();
            title = json.SelectToken("title")?.Value<string>();
            instance = json.SelectToken("instance")?.Value<string>();
        }
        catch (JsonReaderException)
        {
            // Not JSON
        }

        throw new AcquiredApiException(title ?? $"Acquired.com API returned {statusCode}")
        {
            AcquiredHttpStatusCode = statusCode,
            ErrorType = errorType,
            Title = title,
            Instance = instance
        };
    }

    private static string BuildQueryString(string path, Dictionary<string, string?>? queryParams)
    {
        if (queryParams is null || queryParams.Count == 0)
            return path;

        var query = HttpUtility.ParseQueryString(string.Empty);
        foreach (var (key, value) in queryParams)
        {
            if (value is not null)
                query[key] = value;
        }

        return $"{path}?{query}";
    }
}
