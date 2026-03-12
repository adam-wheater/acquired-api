using Newtonsoft.Json;

namespace Acquired.Api.Models;

public class AcquiredErrorResponse
{
    [JsonProperty("status_code")]
    public int StatusCode { get; set; }

    [JsonProperty("error_code", NullValueHandling = NullValueHandling.Ignore)]
    public string? ErrorCode { get; set; }

    [JsonProperty("message")]
    public string? Message { get; set; }

    [JsonProperty("acquired_error_type", NullValueHandling = NullValueHandling.Ignore)]
    public string? AcquiredErrorType { get; set; }

    [JsonProperty("correlation_id", NullValueHandling = NullValueHandling.Ignore)]
    public string? CorrelationId { get; set; }
}
