using Newtonsoft.Json;

namespace Acquired.Models.Payments;

public class InternalTransferRequest
{
    [JsonProperty("transaction")]
    public InternalTransferTransaction Transaction { get; set; } = default!;

    [JsonProperty("payment")]
    public InternalTransferPayment Payment { get; set; } = default!;
}

public class InternalTransferTransaction
{
    [JsonProperty("order_id", NullValueHandling = NullValueHandling.Ignore)]
    public string? OrderId { get; set; }

    [JsonProperty("amount")]
    public decimal Amount { get; set; }

    [JsonProperty("currency")]
    public string Currency { get; set; } = default!;

    [JsonProperty("custom_data", NullValueHandling = NullValueHandling.Ignore)]
    public string? CustomData { get; set; }

    [JsonProperty("custom1", NullValueHandling = NullValueHandling.Ignore)]
    public string? Custom1 { get; set; }

    [JsonProperty("custom2", NullValueHandling = NullValueHandling.Ignore)]
    public string? Custom2 { get; set; }
}

public class InternalTransferPayment
{
    [JsonProperty("reference", NullValueHandling = NullValueHandling.Ignore)]
    public string? Reference { get; set; }
}
