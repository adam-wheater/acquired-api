using Newtonsoft.Json;

namespace Acquired.Models.Common;

public class AcquiredAddress
{
    [JsonProperty("line_1")]
    public string? Line1 { get; set; }

    [JsonProperty("line_2")]
    public string? Line2 { get; set; }

    [JsonProperty("city")]
    public string? City { get; set; }

    [JsonProperty("state")]
    public string? State { get; set; }

    [JsonProperty("postcode")]
    public string? Postcode { get; set; }

    [JsonProperty("country_code")]
    public string? CountryCode { get; set; }
}
