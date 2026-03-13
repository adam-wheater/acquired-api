using Newtonsoft.Json;

namespace Acquired.Models.OpenBanking;

public class ConfirmFundsResponse
{
    [JsonProperty("funds_available")]
    public bool? FundsAvailable { get; set; }

    [JsonProperty("mandate_id")]
    public string? MandateId { get; set; }
}
