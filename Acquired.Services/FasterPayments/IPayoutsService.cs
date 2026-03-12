using Acquired.Models.FasterPayments;

namespace Acquired.Services.FasterPayments;

public interface IPayoutsService
{
    Task<PayoutResponse> CreateAsync(CreatePayoutRequest request, CancellationToken ct = default);
}
