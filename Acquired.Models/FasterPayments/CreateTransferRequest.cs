using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations;

namespace Acquired.Models.FasterPayments;

public class CreateTransferRequest
{
    [JsonProperty("source_mid")]
    [Required]
    public string SourceMid { get; set; } = null!;

    [JsonProperty("destination_mid")]
    [Required]
    public string DestinationMid { get; set; } = null!;

    [JsonProperty("amount")]
    [Required]
    public decimal Amount { get; set; }

    [JsonProperty("currency")]
    [Required]
    public string Currency { get; set; } = null!;

    [JsonProperty("reference")]
    public string? Reference { get; set; }
}
