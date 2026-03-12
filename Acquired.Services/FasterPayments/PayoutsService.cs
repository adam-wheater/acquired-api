using Acquired.Models.FasterPayments;
using Acquired.Services.Http;
using Microsoft.Extensions.Logging;

namespace Acquired.Services.FasterPayments;

public class PayoutsService : IPayoutsService
{
    private readonly IAcquiredHttpClient _client;
    private readonly ILogger<PayoutsService> _logger;

    public PayoutsService(IAcquiredHttpClient client, ILogger<PayoutsService> logger)
    {
        _client = client;
        _logger = logger;
    }

    public async Task<PayoutResponse> CreateAsync(CreatePayoutRequest request, CancellationToken ct = default)
    {
        _logger.LogInformation("Processing payout to payee {PayeeId}", request.PayeeId);
        return await _client.SendAsync<PayoutResponse>(HttpMethod.Post, "/v1/payouts", request, ct);
    }
}
