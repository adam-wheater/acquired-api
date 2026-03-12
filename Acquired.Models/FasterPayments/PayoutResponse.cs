using Newtonsoft.Json;

namespace Acquired.Models.FasterPayments;

public class PayoutResponse
{
    [JsonProperty("payout_id")]
    public string? PayoutId { get; set; }

    [JsonProperty("status")]
    public string? Status { get; set; }

    [JsonProperty("amount")]
    public decimal? Amount { get; set; }

    [JsonProperty("currency")]
    public string? Currency { get; set; }

    [JsonProperty("reference")]
    public string? Reference { get; set; }

    [JsonProperty("created")]
    public string? Created { get; set; }
}
