namespace Acquired.Services.Tools;

public interface IToolService
{
    Task<T> ConfirmPayeeAsync<T>(object request);
}
