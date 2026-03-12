using Acquired.Models.Common;
using Acquired.Models.FasterPayments;
using Acquired.Services.Http;
using Microsoft.Extensions.Logging;

namespace Acquired.Services.FasterPayments;

public class AccountsService : IAccountsService
{
    private readonly IAcquiredHttpClient _client;
    private readonly ILogger<AccountsService> _logger;

    public AccountsService(IAcquiredHttpClient client, ILogger<AccountsService> logger)
    {
        _client = client;
        _logger = logger;
    }

    public async Task<AccountResponse> CreateAsync(CreateAccountRequest request, CancellationToken ct = default)
    {
        _logger.LogInformation("Creating settlement account {AccountName}", request.AccountName);
        return await _client.SendAsync<AccountResponse>(HttpMethod.Post, "/v1/accounts", request, ct);
    }

    public async Task<AccountResponse> GetAsync(string mid, CancellationToken ct = default)
    {
        return await _client.GetAsync<AccountResponse>($"/v1/accounts/{mid}", ct: ct);
    }

    public async Task<AcquiredListResponse<AccountResponse>> ListAsync(Dictionary<string, string?>? filters = null, CancellationToken ct = default)
    {
        return await _client.GetAsync<AcquiredListResponse<AccountResponse>>("/v1/accounts", filters, ct);
    }
}
