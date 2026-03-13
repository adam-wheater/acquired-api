using Acquired.Models.Common;
using Acquired.Models.Customers;
using Acquired.Services.Http;

namespace Acquired.Services.Customers;

public class CustomerService : ICustomerService
{
    private readonly IAcquiredHttpClient _httpClient;

    public CustomerService(IAcquiredHttpClient httpClient)
    {
        _httpClient = httpClient;
    }

    public async Task<CustomerResponse> GetByIdAsync(string customerId)
    {
        return await _httpClient.GetAsync<CustomerResponse>($"/v1/customers/{customerId}");
    }

    public async Task<PaginatedResponse<CustomerResponse>> GetAllAsync(PaginationQuery? query = null)
    {
        var queryParams = new Dictionary<string, string>();
        if (query?.Offset is not null) queryParams["offset"] = query.Offset.Value.ToString();
        if (query?.Limit is not null) queryParams["limit"] = query.Limit.Value.ToString();
        if (query?.Filter is not null) queryParams["filter"] = query.Filter;

        return await _httpClient.GetAsync<PaginatedResponse<CustomerResponse>>(
            "/v1/customers", queryParams.Count > 0 ? queryParams : null);
    }

    public async Task<CreateCustomerResponse> CreateAsync(CustomerRequest request)
    {
        return await _httpClient.PostAsync<CreateCustomerResponse>("/v1/customers", request);
    }

    public async Task<CustomerResponse> UpdateAsync(string customerId, CustomerRequest request)
    {
        return await _httpClient.PutAsync<CustomerResponse>($"/v1/customers/{customerId}", request);
    }
}
