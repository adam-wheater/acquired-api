using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations;

namespace Acquired.Models.PayByBank;

public class CreateSingleImmediatePaymentRequest
{
    [JsonProperty("aspsp_id")]
    [Required]
    public string AspspId { get; set; } = null!;

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

    [JsonProperty("reference")]
    public string? Reference { get; set; }

    [JsonProperty("redirect_url")]
    [Required]
    public string RedirectUrl { get; set; } = null!;

    [JsonProperty("webhook_url")]
    public string? WebhookUrl { get; set; }
}
