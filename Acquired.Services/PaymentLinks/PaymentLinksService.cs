using Acquired.Models.PaymentLinks;
using Acquired.Services.Http;
using Microsoft.Extensions.Logging;

namespace Acquired.Services.PaymentLinks;

public class PaymentLinksService : IPaymentLinksService
{
    private readonly IAcquiredHttpClient _client;
    private readonly ILogger<PaymentLinksService> _logger;

    public PaymentLinksService(IAcquiredHttpClient client, ILogger<PaymentLinksService> logger)
    {
        _client = client;
        _logger = logger;
    }

    public async Task<PaymentLinkResponse> CreateAsync(CreatePaymentLinkRequest request, CancellationToken ct = default)
    {
        _logger.LogInformation("Creating payment link for order {OrderId}", request.Transaction.OrderId);
        return await _client.SendAsync<PaymentLinkResponse>(HttpMethod.Post, "/v1/payment-links", request, ct);
    }

    public async Task SendAsync(string linkId, SendPaymentLinkRequest request, CancellationToken ct = default)
    {
        _logger.LogInformation("Sending payment link {LinkId}", linkId);
        await _client.SendWithoutResponseAsync(HttpMethod.Post, $"/v1/payment-links/{linkId}/send", request, ct);
    }
}
