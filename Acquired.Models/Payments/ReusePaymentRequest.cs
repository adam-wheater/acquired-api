using Newtonsoft.Json;
using Acquired.Models.Common;

namespace Acquired.Models.Payments;

public class ReusePaymentRequest
{
    [JsonProperty("transaction")]
    public PaymentTransaction Transaction { get; set; } = default!;

    [JsonProperty("payment")]
    public ReusePaymentDetails Payment { get; set; } = default!;

    [JsonProperty("customer", NullValueHandling = NullValueHandling.Ignore)]
    public PaymentCustomer? Customer { get; set; }

    [JsonProperty("tds", NullValueHandling = NullValueHandling.Ignore)]
    public TdsModel? Tds { get; set; }
}

public class ReusePaymentDetails
{
    [JsonProperty("card_id")]
    public string CardId { get; set; } = default!;

    [JsonProperty("cvv", NullValueHandling = NullValueHandling.Ignore)]
    public string? Cvv { get; set; }

    [JsonProperty("reference", NullValueHandling = NullValueHandling.Ignore)]
    public string? Reference { get; set; }
}
