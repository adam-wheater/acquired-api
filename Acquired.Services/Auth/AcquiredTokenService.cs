using Acquired.Models.Auth;
using Acquired.Services.Configuration;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using System.Text;

namespace Acquired.Services.Auth;

public class AcquiredTokenService : IAcquiredTokenService
{
    private readonly IHttpClientFactory _httpClientFactory;
    private readonly AcquiredOptions _options;
    private readonly ILogger<AcquiredTokenService> _logger;
    private readonly SemaphoreSlim _semaphore = new(1, 1);

    private string? _cachedToken;
    private DateTime _tokenExpiry = DateTime.MinValue;

    public AcquiredTokenService(
        IHttpClientFactory httpClientFactory,
        IOptions<AcquiredOptions> options,
        ILogger<AcquiredTokenService> logger)
    {
        _httpClientFactory = httpClientFactory;
        _options = options.Value;
        _logger = logger;
    }

    public async Task<string> GetTokenAsync(CancellationToken ct = default)
    {
        if (_cachedToken is not null && DateTime.UtcNow < _tokenExpiry)
            return _cachedToken;

        await _semaphore.WaitAsync(ct);
        try
        {
            // Double-check after acquiring lock
            if (_cachedToken is not null && DateTime.UtcNow < _tokenExpiry)
                return _cachedToken;

            _logger.LogInformation("Acquiring new Acquired.com access token");

            var client = _httpClientFactory.CreateClient("Acquired");
            var loginRequest = new LoginRequest
            {
                AppId = _options.AppId,
                AppKey = _options.AppKey
            };

            var json = JsonConvert.SerializeObject(loginRequest);
            var content = new StringContent(json, Encoding.UTF8, "application/json");

            var response = await client.PostAsync("/v1/login", content, ct);
            var responseBody = await response.Content.ReadAsStringAsync(ct);

            if (!response.IsSuccessStatusCode)
            {
                _logger.LogError("Failed to acquire token: {StatusCode} {Body}",
                    (int)response.StatusCode, responseBody);
                throw new InvalidOperationException(
                    $"Failed to acquire Acquired.com token: {response.StatusCode}");
            }

            var loginResponse = JsonConvert.DeserializeObject<LoginResponse>(responseBody)
                ?? throw new InvalidOperationException("Failed to deserialize login response");

            _cachedToken = loginResponse.AccessToken
                ?? throw new InvalidOperationException("Access token was null in login response");

            _tokenExpiry = DateTime.UtcNow.AddSeconds(
                loginResponse.ExpiresIn - _options.TokenBufferSeconds);

            _logger.LogInformation("Acquired.com token acquired, expires in {ExpiresIn}s",
                loginResponse.ExpiresIn);

            return _cachedToken;
        }
        finally
        {
            _semaphore.Release();
        }
    }
}
