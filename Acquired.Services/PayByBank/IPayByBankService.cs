namespace Acquired.Services.PayByBank;

public interface IPayByBankService
{
    Task<T> GetAspspsAsync<T>();
    Task<T> CreateSingleImmediatePaymentAsync<T>(object request);
}
