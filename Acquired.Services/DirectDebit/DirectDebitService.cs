using Acquired.Services.Http;

namespace Acquired.Services.DirectDebit;

public class DirectDebitService : IDirectDebitService
{
    private readonly IAcquiredHttpClient _httpClient;

    public DirectDebitService(IAcquiredHttpClient httpClient)
    {
        _httpClient = httpClient;
    }

    public async Task<T> CreateMandateAsync<T>(object request)
    {
        return await _httpClient.PostAsync<T>("/v1/mandates", request);
    }

    public async Task<T> GetMandateAsync<T>(string mandateId)
    {
        return await _httpClient.GetAsync<T>($"/v1/mandates/{mandateId}");
    }

    public async Task<T> CancelMandateAsync<T>(string mandateId)
    {
        return await _httpClient.PostAsync<T>($"/v1/mandates/{mandateId}/cancel");
    }
}
