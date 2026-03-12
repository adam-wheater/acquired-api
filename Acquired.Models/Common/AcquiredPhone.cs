using Newtonsoft.Json;

namespace Acquired.Models.Common;

public class AcquiredPhone
{
    [JsonProperty("country_code")]
    public string? CountryCode { get; set; }

    [JsonProperty("number")]
    public string? Number { get; set; }
}
