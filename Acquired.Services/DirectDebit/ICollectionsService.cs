using Acquired.Models.DirectDebit;

namespace Acquired.Services.DirectDebit;

public interface ICollectionsService
{
    Task<CollectionResponse> CreateAsync(CreateCollectionRequest request, CancellationToken ct = default);
    Task<CollectionResponse> GetAsync(string collectionId, CancellationToken ct = default);
    Task CancelAsync(string collectionId, CancellationToken ct = default);
}
