using Newtonsoft.Json;

namespace Acquired.Models.Common;

public class AddressModel
{
    [JsonProperty("line_1", NullValueHandling = NullValueHandling.Ignore)]
    public string? Line1 { get; set; }

    [JsonProperty("line_2", NullValueHandling = NullValueHandling.Ignore)]
    public string? Line2 { get; set; }

    [JsonProperty("city", NullValueHandling = NullValueHandling.Ignore)]
    public string? City { get; set; }

    [JsonProperty("state", NullValueHandling = NullValueHandling.Ignore)]
    public string? State { get; set; }

    [JsonProperty("postcode", NullValueHandling = NullValueHandling.Ignore)]
    public string? Postcode { get; set; }

    [JsonProperty("country_code", NullValueHandling = NullValueHandling.Ignore)]
    public string? CountryCode { get; set; }
}
