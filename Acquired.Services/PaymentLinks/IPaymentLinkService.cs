namespace Acquired.Services.PaymentLinks;

public interface IPaymentLinkService
{
    Task<T> CreatePaymentLinkAsync<T>(object request);
    Task<T> SendPaymentLinkAsync<T>(string linkId, object? request = null);
}
