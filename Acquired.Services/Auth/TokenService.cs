using System.Net.Http.Headers;
using System.Text;
using Acquired.Models.Auth;
using Acquired.Services.Configuration;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;

namespace Acquired.Services.Auth;

public class TokenService : ITokenService
{
    private readonly IHttpClientFactory _httpClientFactory;
    private readonly AcquiredOptions _options;
    private readonly ILogger<TokenService> _logger;
    private readonly SemaphoreSlim _semaphore = new(1, 1);

    private string? _cachedToken;
    private DateTime _tokenExpiry = DateTime.MinValue;

    public TokenService(
        IHttpClientFactory httpClientFactory,
        IOptions<AcquiredOptions> options,
        ILogger<TokenService> logger)
    {
        _httpClientFactory = httpClientFactory;
        _options = options.Value;
        _logger = logger;
    }

    public async Task<string> GetTokenAsync()
    {
        if (_cachedToken is not null && DateTime.UtcNow < _tokenExpiry)
        {
            return _cachedToken;
        }

        await _semaphore.WaitAsync();
        try
        {
            // Double-check after acquiring the lock
            if (_cachedToken is not null && DateTime.UtcNow < _tokenExpiry)
            {
                return _cachedToken;
            }

            _logger.LogInformation("Acquiring new access token from Acquired.com API");

            var client = _httpClientFactory.CreateClient("AcquiredAuth");

            var loginRequest = new LoginRequest
            {
                AppId = _options.AppId,
                AppKey = _options.AppKey
            };

            var json = JsonConvert.SerializeObject(loginRequest);
            var content = new StringContent(json, Encoding.UTF8, "application/json");

            var response = await client.PostAsync("/v1/login", content);
            var responseBody = await response.Content.ReadAsStringAsync();

            if (!response.IsSuccessStatusCode)
            {
                _logger.LogError("Failed to acquire token. Status: {StatusCode}, Body: {Body}",
                    (int)response.StatusCode, responseBody);
                throw new HttpRequestException(
                    $"Failed to acquire token. Status: {(int)response.StatusCode}");
            }

            var loginResponse = JsonConvert.DeserializeObject<LoginResponse>(responseBody);

            if (string.IsNullOrEmpty(loginResponse?.AccessToken))
            {
                throw new InvalidOperationException("Login response did not contain an access token.");
            }

            _cachedToken = loginResponse.AccessToken;
            _tokenExpiry = DateTime.UtcNow.AddSeconds(
                loginResponse.ExpiresIn - _options.TokenBufferSeconds);

            _logger.LogInformation("Access token acquired. Expires in {ExpiresIn}s (buffer: {Buffer}s)",
                loginResponse.ExpiresIn, _options.TokenBufferSeconds);

            return _cachedToken;
        }
        finally
        {
            _semaphore.Release();
        }
    }
}
