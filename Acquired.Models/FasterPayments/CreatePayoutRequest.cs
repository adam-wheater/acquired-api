using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations;

namespace Acquired.Models.FasterPayments;

public class CreatePayoutRequest
{
    [JsonProperty("payee_id")]
    [Required]
    public string PayeeId { get; set; } = null!;

    [JsonProperty("amount")]
    [Required]
    public decimal Amount { get; set; }

    [JsonProperty("currency")]
    [Required]
    public string Currency { get; set; } = null!;

    [JsonProperty("reference")]
    public string? Reference { get; set; }
}
