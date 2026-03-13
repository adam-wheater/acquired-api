using Newtonsoft.Json;
using Acquired.Models.Payments;

namespace Acquired.Models.PaymentLinks;

public class PaymentLinkRequest
{
    [JsonProperty("transaction")]
    public PaymentTransaction Transaction { get; set; } = default!;

    [JsonProperty("customer", NullValueHandling = NullValueHandling.Ignore)]
    public PaymentCustomer? Customer { get; set; }

    [JsonProperty("expire_at", NullValueHandling = NullValueHandling.Ignore)]
    public string? ExpireAt { get; set; }

    [JsonProperty("return_url", NullValueHandling = NullValueHandling.Ignore)]
    public string? ReturnUrl { get; set; }

    [JsonProperty("webhook_url", NullValueHandling = NullValueHandling.Ignore)]
    public string? WebhookUrl { get; set; }

    [JsonProperty("reference", NullValueHandling = NullValueHandling.Ignore)]
    public string? Reference { get; set; }

    [JsonProperty("payment_methods", NullValueHandling = NullValueHandling.Ignore)]
    public List<string>? PaymentMethods { get; set; }
}
