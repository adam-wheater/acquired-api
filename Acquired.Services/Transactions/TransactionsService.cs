using Acquired.Models.Common;
using Acquired.Models.Transactions;
using Acquired.Services.Http;
using Microsoft.Extensions.Logging;

namespace Acquired.Services.Transactions;

public class TransactionsService : ITransactionsService
{
    private readonly IAcquiredHttpClient _client;
    private readonly ILogger<TransactionsService> _logger;

    public TransactionsService(IAcquiredHttpClient client, ILogger<TransactionsService> logger)
    {
        _client = client;
        _logger = logger;
    }

    public async Task<TransactionResponse> GetAsync(string transactionId, CancellationToken ct = default)
    {
        return await _client.GetAsync<TransactionResponse>($"/v1/transactions/{transactionId}", ct: ct);
    }

    public async Task<AcquiredListResponse<TransactionResponse>> ListAsync(Dictionary<string, string?>? filters = null, CancellationToken ct = default)
    {
        return await _client.GetAsync<AcquiredListResponse<TransactionResponse>>("/v1/transactions", filters, ct);
    }

    public async Task<TransactionResponse> RefundAsync(RefundRequest request, CancellationToken ct = default)
    {
        _logger.LogInformation("Refunding transaction {TransactionId}", request.TransactionId);
        return await _client.SendAsync<TransactionResponse>(HttpMethod.Post, "/v1/transactions/refund", request, ct);
    }

    public async Task<TransactionResponse> VoidAsync(VoidRequest request, CancellationToken ct = default)
    {
        _logger.LogInformation("Voiding transaction {TransactionId}", request.TransactionId);
        return await _client.SendAsync<TransactionResponse>(HttpMethod.Post, "/v1/transactions/void", request, ct);
    }

    public async Task<TransactionResponse> CaptureAsync(CaptureRequest request, CancellationToken ct = default)
    {
        _logger.LogInformation("Capturing transaction {TransactionId}", request.TransactionId);
        return await _client.SendAsync<TransactionResponse>(HttpMethod.Post, "/v1/transactions/capture", request, ct);
    }

    public async Task<TransactionResponse> ReversalAsync(ReversalRequest request, CancellationToken ct = default)
    {
        _logger.LogInformation("Reversing transaction {TransactionId}", request.TransactionId);
        return await _client.SendAsync<TransactionResponse>(HttpMethod.Post, "/v1/transactions/reversal", request, ct);
    }
}
