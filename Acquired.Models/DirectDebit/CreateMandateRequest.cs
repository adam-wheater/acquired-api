using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations;

namespace Acquired.Models.DirectDebit;

public class CreateMandateRequest
{
    [JsonProperty("customer_id")]
    [Required]
    public string CustomerId { get; set; } = null!;

    [JsonProperty("reference")]
    [Required]
    public string Reference { get; set; } = null!;

    [JsonProperty("redirect_url")]
    [Required]
    public string RedirectUrl { get; set; } = null!;

    [JsonProperty("webhook_url")]
    public string? WebhookUrl { get; set; }
}
