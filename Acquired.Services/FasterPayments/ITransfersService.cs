using Acquired.Models.FasterPayments;

namespace Acquired.Services.FasterPayments;

public interface ITransfersService
{
    Task<TransferResponse> CreateAsync(CreateTransferRequest request, CancellationToken ct = default);
}
