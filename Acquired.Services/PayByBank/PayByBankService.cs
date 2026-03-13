using Acquired.Services.Http;

namespace Acquired.Services.PayByBank;

public class PayByBankService : IPayByBankService
{
    private readonly IAcquiredHttpClient _httpClient;

    public PayByBankService(IAcquiredHttpClient httpClient)
    {
        _httpClient = httpClient;
    }

    public async Task<T> GetAspspsAsync<T>()
    {
        return await _httpClient.GetAsync<T>("/v1/aspsps");
    }

    public async Task<T> CreateSingleImmediatePaymentAsync<T>(object request)
    {
        return await _httpClient.PostAsync<T>("/v1/single-immediate-payment", request);
    }
}
