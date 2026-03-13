using Newtonsoft.Json;
using Acquired.Models.Common;

namespace Acquired.Models.PaymentSessions;

public class PaymentSessionResponse
{
    [JsonProperty("session_id")]
    public string? SessionId { get; set; }

    [JsonProperty("status")]
    public string? Status { get; set; }

    [JsonProperty("links", NullValueHandling = NullValueHandling.Ignore)]
    public List<LinkModel>? Links { get; set; }

    [JsonProperty("created")]
    public string? Created { get; set; }

    [JsonProperty("expire_at")]
    public string? ExpireAt { get; set; }
}
