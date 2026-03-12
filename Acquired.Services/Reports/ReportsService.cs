using Acquired.Models.Common;
using Acquired.Models.Reports;
using Acquired.Services.Http;
using Microsoft.Extensions.Logging;

namespace Acquired.Services.Reports;

public class ReportsService : IReportsService
{
    private readonly IAcquiredHttpClient _client;
    private readonly ILogger<ReportsService> _logger;

    public ReportsService(IAcquiredHttpClient client, ILogger<ReportsService> logger)
    {
        _client = client;
        _logger = logger;
    }

    public async Task<AcquiredListResponse<ReconciliationResponse>> ListReconciliationsAsync(Dictionary<string, string?>? filters = null, CancellationToken ct = default)
    {
        return await _client.GetAsync<AcquiredListResponse<ReconciliationResponse>>("/v1/reports/reconciliations", filters, ct);
    }

    public async Task<ReconciliationResponse> GetReconciliationAsync(string id, CancellationToken ct = default)
    {
        return await _client.GetAsync<ReconciliationResponse>($"/v1/reports/reconciliations/{id}", ct: ct);
    }
}
