using Newtonsoft.Json;

namespace Acquired.Models.Common;

public class AcquiredErrorResponse
{
    [JsonProperty("status")]
    public string? Status { get; set; }

    [JsonProperty("error_type")]
    public string? ErrorType { get; set; }

    [JsonProperty("title")]
    public string? Title { get; set; }

    [JsonProperty("instance")]
    public string? Instance { get; set; }

    [JsonProperty("invalid_parameters", NullValueHandling = NullValueHandling.Ignore)]
    public List<InvalidParameter>? InvalidParameters { get; set; }
}

public class InvalidParameter
{
    [JsonProperty("parameter")]
    public string? Parameter { get; set; }

    [JsonProperty("reason")]
    public string? Reason { get; set; }
}
