using Acquired.Models.Common;
using Acquired.Services.Http;

namespace Acquired.Services.Transactions;

public class TransactionService : ITransactionService
{
    private readonly IAcquiredHttpClient _httpClient;

    public TransactionService(IAcquiredHttpClient httpClient)
    {
        _httpClient = httpClient;
    }

    public async Task<T> GetByIdAsync<T>(string transactionId)
    {
        return await _httpClient.GetAsync<T>($"/v1/transactions/{transactionId}");
    }

    public async Task<PaginatedResponse<T>> GetAllAsync<T>(PaginationQuery? query = null)
    {
        var queryParams = new Dictionary<string, string>();
        if (query?.Offset is not null) queryParams["offset"] = query.Offset.Value.ToString();
        if (query?.Limit is not null) queryParams["limit"] = query.Limit.Value.ToString();
        if (query?.Filter is not null) queryParams["filter"] = query.Filter;

        return await _httpClient.GetAsync<PaginatedResponse<T>>(
            "/v1/transactions", queryParams.Count > 0 ? queryParams : null);
    }

    public async Task<T> RefundAsync<T>(string transactionId, object request)
    {
        return await _httpClient.PostAsync<T>($"/v1/transactions/{transactionId}/refund", request);
    }

    public async Task<T> VoidAsync<T>(string transactionId)
    {
        return await _httpClient.PostAsync<T>($"/v1/transactions/{transactionId}/void");
    }

    public async Task<T> CaptureAsync<T>(string transactionId, object request)
    {
        return await _httpClient.PostAsync<T>($"/v1/transactions/{transactionId}/capture", request);
    }

    public async Task<T> ReversalAsync<T>(string transactionId, object? request = null)
    {
        return await _httpClient.PostAsync<T>($"/v1/transactions/{transactionId}/reversal", request);
    }

    public async Task<T> CancelAsync<T>(string transactionId)
    {
        return await _httpClient.PostAsync<T>($"/v1/transactions/{transactionId}/cancel");
    }

    public async Task<T> RetryAsync<T>(string transactionId, object? request = null)
    {
        return await _httpClient.PostAsync<T>($"/v1/transactions/{transactionId}/retry", request);
    }
}
