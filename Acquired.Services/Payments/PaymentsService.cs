using Acquired.Models.Payments;
using Acquired.Services.Http;
using Microsoft.Extensions.Logging;

namespace Acquired.Services.Payments;

public class PaymentsService : IPaymentsService
{
    private readonly IAcquiredHttpClient _client;
    private readonly ILogger<PaymentsService> _logger;

    public PaymentsService(IAcquiredHttpClient client, ILogger<PaymentsService> logger)
    {
        _client = client;
        _logger = logger;
    }

    public async Task<PaymentResponse> CreateAsync(CreatePaymentRequest request, CancellationToken ct = default)
    {
        _logger.LogInformation("Processing payment for order {OrderId}, amount {Amount} {Currency}",
            request.Transaction.OrderId, request.Transaction.Amount, request.Transaction.Currency);
        return await _client.SendAsync<PaymentResponse>(HttpMethod.Post, "/v1/payments", request, ct);
    }
}
