using Acquired.Models.Common;
using Acquired.Models.DirectDebit;
using Acquired.Services.Http;
using Microsoft.Extensions.Logging;

namespace Acquired.Services.DirectDebit;

public class MandatesService : IMandatesService
{
    private readonly IAcquiredHttpClient _client;
    private readonly ILogger<MandatesService> _logger;

    public MandatesService(IAcquiredHttpClient client, ILogger<MandatesService> logger)
    {
        _client = client;
        _logger = logger;
    }

    public async Task<MandateResponse> CreateAsync(CreateMandateRequest request, CancellationToken ct = default)
    {
        _logger.LogInformation("Creating mandate for customer {CustomerId}", request.CustomerId);
        return await _client.SendAsync<MandateResponse>(HttpMethod.Post, "/v1/open-banking/mandates", request, ct);
    }

    public async Task<MandateResponse> GetAsync(string mandateId, CancellationToken ct = default)
    {
        return await _client.GetAsync<MandateResponse>($"/v1/open-banking/mandates/{mandateId}", ct: ct);
    }

    public async Task<AcquiredListResponse<MandateResponse>> ListAsync(Dictionary<string, string?>? filters = null, CancellationToken ct = default)
    {
        return await _client.GetAsync<AcquiredListResponse<MandateResponse>>("/v1/open-banking/mandates", filters, ct);
    }

    public async Task CancelAsync(string mandateId, CancellationToken ct = default)
    {
        _logger.LogInformation("Cancelling mandate {MandateId}", mandateId);
        await _client.SendWithoutResponseAsync(HttpMethod.Post, $"/v1/open-banking/mandates/{mandateId}/cancel", null, ct);
    }
}
