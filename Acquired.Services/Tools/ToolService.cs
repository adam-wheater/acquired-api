using Acquired.Services.Http;

namespace Acquired.Services.Tools;

public class ToolService : IToolService
{
    private readonly IAcquiredHttpClient _httpClient;

    public ToolService(IAcquiredHttpClient httpClient)
    {
        _httpClient = httpClient;
    }

    public async Task<T> ConfirmPayeeAsync<T>(object request)
    {
        return await _httpClient.PostAsync<T>("/v1/tools/confirmation-of-payee", request);
    }
}
