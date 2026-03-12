using Newtonsoft.Json;

namespace Acquired.Models.DirectDebit;

public class MandateResponse
{
    [JsonProperty("mandate_id")]
    public string? MandateId { get; set; }

    [JsonProperty("customer_id")]
    public string? CustomerId { get; set; }

    [JsonProperty("reference")]
    public string? Reference { get; set; }

    [JsonProperty("status")]
    public string? Status { get; set; }

    [JsonProperty("redirect_url")]
    public string? RedirectUrl { get; set; }

    [JsonProperty("created")]
    public string? Created { get; set; }
}
