using Newtonsoft.Json;
using Acquired.Models.Common;

namespace Acquired.Models.Payments;

public class CreditPaymentRequest
{
    [JsonProperty("transaction")]
    public PaymentTransaction Transaction { get; set; } = default!;

    [JsonProperty("payment")]
    public CreditPaymentDetails Payment { get; set; } = default!;

    [JsonProperty("customer", NullValueHandling = NullValueHandling.Ignore)]
    public PaymentCustomer? Customer { get; set; }
}

public class CreditPaymentDetails
{
    [JsonProperty("card", NullValueHandling = NullValueHandling.Ignore)]
    public PaymentCard? Card { get; set; }

    [JsonProperty("card_id", NullValueHandling = NullValueHandling.Ignore)]
    public string? CardId { get; set; }

    [JsonProperty("cvv", NullValueHandling = NullValueHandling.Ignore)]
    public string? Cvv { get; set; }

    [JsonProperty("reference", NullValueHandling = NullValueHandling.Ignore)]
    public string? Reference { get; set; }
}
