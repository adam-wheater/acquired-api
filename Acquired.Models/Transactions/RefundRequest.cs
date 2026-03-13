using Newtonsoft.Json;

namespace Acquired.Models.Transactions;

public class RefundRequest
{
    [JsonProperty("amount")]
    public decimal Amount { get; set; }

    [JsonProperty("reason", NullValueHandling = NullValueHandling.Ignore)]
    public string? Reason { get; set; }

    [JsonProperty("metadata", NullValueHandling = NullValueHandling.Ignore)]
    public Dictionary<string, string>? Metadata { get; set; }
}
