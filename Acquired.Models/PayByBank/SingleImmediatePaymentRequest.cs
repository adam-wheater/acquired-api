using Newtonsoft.Json;
using Acquired.Models.Common;
using Acquired.Models.Payments;

namespace Acquired.Models.PayByBank;

public class SingleImmediatePaymentRequest
{
    [JsonProperty("transaction")]
    public PaymentTransaction Transaction { get; set; } = default!;

    [JsonProperty("payment")]
    public PayByBankPaymentDetails Payment { get; set; } = default!;

    [JsonProperty("customer", NullValueHandling = NullValueHandling.Ignore)]
    public PaymentCustomer? Customer { get; set; }
}

public class PayByBankPaymentDetails
{
    [JsonProperty("bank_id", NullValueHandling = NullValueHandling.Ignore)]
    public string? BankId { get; set; }

    [JsonProperty("return_url", NullValueHandling = NullValueHandling.Ignore)]
    public string? ReturnUrl { get; set; }

    [JsonProperty("webhook_url", NullValueHandling = NullValueHandling.Ignore)]
    public string? WebhookUrl { get; set; }

    [JsonProperty("reference", NullValueHandling = NullValueHandling.Ignore)]
    public string? Reference { get; set; }
}
