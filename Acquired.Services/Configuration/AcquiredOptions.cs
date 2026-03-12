namespace Acquired.Services.Configuration;

public class AcquiredOptions
{
    public const string SectionName = "Acquired";

    public string BaseUrl { get; set; } = "https://test-api.acquired.com/v1";
    public string AppId { get; set; } = string.Empty;
    public string AppKey { get; set; } = string.Empty;
    public int TokenBufferSeconds { get; set; } = 60;
    public int TimeoutSeconds { get; set; } = 30;
}
