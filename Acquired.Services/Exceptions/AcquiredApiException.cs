namespace Acquired.Services.Exceptions;

public class AcquiredApiException : AcquiredException
{
    public int AcquiredHttpStatusCode { get; set; }
    public string? ErrorType { get; set; }
    public string? Title { get; set; }
    public string? Instance { get; set; }

    public AcquiredApiException(string message) : base(message) { }

    public AcquiredApiException(string message, Exception innerException)
        : base(message, innerException) { }
}
