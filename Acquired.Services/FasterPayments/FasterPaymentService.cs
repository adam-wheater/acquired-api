using Acquired.Services.Http;

namespace Acquired.Services.FasterPayments;

public class FasterPaymentService : IFasterPaymentService
{
    private readonly IAcquiredHttpClient _httpClient;

    public FasterPaymentService(IAcquiredHttpClient httpClient)
    {
        _httpClient = httpClient;
    }

    public async Task<T> CreatePayeeAsync<T>(string customerId, object request)
    {
        return await _httpClient.PostAsync<T>($"/v1/customers/{customerId}/payees", request);
    }

    public async Task<T> ListPayeesAsync<T>()
    {
        return await _httpClient.GetAsync<T>("/v1/payees");
    }

    public async Task<T> CreatePayoutAsync<T>(object request)
    {
        return await _httpClient.PostAsync<T>("/v1/pay-out", request);
    }

    public async Task<T> CreateAccountAsync<T>(object request)
    {
        return await _httpClient.PostAsync<T>("/v1/accounts", request);
    }

    public async Task<T> ListAccountsAsync<T>()
    {
        return await _httpClient.GetAsync<T>("/v1/accounts");
    }

    public async Task<T> GetAccountByMidAsync<T>(string mid)
    {
        return await _httpClient.GetAsync<T>($"/v1/accounts/{mid}");
    }
}
