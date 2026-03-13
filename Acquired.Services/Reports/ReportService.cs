using Acquired.Models.Common;
using Acquired.Models.Reports;
using Acquired.Services.Http;

namespace Acquired.Services.Reports;

public class ReportService : IReportService
{
    private readonly IAcquiredHttpClient _httpClient;

    public ReportService(IAcquiredHttpClient httpClient)
    {
        _httpClient = httpClient;
    }

    public async Task<T> GetReconciliationAsync<T>(string reconciliationId)
    {
        return await _httpClient.GetAsync<T>($"/v1/reports/reconciliations/{reconciliationId}");
    }

    public async Task<PaginatedResponse<T>> ListReconciliationsAsync<T>(ReconQuery? query = null)
    {
        var queryParams = new Dictionary<string, string>();
        if (query?.Offset is not null) queryParams["offset"] = query.Offset.Value.ToString();
        if (query?.Limit is not null) queryParams["limit"] = query.Limit.Value.ToString();
        if (query?.Filter is not null) queryParams["filter"] = query.Filter;
        if (query?.DateFrom is not null) queryParams["date_from"] = query.DateFrom;
        if (query?.DateTo is not null) queryParams["date_to"] = query.DateTo;

        return await _httpClient.GetAsync<PaginatedResponse<T>>(
            "/v1/reports/reconciliations", queryParams.Count > 0 ? queryParams : null);
    }
}
