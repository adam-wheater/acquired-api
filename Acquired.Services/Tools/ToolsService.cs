using Acquired.Models.Tools;
using Acquired.Services.Http;
using Microsoft.Extensions.Logging;

namespace Acquired.Services.Tools;

public class ToolsService : IToolsService
{
    private readonly IAcquiredHttpClient _client;
    private readonly ILogger<ToolsService> _logger;

    public ToolsService(IAcquiredHttpClient client, ILogger<ToolsService> logger)
    {
        _client = client;
        _logger = logger;
    }

    public async Task<ConfirmationOfPayeeResponse> ConfirmPayeeAsync(ConfirmationOfPayeeRequest request, CancellationToken ct = default)
    {
        _logger.LogInformation("Confirming payee {Name}", request.Name);
        return await _client.SendAsync<ConfirmationOfPayeeResponse>(HttpMethod.Post, "/v1/tools/confirmation-of-payee", request, ct);
    }
}
