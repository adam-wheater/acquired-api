namespace Acquired.Services.DirectDebit;

public interface IDirectDebitService
{
    Task<T> CreateMandateAsync<T>(object request);
    Task<T> GetMandateAsync<T>(string mandateId);
    Task<T> CancelMandateAsync<T>(string mandateId);
}
