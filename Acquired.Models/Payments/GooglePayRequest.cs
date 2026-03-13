using Newtonsoft.Json;
using Acquired.Models.Common;

namespace Acquired.Models.Payments;

public class GooglePayRequest
{
    [JsonProperty("transaction")]
    public PaymentTransaction Transaction { get; set; } = default!;

    [JsonProperty("payment")]
    public GooglePayPaymentDetails Payment { get; set; } = default!;

    [JsonProperty("customer", NullValueHandling = NullValueHandling.Ignore)]
    public PaymentCustomer? Customer { get; set; }

    [JsonProperty("tds", NullValueHandling = NullValueHandling.Ignore)]
    public TdsModel? Tds { get; set; }
}

public class GooglePayPaymentDetails
{
    [JsonProperty("google_pay")]
    public GooglePayToken GooglePay { get; set; } = default!;

    [JsonProperty("create_card", NullValueHandling = NullValueHandling.Ignore)]
    public bool? CreateCard { get; set; }

    [JsonProperty("reference", NullValueHandling = NullValueHandling.Ignore)]
    public string? Reference { get; set; }
}

public class GooglePayToken
{
    [JsonProperty("protocol_version", NullValueHandling = NullValueHandling.Ignore)]
    public string? ProtocolVersion { get; set; }

    [JsonProperty("signature", NullValueHandling = NullValueHandling.Ignore)]
    public string? Signature { get; set; }

    [JsonProperty("signed_message", NullValueHandling = NullValueHandling.Ignore)]
    public string? SignedMessage { get; set; }
}
