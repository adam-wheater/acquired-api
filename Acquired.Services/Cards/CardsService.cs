using Acquired.Models.Cards;
using Acquired.Models.Common;
using Acquired.Services.Http;
using Microsoft.Extensions.Logging;

namespace Acquired.Services.Cards;

public class CardsService : ICardsService
{
    private readonly IAcquiredHttpClient _client;
    private readonly ILogger<CardsService> _logger;

    public CardsService(IAcquiredHttpClient client, ILogger<CardsService> logger)
    {
        _client = client;
        _logger = logger;
    }

    public async Task<CardResponse> GetAsync(string cardId, CancellationToken ct = default)
    {
        return await _client.GetAsync<CardResponse>($"/v1/cards/{cardId}", ct: ct);
    }

    public async Task<AcquiredListResponse<CardResponse>> ListAsync(Dictionary<string, string?>? filters = null, CancellationToken ct = default)
    {
        return await _client.GetAsync<AcquiredListResponse<CardResponse>>("/v1/cards", filters, ct);
    }

    public async Task<AcquiredListResponse<CardResponse>> ListByCustomerAsync(string customerId, CancellationToken ct = default)
    {
        return await _client.GetAsync<AcquiredListResponse<CardResponse>>($"/v1/customers/{customerId}/cards", ct: ct);
    }

    public async Task<CardResponse> UpdateAsync(string cardId, UpdateCardRequest request, CancellationToken ct = default)
    {
        return await _client.SendAsync<CardResponse>(HttpMethod.Put, $"/v1/cards/{cardId}", request, ct);
    }
}
