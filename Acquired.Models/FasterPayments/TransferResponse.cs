using Newtonsoft.Json;

namespace Acquired.Models.FasterPayments;

public class TransferResponse
{
    [JsonProperty("transfer_id")]
    public string? TransferId { get; set; }

    [JsonProperty("status")]
    public string? Status { get; set; }

    [JsonProperty("amount")]
    public decimal? Amount { get; set; }

    [JsonProperty("currency")]
    public string? Currency { get; set; }

    [JsonProperty("created")]
    public string? Created { get; set; }
}
