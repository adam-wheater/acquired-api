using Newtonsoft.Json;

namespace Acquired.Models.Tools;

public class ConfirmationOfPayeeResponse
{
    [JsonProperty("match")]
    public bool? Match { get; set; }

    [JsonProperty("name_match")]
    public string? NameMatch { get; set; }

    [JsonProperty("reason_code")]
    public string? ReasonCode { get; set; }

    [JsonProperty("reason")]
    public string? Reason { get; set; }
}
