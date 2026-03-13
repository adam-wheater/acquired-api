using Newtonsoft.Json;

namespace Acquired.Models.FasterPayments;

public class PayoutRequest
{
    [JsonProperty("payee")]
    public PayoutPayee Payee { get; set; } = default!;

    [JsonProperty("transaction")]
    public PayoutTransaction Transaction { get; set; } = default!;

    [JsonProperty("reference", NullValueHandling = NullValueHandling.Ignore)]
    public string? Reference { get; set; }
}

public class PayoutPayee
{
    [JsonProperty("payee_id", NullValueHandling = NullValueHandling.Ignore)]
    public string? PayeeId { get; set; }

    [JsonProperty("account_number", NullValueHandling = NullValueHandling.Ignore)]
    public string? AccountNumber { get; set; }

    [JsonProperty("sort_code", NullValueHandling = NullValueHandling.Ignore)]
    public string? SortCode { get; set; }

    [JsonProperty("account_name", NullValueHandling = NullValueHandling.Ignore)]
    public string? AccountName { get; set; }
}

public class PayoutTransaction
{
    [JsonProperty("order_id", NullValueHandling = NullValueHandling.Ignore)]
    public string? OrderId { get; set; }

    [JsonProperty("amount")]
    public decimal Amount { get; set; }

    [JsonProperty("currency")]
    public string Currency { get; set; } = default!;

    [JsonProperty("custom_data", NullValueHandling = NullValueHandling.Ignore)]
    public string? CustomData { get; set; }
}
