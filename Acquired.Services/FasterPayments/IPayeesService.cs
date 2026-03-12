using Acquired.Models.Common;
using Acquired.Models.FasterPayments;

namespace Acquired.Services.FasterPayments;

public interface IPayeesService
{
    Task<PayeeResponse> CreateAsync(string customerId, CreatePayeeRequest request, CancellationToken ct = default);
    Task<AcquiredListResponse<PayeeResponse>> ListAsync(Dictionary<string, string?>? filters = null, CancellationToken ct = default);
}
