namespace Acquired.Services.Configuration;

public class AcquiredOptions
{
    public const string SectionName = "Acquired";
    public string BaseUrl { get; set; } = default!;
    public string AppId { get; set; } = default!;
    public string AppKey { get; set; } = default!;
    public int TokenBufferSeconds { get; set; } = 60;
    public int TimeoutSeconds { get; set; } = 30;
}
