using Newtonsoft.Json;

namespace Acquired.Models.Tools;

public class ConfirmationOfPayeeResponse
{
    [JsonProperty("result")]
    public string? Result { get; set; }

    [JsonProperty("matched_name")]
    public string? MatchedName { get; set; }

    [JsonProperty("reason_code")]
    public string? ReasonCode { get; set; }

    [JsonProperty("reason_description")]
    public string? ReasonDescription { get; set; }
}
