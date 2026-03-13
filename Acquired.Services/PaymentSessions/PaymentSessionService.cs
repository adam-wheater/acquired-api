using Acquired.Services.Http;

namespace Acquired.Services.PaymentSessions;

public class PaymentSessionService : IPaymentSessionService
{
    private readonly IAcquiredHttpClient _httpClient;

    public PaymentSessionService(IAcquiredHttpClient httpClient)
    {
        _httpClient = httpClient;
    }

    public async Task<T> CreateSessionAsync<T>(object request)
    {
        return await _httpClient.PostAsync<T>("/v1/payment-sessions", request);
    }

    public async Task<T> UpdateSessionAsync<T>(string sessionId, object request)
    {
        return await _httpClient.PutAsync<T>($"/v1/payment-sessions/{sessionId}", request);
    }
}
