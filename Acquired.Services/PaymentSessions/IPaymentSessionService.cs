namespace Acquired.Services.PaymentSessions;

public interface IPaymentSessionService
{
    Task<T> CreateSessionAsync<T>(object request);
    Task<T> UpdateSessionAsync<T>(string sessionId, object request);
}
