namespace Acquired.Services.Exceptions;

public abstract class AcquiredException : Exception
{
    public string? CorrelationId { get; set; }

    protected AcquiredException(string message) : base(message) { }

    protected AcquiredException(string message, Exception innerException)
        : base(message, innerException) { }
}
