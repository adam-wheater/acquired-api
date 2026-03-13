using Acquired.Services.Http;

namespace Acquired.Services.PaymentLinks;

public class PaymentLinkService : IPaymentLinkService
{
    private readonly IAcquiredHttpClient _httpClient;

    public PaymentLinkService(IAcquiredHttpClient httpClient)
    {
        _httpClient = httpClient;
    }

    public async Task<T> CreatePaymentLinkAsync<T>(object request)
    {
        return await _httpClient.PostAsync<T>("/v1/payment-links", request);
    }

    public async Task<T> SendPaymentLinkAsync<T>(string linkId, object? request = null)
    {
        return await _httpClient.PostAsync<T>($"/v1/payment-links/{linkId}/send", request);
    }
}
