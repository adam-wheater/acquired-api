using Newtonsoft.Json;

namespace Acquired.Models.FasterPayments;

public class PayeeResponse
{
    [JsonProperty("payee_id")]
    public string? PayeeId { get; set; }

    [JsonProperty("status")]
    public string? Status { get; set; }

    [JsonProperty("payee_details")]
    public PayeeDetailsResponse? PayeeDetails { get; set; }

    [JsonProperty("created")]
    public string? Created { get; set; }
}

public class PayeeDetailsResponse
{
    [JsonProperty("name")]
    public string? Name { get; set; }

    [JsonProperty("account_number")]
    public string? AccountNumber { get; set; }

    [JsonProperty("sort_code")]
    public string? SortCode { get; set; }

    [JsonProperty("country_code")]
    public string? CountryCode { get; set; }
}
