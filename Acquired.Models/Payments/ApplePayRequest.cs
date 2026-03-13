using Newtonsoft.Json;
using Acquired.Models.Common;

namespace Acquired.Models.Payments;

public class ApplePayRequest
{
    [JsonProperty("transaction")]
    public PaymentTransaction Transaction { get; set; } = default!;

    [JsonProperty("payment")]
    public ApplePayPaymentDetails Payment { get; set; } = default!;

    [JsonProperty("customer", NullValueHandling = NullValueHandling.Ignore)]
    public PaymentCustomer? Customer { get; set; }

    [JsonProperty("tds", NullValueHandling = NullValueHandling.Ignore)]
    public TdsModel? Tds { get; set; }
}

public class ApplePayPaymentDetails
{
    [JsonProperty("apple_pay")]
    public ApplePayToken ApplePay { get; set; } = default!;

    [JsonProperty("create_card", NullValueHandling = NullValueHandling.Ignore)]
    public bool? CreateCard { get; set; }

    [JsonProperty("reference", NullValueHandling = NullValueHandling.Ignore)]
    public string? Reference { get; set; }
}

public class ApplePayToken
{
    [JsonProperty("version", NullValueHandling = NullValueHandling.Ignore)]
    public string? Version { get; set; }

    [JsonProperty("data", NullValueHandling = NullValueHandling.Ignore)]
    public string? Data { get; set; }

    [JsonProperty("signature", NullValueHandling = NullValueHandling.Ignore)]
    public string? Signature { get; set; }

    [JsonProperty("header", NullValueHandling = NullValueHandling.Ignore)]
    public ApplePayHeader? Header { get; set; }
}

public class ApplePayHeader
{
    [JsonProperty("ephemeral_public_key", NullValueHandling = NullValueHandling.Ignore)]
    public string? EphemeralPublicKey { get; set; }

    [JsonProperty("public_key_hash", NullValueHandling = NullValueHandling.Ignore)]
    public string? PublicKeyHash { get; set; }

    [JsonProperty("transaction_id", NullValueHandling = NullValueHandling.Ignore)]
    public string? TransactionId { get; set; }
}
