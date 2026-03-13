using Newtonsoft.Json;

namespace Acquired.Models.OpenBanking;

public class CreateObMandateRequest
{
    [JsonProperty("customer_id", NullValueHandling = NullValueHandling.Ignore)]
    public string? CustomerId { get; set; }

    [JsonProperty("bank_id", NullValueHandling = NullValueHandling.Ignore)]
    public string? BankId { get; set; }

    [JsonProperty("mandate_type", NullValueHandling = NullValueHandling.Ignore)]
    public string? MandateType { get; set; }

    [JsonProperty("reference", NullValueHandling = NullValueHandling.Ignore)]
    public string? Reference { get; set; }

    [JsonProperty("return_url", NullValueHandling = NullValueHandling.Ignore)]
    public string? ReturnUrl { get; set; }

    [JsonProperty("webhook_url", NullValueHandling = NullValueHandling.Ignore)]
    public string? WebhookUrl { get; set; }

    [JsonProperty("max_amount", NullValueHandling = NullValueHandling.Ignore)]
    public decimal? MaxAmount { get; set; }

    [JsonProperty("currency", NullValueHandling = NullValueHandling.Ignore)]
    public string? Currency { get; set; }

    [JsonProperty("frequency", NullValueHandling = NullValueHandling.Ignore)]
    public string? Frequency { get; set; }

    [JsonProperty("valid_from", NullValueHandling = NullValueHandling.Ignore)]
    public string? ValidFrom { get; set; }

    [JsonProperty("valid_to", NullValueHandling = NullValueHandling.Ignore)]
    public string? ValidTo { get; set; }

    [JsonProperty("custom_data", NullValueHandling = NullValueHandling.Ignore)]
    public string? CustomData { get; set; }
}
