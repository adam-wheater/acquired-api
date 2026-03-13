using Newtonsoft.Json;
using Acquired.Models.Common;

namespace Acquired.Models.PaymentLinks;

public class PaymentLinkResponse
{
    [JsonProperty("link_id")]
    public string? LinkId { get; set; }

    [JsonProperty("status")]
    public string? Status { get; set; }

    [JsonProperty("url")]
    public string? Url { get; set; }

    [JsonProperty("links", NullValueHandling = NullValueHandling.Ignore)]
    public List<LinkModel>? Links { get; set; }

    [JsonProperty("created")]
    public string? Created { get; set; }

    [JsonProperty("expire_at")]
    public string? ExpireAt { get; set; }
}
