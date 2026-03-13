using Newtonsoft.Json;
using Acquired.Models.Common;

namespace Acquired.Models.Payments;

public class RecurringPaymentRequest
{
    [JsonProperty("transaction")]
    public PaymentTransaction Transaction { get; set; } = default!;

    [JsonProperty("payment")]
    public RecurringPaymentDetails Payment { get; set; } = default!;

    [JsonProperty("customer", NullValueHandling = NullValueHandling.Ignore)]
    public PaymentCustomer? Customer { get; set; }

    [JsonProperty("tds", NullValueHandling = NullValueHandling.Ignore)]
    public TdsModel? Tds { get; set; }
}

public class RecurringPaymentDetails
{
    [JsonProperty("card", NullValueHandling = NullValueHandling.Ignore)]
    public PaymentCard? Card { get; set; }

    [JsonProperty("card_id", NullValueHandling = NullValueHandling.Ignore)]
    public string? CardId { get; set; }

    [JsonProperty("cvv", NullValueHandling = NullValueHandling.Ignore)]
    public string? Cvv { get; set; }

    [JsonProperty("create_card", NullValueHandling = NullValueHandling.Ignore)]
    public bool? CreateCard { get; set; }

    [JsonProperty("reference", NullValueHandling = NullValueHandling.Ignore)]
    public string? Reference { get; set; }
}
