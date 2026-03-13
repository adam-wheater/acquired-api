using Newtonsoft.Json;

namespace Acquired.Models.OpenBanking;

public class ConfirmFundsRequest
{
    [JsonProperty("mandate_id")]
    public string MandateId { get; set; } = default!;

    [JsonProperty("amount")]
    public decimal Amount { get; set; }

    [JsonProperty("currency")]
    public string Currency { get; set; } = default!;
}
