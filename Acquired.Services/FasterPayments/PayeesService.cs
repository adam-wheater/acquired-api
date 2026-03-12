using Acquired.Models.Common;
using Acquired.Models.FasterPayments;
using Acquired.Services.Http;
using Microsoft.Extensions.Logging;

namespace Acquired.Services.FasterPayments;

public class PayeesService : IPayeesService
{
    private readonly IAcquiredHttpClient _client;
    private readonly ILogger<PayeesService> _logger;

    public PayeesService(IAcquiredHttpClient client, ILogger<PayeesService> logger)
    {
        _client = client;
        _logger = logger;
    }

    public async Task<PayeeResponse> CreateAsync(string customerId, CreatePayeeRequest request, CancellationToken ct = default)
    {
        _logger.LogInformation("Creating payee for customer {CustomerId}", customerId);
        return await _client.SendAsync<PayeeResponse>(HttpMethod.Post, $"/v1/customers/{customerId}/payees", request, ct);
    }

    public async Task<AcquiredListResponse<PayeeResponse>> ListAsync(Dictionary<string, string?>? filters = null, CancellationToken ct = default)
    {
        return await _client.GetAsync<AcquiredListResponse<PayeeResponse>>("/v1/payees", filters, ct);
    }
}
