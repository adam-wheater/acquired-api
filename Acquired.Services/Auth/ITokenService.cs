namespace Acquired.Services.Auth;

public interface ITokenService
{
    Task<string> GetTokenAsync();
}
