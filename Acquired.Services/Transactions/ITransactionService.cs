using Acquired.Models.Common;

namespace Acquired.Services.Transactions;

public interface ITransactionService
{
    Task<T> GetByIdAsync<T>(string transactionId);
    Task<PaginatedResponse<T>> GetAllAsync<T>(PaginationQuery? query = null);
    Task<T> RefundAsync<T>(string transactionId, object request);
    Task<T> VoidAsync<T>(string transactionId);
    Task<T> CaptureAsync<T>(string transactionId, object request);
    Task<T> ReversalAsync<T>(string transactionId, object? request = null);
    Task<T> CancelAsync<T>(string transactionId);
    Task<T> RetryAsync<T>(string transactionId, object? request = null);
}
