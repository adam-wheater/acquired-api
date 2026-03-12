using Acquired.Models.Common;
using Acquired.Models.DirectDebit;

namespace Acquired.Services.DirectDebit;

public interface IMandatesService
{
    Task<MandateResponse> CreateAsync(CreateMandateRequest request, CancellationToken ct = default);
    Task<MandateResponse> GetAsync(string mandateId, CancellationToken ct = default);
    Task<AcquiredListResponse<MandateResponse>> ListAsync(Dictionary<string, string?>? filters = null, CancellationToken ct = default);
    Task CancelAsync(string mandateId, CancellationToken ct = default);
}
