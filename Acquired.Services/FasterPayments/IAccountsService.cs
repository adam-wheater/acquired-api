using Acquired.Models.Common;
using Acquired.Models.FasterPayments;

namespace Acquired.Services.FasterPayments;

public interface IAccountsService
{
    Task<AccountResponse> CreateAsync(CreateAccountRequest request, CancellationToken ct = default);
    Task<AccountResponse> GetAsync(string mid, CancellationToken ct = default);
    Task<AcquiredListResponse<AccountResponse>> ListAsync(Dictionary<string, string?>? filters = null, CancellationToken ct = default);
}
