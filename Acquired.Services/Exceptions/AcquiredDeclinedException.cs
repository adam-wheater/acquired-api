namespace Acquired.Services.Exceptions;

public class AcquiredDeclinedException : AcquiredException
{
    public string? Status { get; set; }
    public string? DeclineReason { get; set; }
    public string? TransactionId { get; set; }

    public AcquiredDeclinedException(string message) : base(message) { }

    public AcquiredDeclinedException(string message, Exception innerException)
        : base(message, innerException) { }
}
