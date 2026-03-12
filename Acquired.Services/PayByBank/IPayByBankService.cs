using Acquired.Models.Common;
using Acquired.Models.PayByBank;

namespace Acquired.Services.PayByBank;

public interface IPayByBankService
{
    Task<AcquiredListResponse<AspspResponse>> ListBanksAsync(CancellationToken ct = default);
    Task<SingleImmediatePaymentResponse> CreateSingleImmediatePaymentAsync(CreateSingleImmediatePaymentRequest request, CancellationToken ct = default);
}
