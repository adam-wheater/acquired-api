using Acquired.Models.Common;
using Acquired.Models.Customers;

namespace Acquired.Services.Customers;

public interface ICustomerService
{
    Task<CustomerResponse> GetByIdAsync(string customerId);
    Task<PaginatedResponse<CustomerResponse>> GetAllAsync(PaginationQuery? query = null);
    Task<CreateCustomerResponse> CreateAsync(CustomerRequest request);
    Task<CustomerResponse> UpdateAsync(string customerId, CustomerRequest request);
}
