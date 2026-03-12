using Newtonsoft.Json;

namespace Acquired.Models.PaymentLinks;

public class SendPaymentLinkRequest
{
    [JsonProperty("email")]
    public string? Email { get; set; }

    [JsonProperty("sms")]
    public string? Sms { get; set; }
}
