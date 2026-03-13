namespace Acquired.Services.FasterPayments;

public interface IFasterPaymentService
{
    Task<T> CreatePayeeAsync<T>(string customerId, object request);
    Task<T> ListPayeesAsync<T>();
    Task<T> CreatePayoutAsync<T>(object request);
    Task<T> CreateAccountAsync<T>(object request);
    Task<T> ListAccountsAsync<T>();
    Task<T> GetAccountByMidAsync<T>(string mid);
}
