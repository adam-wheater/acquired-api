using Acquired.Models.PaymentSessions;
using Acquired.Services.Http;
using Microsoft.Extensions.Logging;

namespace Acquired.Services.PaymentSessions;

public class PaymentSessionsService : IPaymentSessionsService
{
    private readonly IAcquiredHttpClient _client;
    private readonly ILogger<PaymentSessionsService> _logger;

    public PaymentSessionsService(IAcquiredHttpClient client, ILogger<PaymentSessionsService> logger)
    {
        _client = client;
        _logger = logger;
    }

    public async Task<PaymentSessionResponse> CreateAsync(CreatePaymentSessionRequest request, CancellationToken ct = default)
    {
        _logger.LogInformation("Creating payment session for order {OrderId}", request.OrderId);
        return await _client.SendAsync<PaymentSessionResponse>(HttpMethod.Post, "/v1/payment-sessions", request, ct);
    }
}
