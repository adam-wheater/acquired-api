using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace Acquired.Models.Payments;

public class PaymentResponse
{
    [JsonProperty("transaction_id")]
    public string? TransactionId { get; set; }

    [JsonProperty("order_id")]
    public string? OrderId { get; set; }

    [JsonProperty("status")]
    public string? Status { get; set; }

    [JsonProperty("amount")]
    public decimal? Amount { get; set; }

    [JsonProperty("currency")]
    public string? Currency { get; set; }

    [JsonProperty("authorization_code")]
    public string? AuthorizationCode { get; set; }

    [JsonProperty("mid")]
    public string? Mid { get; set; }

    [JsonProperty("card_id")]
    public string? CardId { get; set; }

    [JsonProperty("customer_id")]
    public string? CustomerId { get; set; }

    [JsonProperty("tds")]
    public JObject? Tds { get; set; }

    [JsonProperty("created")]
    public string? Created { get; set; }
}
