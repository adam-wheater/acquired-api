namespace Acquired.Services.Auth;

public interface IAcquiredTokenService
{
    Task<string> GetTokenAsync(CancellationToken ct = default);
}
