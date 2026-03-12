using Acquired.Models.Common;
using Acquired.Models.PayByBank;
using Acquired.Services.Http;
using Microsoft.Extensions.Logging;

namespace Acquired.Services.PayByBank;

public class PayByBankService : IPayByBankService
{
    private readonly IAcquiredHttpClient _client;
    private readonly ILogger<PayByBankService> _logger;

    public PayByBankService(IAcquiredHttpClient client, ILogger<PayByBankService> logger)
    {
        _client = client;
        _logger = logger;
    }

    public async Task<AcquiredListResponse<AspspResponse>> ListBanksAsync(CancellationToken ct = default)
    {
        return await _client.GetAsync<AcquiredListResponse<AspspResponse>>("/v1/aspsps", ct: ct);
    }

    public async Task<SingleImmediatePaymentResponse> CreateSingleImmediatePaymentAsync(CreateSingleImmediatePaymentRequest request, CancellationToken ct = default)
    {
        _logger.LogInformation("Creating single immediate payment for order {OrderId}", request.OrderId);
        return await _client.SendAsync<SingleImmediatePaymentResponse>(HttpMethod.Post, "/v1/single-immediate-payment", request, ct);
    }
}
