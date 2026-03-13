using Acquired.Models.Cards;
using Acquired.Models.Common;
using Acquired.Services.Http;

namespace Acquired.Services.Cards;

public class CardService : ICardService
{
    private readonly IAcquiredHttpClient _httpClient;

    public CardService(IAcquiredHttpClient httpClient)
    {
        _httpClient = httpClient;
    }

    public async Task<CardResponse> GetByIdAsync(string cardId)
    {
        return await _httpClient.GetAsync<CardResponse>($"/v1/cards/{cardId}");
    }

    public async Task<PaginatedResponse<CardResponse>> GetAllAsync(PaginationQuery? query = null)
    {
        var queryParams = new Dictionary<string, string>();
        if (query?.Offset is not null) queryParams["offset"] = query.Offset.Value.ToString();
        if (query?.Limit is not null) queryParams["limit"] = query.Limit.Value.ToString();
        if (query?.Filter is not null) queryParams["filter"] = query.Filter;

        return await _httpClient.GetAsync<PaginatedResponse<CardResponse>>(
            "/v1/cards", queryParams.Count > 0 ? queryParams : null);
    }

    public async Task<PaginatedResponse<CardResponse>> GetByCustomerIdAsync(string customerId, PaginationQuery? query = null)
    {
        var queryParams = new Dictionary<string, string>();
        if (query?.Offset is not null) queryParams["offset"] = query.Offset.Value.ToString();
        if (query?.Limit is not null) queryParams["limit"] = query.Limit.Value.ToString();
        if (query?.Filter is not null) queryParams["filter"] = query.Filter;

        return await _httpClient.GetAsync<PaginatedResponse<CardResponse>>(
            $"/v1/customers/{customerId}/cards", queryParams.Count > 0 ? queryParams : null);
    }

    public async Task<CardResponse> UpdateAsync(string cardId, UpdateCardRequest request)
    {
        return await _httpClient.PutAsync<CardResponse>($"/v1/cards/{cardId}", request);
    }
}
