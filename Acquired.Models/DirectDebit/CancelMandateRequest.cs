using Newtonsoft.Json;

namespace Acquired.Models.DirectDebit;

public class CancelMandateRequest
{
    [JsonProperty("reason", NullValueHandling = NullValueHandling.Ignore)]
    public string? Reason { get; set; }
}
