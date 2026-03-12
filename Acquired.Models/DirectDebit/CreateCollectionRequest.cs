using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations;

namespace Acquired.Models.DirectDebit;

public class CreateCollectionRequest
{
    [JsonProperty("mandate_id")]
    [Required]
    public string MandateId { get; set; } = null!;

    [JsonProperty("amount")]
    [Required]
    public decimal Amount { get; set; }

    [JsonProperty("currency")]
    [Required]
    public string Currency { get; set; } = null!;

    [JsonProperty("reference")]
    public string? Reference { get; set; }
}
