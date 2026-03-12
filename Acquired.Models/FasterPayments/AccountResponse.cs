using Newtonsoft.Json;

namespace Acquired.Models.FasterPayments;

public class AccountResponse
{
    [JsonProperty("mid")]
    public string? Mid { get; set; }

    [JsonProperty("account_name")]
    public string? AccountName { get; set; }

    [JsonProperty("currency")]
    public string? Currency { get; set; }

    [JsonProperty("balance")]
    public decimal? Balance { get; set; }

    [JsonProperty("status")]
    public string? Status { get; set; }

    [JsonProperty("created")]
    public string? Created { get; set; }
}
