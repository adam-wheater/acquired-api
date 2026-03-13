using Acquired.Models.Cards;
using Acquired.Models.Common;

namespace Acquired.Services.Cards;

public interface ICardService
{
    Task<CardResponse> GetByIdAsync(string cardId);
    Task<PaginatedResponse<CardResponse>> GetAllAsync(PaginationQuery? query = null);
    Task<PaginatedResponse<CardResponse>> GetByCustomerIdAsync(string customerId, PaginationQuery? query = null);
    Task<CardResponse> UpdateAsync(string cardId, UpdateCardRequest request);
}
