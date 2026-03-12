using Acquired.Models.Common;
using Acquired.Models.Reports;

namespace Acquired.Services.Reports;

public interface IReportsService
{
    Task<AcquiredListResponse<ReconciliationResponse>> ListReconciliationsAsync(Dictionary<string, string?>? filters = null, CancellationToken ct = default);
    Task<ReconciliationResponse> GetReconciliationAsync(string id, CancellationToken ct = default);
}
