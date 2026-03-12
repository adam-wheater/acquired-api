using Acquired.Models.FasterPayments;
using Acquired.Services.Http;
using Microsoft.Extensions.Logging;

namespace Acquired.Services.FasterPayments;

public class TransfersService : ITransfersService
{
    private readonly IAcquiredHttpClient _client;
    private readonly ILogger<TransfersService> _logger;

    public TransfersService(IAcquiredHttpClient client, ILogger<TransfersService> logger)
    {
        _client = client;
        _logger = logger;
    }

    public async Task<TransferResponse> CreateAsync(CreateTransferRequest request, CancellationToken ct = default)
    {
        _logger.LogInformation("Creating transfer from {Source} to {Destination}", request.SourceMid, request.DestinationMid);
        return await _client.SendAsync<TransferResponse>(HttpMethod.Post, "/v1/transfers", request, ct);
    }
}
