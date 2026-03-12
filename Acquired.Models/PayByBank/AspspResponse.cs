using Newtonsoft.Json;

namespace Acquired.Models.PayByBank;

public class AspspResponse
{
    [JsonProperty("aspsp_id")]
    public string? AspspId { get; set; }

    [JsonProperty("name")]
    public string? Name { get; set; }

    [JsonProperty("logo")]
    public string? Logo { get; set; }

    [JsonProperty("services")]
    public List<string>? Services { get; set; }
}
