using Newtonsoft.Json;

namespace Acquired.Models.Common;

public class LinkModel
{
    [JsonProperty("rel")]
    public string? Rel { get; set; }

    [JsonProperty("href")]
    public string? Href { get; set; }
}
