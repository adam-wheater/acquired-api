using Acquired.Models.Common;

namespace Acquired.Services.OpenBanking;

public interface IOpenBankingService
{
    Task<T> GetSupportedBanksAsync<T>();
    Task<T> CreateMandateAsync<T>(object request);
    Task<PaginatedResponse<T>> ListMandatesAsync<T>(PaginationQuery? query = null);
    Task<T> GetMandateAsync<T>(string mandateId);
    Task<T> CreateVrpAsync<T>(object request);
    Task<PaginatedResponse<T>> ListVrpsAsync<T>(PaginationQuery? query = null);
    Task<T> ConfirmFundsAsync<T>(string mandateId, object request);
}
