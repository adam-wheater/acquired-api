using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace Acquired.Models.Transactions;

public class TransactionResponse
{
    [JsonProperty("transaction_id")]
    public string? TransactionId { get; set; }

    [JsonProperty("order_id")]
    public string? OrderId { get; set; }

    [JsonProperty("transaction_type")]
    public string? TransactionType { get; set; }

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

    [JsonProperty("card")]
    public JObject? Card { get; set; }

    [JsonProperty("customer")]
    public JObject? Customer { get; set; }

    [JsonProperty("tds")]
    public JObject? Tds { get; set; }

    [JsonProperty("custom_data")]
    public string? CustomData { get; set; }

    [JsonProperty("custom1")]
    public string? Custom1 { get; set; }

    [JsonProperty("custom2")]
    public string? Custom2 { get; set; }

    [JsonProperty("created")]
    public string? Created { get; set; }
}
