using Acquired.Services.Http;

namespace Acquired.Services.Payments;

public class PaymentService : IPaymentService
{
    private readonly IAcquiredHttpClient _httpClient;

    public PaymentService(IAcquiredHttpClient httpClient)
    {
        _httpClient = httpClient;
    }

    public async Task<T> CreatePaymentAsync<T>(object request)
    {
        return await _httpClient.PostAsync<T>("/v1/payments", request);
    }

    public async Task<T> CreateReusePaymentAsync<T>(object request)
    {
        return await _httpClient.PostAsync<T>("/v1/payments/reuse", request);
    }

    public async Task<T> CreateApplePayAsync<T>(object request)
    {
        return await _httpClient.PostAsync<T>("/v1/payments/apple-pay", request);
    }

    public async Task<T> CreateGooglePayAsync<T>(object request)
    {
        return await _httpClient.PostAsync<T>("/v1/payments/google-pay", request);
    }

    public async Task<T> CreateRecurringAsync<T>(object request)
    {
        return await _httpClient.PostAsync<T>("/v1/payments/recurring", request);
    }

    public async Task<T> CreateCreditAsync<T>(object request)
    {
        return await _httpClient.PostAsync<T>("/v1/payments/credit", request);
    }

    public async Task<T> CreateInternalTransferAsync<T>(object request)
    {
        return await _httpClient.PostAsync<T>("/v1/payments/internal-transfer", request);
    }

    public async Task<T> CreateCollectionAsync<T>(object request)
    {
        return await _httpClient.PostAsync<T>("/v1/payments/collections", request);
    }
}
