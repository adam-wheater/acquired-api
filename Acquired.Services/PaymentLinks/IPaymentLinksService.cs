using Acquired.Models.PaymentLinks;

namespace Acquired.Services.PaymentLinks;

public interface IPaymentLinksService
{
    Task<PaymentLinkResponse> CreateAsync(CreatePaymentLinkRequest request, CancellationToken ct = default);
    Task SendAsync(string linkId, SendPaymentLinkRequest request, CancellationToken ct = default);
}
