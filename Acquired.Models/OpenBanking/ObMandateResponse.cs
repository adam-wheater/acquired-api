using Newtonsoft.Json;
using Acquired.Models.Common;

namespace Acquired.Models.OpenBanking;

public class ObMandateResponse
{
    [JsonProperty("mandate_id")]
    public string? MandateId { get; set; }

    [JsonProperty("status")]
    public string? Status { get; set; }

    [JsonProperty("links", NullValueHandling = NullValueHandling.Ignore)]
    public List<LinkModel>? Links { get; set; }

    [JsonProperty("created")]
    public string? Created { get; set; }
}
