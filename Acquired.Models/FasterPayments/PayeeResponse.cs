using Newtonsoft.Json;

namespace Acquired.Models.FasterPayments;

public class PayeeResponse
{
    [JsonProperty("payee_id")]
    public string? PayeeId { get; set; }

    [JsonProperty("account_name")]
    public string? AccountName { get; set; }

    [JsonProperty("sort_code")]
    public string? SortCode { get; set; }

    [JsonProperty("account_number")]
    public string? AccountNumber { get; set; }

    [JsonProperty("reference")]
    public string? Reference { get; set; }

    [JsonProperty("status")]
    public string? Status { get; set; }

    [JsonProperty("created")]
    public string? Created { get; set; }
}
