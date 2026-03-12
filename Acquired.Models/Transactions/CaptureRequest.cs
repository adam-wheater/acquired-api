using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations;

namespace Acquired.Models.Transactions;

public class CaptureRequest
{
    [JsonProperty("transaction_id")]
    [Required]
    public string TransactionId { get; set; } = null!;

    [JsonProperty("amount")]
    public decimal? Amount { get; set; }

    [JsonProperty("reference")]
    public string? Reference { get; set; }
}
