using Newtonsoft.Json;

namespace Acquired.Models.Payments;

public class CollectionRequest
{
    [JsonProperty("mandate_id")]
    public string MandateId { get; set; } = default!;

    [JsonProperty("amount")]
    public decimal Amount { get; set; }

    [JsonProperty("currency")]
    public string Currency { get; set; } = default!;

    [JsonProperty("reference", NullValueHandling = NullValueHandling.Ignore)]
    public string? Reference { get; set; }

    [JsonProperty("custom_data", NullValueHandling = NullValueHandling.Ignore)]
    public string? CustomData { get; set; }
}
