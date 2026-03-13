using Newtonsoft.Json;

namespace Acquired.Models.Transactions;

public class RetryRequest
{
    [JsonProperty("custom_data", NullValueHandling = NullValueHandling.Ignore)]
    public string? CustomData { get; set; }
}
