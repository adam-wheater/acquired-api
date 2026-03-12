using Acquired.Models.Common;
using Acquired.Models.Customers;
using Acquired.Services.Http;
using Microsoft.Extensions.Logging;

namespace Acquired.Services.Customers;

public class CustomersService : ICustomersService
{
    private readonly IAcquiredHttpClient _client;
    private readonly ILogger<CustomersService> _logger;

    public CustomersService(IAcquiredHttpClient client, ILogger<CustomersService> logger)
    {
        _client = client;
        _logger = logger;
    }

    public async Task<CustomerResponse> CreateAsync(CreateCustomerRequest request, CancellationToken ct = default)
    {
        _logger.LogInformation("Creating customer with reference {Reference}", request.Reference);
        return await _client.SendAsync<CustomerResponse>(HttpMethod.Post, "/v1/customers", request, ct);
    }

    public async Task<CustomerResponse> GetAsync(string customerId, CancellationToken ct = default)
    {
        return await _client.GetAsync<CustomerResponse>($"/v1/customers/{customerId}", ct: ct);
    }

    public async Task<AcquiredListResponse<CustomerResponse>> ListAsync(Dictionary<string, string?>? filters = null, CancellationToken ct = default)
    {
        return await _client.GetAsync<AcquiredListResponse<CustomerResponse>>("/v1/customers", filters, ct);
    }

    public async Task<CustomerResponse> UpdateAsync(string customerId, UpdateCustomerRequest request, CancellationToken ct = default)
    {
        return await _client.SendAsync<CustomerResponse>(HttpMethod.Put, $"/v1/customers/{customerId}", request, ct);
    }
}
