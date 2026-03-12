using Acquired.Models.Common;
using Acquired.Models.Customers;

namespace Acquired.Services.Customers;

public interface ICustomersService
{
    Task<CustomerResponse> CreateAsync(CreateCustomerRequest request, CancellationToken ct = default);
    Task<CustomerResponse> GetAsync(string customerId, CancellationToken ct = default);
    Task<AcquiredListResponse<CustomerResponse>> ListAsync(Dictionary<string, string?>? filters = null, CancellationToken ct = default);
    Task<CustomerResponse> UpdateAsync(string customerId, UpdateCustomerRequest request, CancellationToken ct = default);
}
