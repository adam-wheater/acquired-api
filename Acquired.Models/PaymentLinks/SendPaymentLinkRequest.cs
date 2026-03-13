using Newtonsoft.Json;

namespace Acquired.Models.PaymentLinks;

public class SendPaymentLinkRequest
{
    [JsonProperty("email", NullValueHandling = NullValueHandling.Ignore)]
    public string? Email { get; set; }

    [JsonProperty("phone", NullValueHandling = NullValueHandling.Ignore)]
    public SendPaymentLinkPhone? Phone { get; set; }
}

public class SendPaymentLinkPhone
{
    [JsonProperty("country_code")]
    public string? CountryCode { get; set; }

    [JsonProperty("number")]
    public string? Number { get; set; }
}
