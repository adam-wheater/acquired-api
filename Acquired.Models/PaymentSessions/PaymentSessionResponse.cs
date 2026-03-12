using Newtonsoft.Json;

namespace Acquired.Models.PaymentSessions;

public class PaymentSessionResponse
{
    [JsonProperty("session_id")]
    public string? SessionId { get; set; }
}
