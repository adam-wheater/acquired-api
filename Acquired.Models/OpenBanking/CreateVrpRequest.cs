using Newtonsoft.Json;

namespace Acquired.Models.OpenBanking;

public class CreateVrpRequest
{
    [JsonProperty("mandate_id")]
    public string MandateId { get; set; } = default!;

    [JsonProperty("amount")]
    public decimal Amount { get; set; }

    [JsonProperty("currency")]
    public string Currency { get; set; } = default!;

    [JsonProperty("reference", NullValueHandling = NullValueHandling.Ignore)]
    public string? Reference { get; set; }

    [JsonProperty("order_id", NullValueHandling = NullValueHandling.Ignore)]
    public string? OrderId { get; set; }

    [JsonProperty("webhook_url", NullValueHandling = NullValueHandling.Ignore)]
    public string? WebhookUrl { get; set; }

    [JsonProperty("custom_data", NullValueHandling = NullValueHandling.Ignore)]
    public string? CustomData { get; set; }
}
