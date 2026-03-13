namespace Acquired.Services.Payments;

public interface IPaymentService
{
    Task<T> CreatePaymentAsync<T>(object request);
    Task<T> CreateReusePaymentAsync<T>(object request);
    Task<T> CreateApplePayAsync<T>(object request);
    Task<T> CreateGooglePayAsync<T>(object request);
    Task<T> CreateRecurringAsync<T>(object request);
    Task<T> CreateCreditAsync<T>(object request);
    Task<T> CreateInternalTransferAsync<T>(object request);
    Task<T> CreateCollectionAsync<T>(object request);
}
