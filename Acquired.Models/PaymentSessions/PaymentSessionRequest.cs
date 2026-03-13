using Newtonsoft.Json;
using Acquired.Models.Common;
using Acquired.Models.Payments;

namespace Acquired.Models.PaymentSessions;

public class PaymentSessionRequest
{
    [JsonProperty("transaction")]
    public PaymentTransaction Transaction { get; set; } = default!;

    [JsonProperty("customer", NullValueHandling = NullValueHandling.Ignore)]
    public PaymentCustomer? Customer { get; set; }

    [JsonProperty("tds", NullValueHandling = NullValueHandling.Ignore)]
    public TdsModel? Tds { get; set; }

    [JsonProperty("payment_methods", NullValueHandling = NullValueHandling.Ignore)]
    public List<string>? PaymentMethods { get; set; }

    [JsonProperty("is_dynamic_descriptor", NullValueHandling = NullValueHandling.Ignore)]
    public bool? IsDynamicDescriptor { get; set; }

    [JsonProperty("webhook_url", NullValueHandling = NullValueHandling.Ignore)]
    public string? WebhookUrl { get; set; }

    [JsonProperty("redirect_url", NullValueHandling = NullValueHandling.Ignore)]
    public string? RedirectUrl { get; set; }

    [JsonProperty("expire_at", NullValueHandling = NullValueHandling.Ignore)]
    public string? ExpireAt { get; set; }
}
