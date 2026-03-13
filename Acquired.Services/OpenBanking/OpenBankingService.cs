using Acquired.Models.Common;
using Acquired.Services.Http;

namespace Acquired.Services.OpenBanking;

public class OpenBankingService : IOpenBankingService
{
    private readonly IAcquiredHttpClient _httpClient;

    public OpenBankingService(IAcquiredHttpClient httpClient)
    {
        _httpClient = httpClient;
    }

    public async Task<T> GetSupportedBanksAsync<T>()
    {
        return await _httpClient.GetAsync<T>("/v1/open-banking/supported-banks");
    }

    public async Task<T> CreateMandateAsync<T>(object request)
    {
        return await _httpClient.PostAsync<T>("/v1/open-banking/mandates", request);
    }

    public async Task<PaginatedResponse<T>> ListMandatesAsync<T>(PaginationQuery? query = null)
    {
        var queryParams = new Dictionary<string, string>();
        if (query?.Offset is not null) queryParams["offset"] = query.Offset.Value.ToString();
        if (query?.Limit is not null) queryParams["limit"] = query.Limit.Value.ToString();
        if (query?.Filter is not null) queryParams["filter"] = query.Filter;

        return await _httpClient.GetAsync<PaginatedResponse<T>>(
            "/v1/open-banking/mandates", queryParams.Count > 0 ? queryParams : null);
    }

    public async Task<T> GetMandateAsync<T>(string mandateId)
    {
        return await _httpClient.GetAsync<T>($"/v1/open-banking/mandates/{mandateId}");
    }

    public async Task<T> CreateVrpAsync<T>(object request)
    {
        return await _httpClient.PostAsync<T>("/v1/open-banking/vrps", request);
    }

    public async Task<PaginatedResponse<T>> ListVrpsAsync<T>(PaginationQuery? query = null)
    {
        var queryParams = new Dictionary<string, string>();
        if (query?.Offset is not null) queryParams["offset"] = query.Offset.Value.ToString();
        if (query?.Limit is not null) queryParams["limit"] = query.Limit.Value.ToString();
        if (query?.Filter is not null) queryParams["filter"] = query.Filter;

        return await _httpClient.GetAsync<PaginatedResponse<T>>(
            "/v1/open-banking/vrps", queryParams.Count > 0 ? queryParams : null);
    }

    public async Task<T> ConfirmFundsAsync<T>(string mandateId, object request)
    {
        return await _httpClient.PostAsync<T>($"/v1/open-banking/mandates/{mandateId}/confirm-funds", request);
    }
}
