using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations;

namespace Acquired.Models.PaymentSessions;

public class CreatePaymentSessionRequest
{
    [JsonProperty("order_id")]
    [Required]
    [StringLength(50, MinimumLength = 2)]
    public string OrderId { get; set; } = null!;

    [JsonProperty("amount")]
    [Required]
    public decimal Amount { get; set; }

    [JsonProperty("currency")]
    [Required]
    public string Currency { get; set; } = null!;

    [JsonProperty("moto")]
    public bool? Moto { get; set; }

    [JsonProperty("capture")]
    public bool? Capture { get; set; }

    [JsonProperty("custom_data")]
    public string? CustomData { get; set; }
}
