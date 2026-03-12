using Acquired.Models.PaymentSessions;

namespace Acquired.Services.PaymentSessions;

public interface IPaymentSessionsService
{
    Task<PaymentSessionResponse> CreateAsync(CreatePaymentSessionRequest request, CancellationToken ct = default);
}
