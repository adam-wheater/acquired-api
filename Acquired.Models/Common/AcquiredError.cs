using Newtonsoft.Json;

namespace Acquired.Models.Common;

public class AcquiredError
{
    [JsonProperty("status")]
    public string? Status { get; set; }

    [JsonProperty("error_type")]
    public string? ErrorType { get; set; }

    [JsonProperty("title")]
    public string? Title { get; set; }

    [JsonProperty("instance")]
    public string? Instance { get; set; }

    [JsonProperty("invalid_parameters")]
    public List<AcquiredInvalidParameter>? InvalidParameters { get; set; }
}

public class AcquiredInvalidParameter
{
    [JsonProperty("parameter")]
    public string? Parameter { get; set; }

    [JsonProperty("reason")]
    public string? Reason { get; set; }
}
