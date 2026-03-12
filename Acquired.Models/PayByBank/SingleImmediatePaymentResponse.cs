using Newtonsoft.Json;

namespace Acquired.Models.PayByBank;

public class SingleImmediatePaymentResponse
{
    [JsonProperty("transaction_id")]
    public string? TransactionId { get; set; }

    [JsonProperty("order_id")]
    public string? OrderId { get; set; }

    [JsonProperty("status")]
    public string? Status { get; set; }

    [JsonProperty("redirect_url")]
    public string? RedirectUrl { get; set; }

    [JsonProperty("created")]
    public string? Created { get; set; }
}
