using Newtonsoft.Json;

namespace Acquired.Models.Transactions;

public class ReversalRequest
{
    [JsonProperty("amount", NullValueHandling = NullValueHandling.Ignore)]
    public decimal? Amount { get; set; }

    [JsonProperty("reason", NullValueHandling = NullValueHandling.Ignore)]
    public string? Reason { get; set; }

    [JsonProperty("custom_data", NullValueHandling = NullValueHandling.Ignore)]
    public string? CustomData { get; set; }

    [JsonProperty("custom1", NullValueHandling = NullValueHandling.Ignore)]
    public string? Custom1 { get; set; }

    [JsonProperty("custom2", NullValueHandling = NullValueHandling.Ignore)]
    public string? Custom2 { get; set; }
}
