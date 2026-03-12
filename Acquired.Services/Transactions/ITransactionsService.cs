using Acquired.Models.Common;
using Acquired.Models.Transactions;

namespace Acquired.Services.Transactions;

public interface ITransactionsService
{
    Task<TransactionResponse> GetAsync(string transactionId, CancellationToken ct = default);
    Task<AcquiredListResponse<TransactionResponse>> ListAsync(Dictionary<string, string?>? filters = null, CancellationToken ct = default);
    Task<TransactionResponse> RefundAsync(RefundRequest request, CancellationToken ct = default);
    Task<TransactionResponse> VoidAsync(VoidRequest request, CancellationToken ct = default);
    Task<TransactionResponse> CaptureAsync(CaptureRequest request, CancellationToken ct = default);
    Task<TransactionResponse> ReversalAsync(ReversalRequest request, CancellationToken ct = default);
}
