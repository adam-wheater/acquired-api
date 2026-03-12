using Acquired.Models.Payments;

namespace Acquired.Services.Payments;

public interface IPaymentsService
{
    Task<PaymentResponse> CreateAsync(CreatePaymentRequest request, CancellationToken ct = default);
}
