using Acquired.Models.Cards;
using Acquired.Models.Common;

namespace Acquired.Services.Cards;

public interface ICardsService
{
    Task<CardResponse> GetAsync(string cardId, CancellationToken ct = default);
    Task<AcquiredListResponse<CardResponse>> ListAsync(Dictionary<string, string?>? filters = null, CancellationToken ct = default);
    Task<AcquiredListResponse<CardResponse>> ListByCustomerAsync(string customerId, CancellationToken ct = default);
    Task<CardResponse> UpdateAsync(string cardId, UpdateCardRequest request, CancellationToken ct = default);
}
