using Newtonsoft.Json;

namespace Acquired.Models.PaymentLinks;

public class PaymentLinkResponse
{
    [JsonProperty("link_id")]
    public string? LinkId { get; set; }

    [JsonProperty("link_url")]
    public string? LinkUrl { get; set; }

    [JsonProperty("status")]
    public string? Status { get; set; }

    [JsonProperty("created")]
    public string? Created { get; set; }
}
