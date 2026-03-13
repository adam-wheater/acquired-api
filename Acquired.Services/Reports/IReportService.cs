using Acquired.Models.Common;
using Acquired.Models.Reports;

namespace Acquired.Services.Reports;

public interface IReportService
{
    Task<T> GetReconciliationAsync<T>(string reconciliationId);
    Task<PaginatedResponse<T>> ListReconciliationsAsync<T>(ReconQuery? query = null);
}
