using Acquired.Models.Tools;

namespace Acquired.Services.Tools;

public interface IToolsService
{
    Task<ConfirmationOfPayeeResponse> ConfirmPayeeAsync(ConfirmationOfPayeeRequest request, CancellationToken ct = default);
}
