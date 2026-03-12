using Acquired.Models.DirectDebit;
using Acquired.Services.Http;
using Microsoft.Extensions.Logging;

namespace Acquired.Services.DirectDebit;

public class CollectionsService : ICollectionsService
{
    private readonly IAcquiredHttpClient _client;
    private readonly ILogger<CollectionsService> _logger;

    public CollectionsService(IAcquiredHttpClient client, ILogger<CollectionsService> logger)
    {
        _client = client;
        _logger = logger;
    }

    public async Task<CollectionResponse> CreateAsync(CreateCollectionRequest request, CancellationToken ct = default)
    {
        _logger.LogInformation("Creating collection for mandate {MandateId}", request.MandateId);
        return await _client.SendAsync<CollectionResponse>(HttpMethod.Post, "/v1/open-banking/collections", request, ct);
    }

    public async Task<CollectionResponse> GetAsync(string collectionId, CancellationToken ct = default)
    {
        return await _client.GetAsync<CollectionResponse>($"/v1/open-banking/collections/{collectionId}", ct: ct);
    }

    public async Task CancelAsync(string collectionId, CancellationToken ct = default)
    {
        _logger.LogInformation("Cancelling collection {CollectionId}", collectionId);
        await _client.SendWithoutResponseAsync(HttpMethod.Post, $"/v1/open-banking/collections/{collectionId}/cancel", null, ct);
    }
}
