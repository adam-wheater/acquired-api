using Newtonsoft.Json;

namespace Acquired.Models.FasterPayments;

public class AccountResponse
{
    [JsonProperty("account_id")]
    public string? AccountId { get; set; }

    [JsonProperty("account_name")]
    public string? AccountName { get; set; }

    [JsonProperty("sort_code")]
    public string? SortCode { get; set; }

    [JsonProperty("account_number")]
    public string? AccountNumber { get; set; }

    [JsonProperty("currency")]
    public string? Currency { get; set; }

    [JsonProperty("status")]
    public string? Status { get; set; }

    [JsonProperty("created")]
    public string? Created { get; set; }
}
