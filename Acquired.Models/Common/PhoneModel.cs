using Newtonsoft.Json;

namespace Acquired.Models.Common;

public class PhoneModel
{
    [JsonProperty("country_code", NullValueHandling = NullValueHandling.Ignore)]
    public string? CountryCode { get; set; }

    [JsonProperty("number", NullValueHandling = NullValueHandling.Ignore)]
    public string? Number { get; set; }
}
