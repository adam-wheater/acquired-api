using Newtonsoft.Json;

namespace Acquired.Models.Transactions;

public class CaptureRequest
{
    [JsonProperty("amount")]
    public decimal Amount { get; set; }
}
