using Newtonsoft.Json;

namespace Acquired.Models.FasterPayments;

public class CreateAccountRequest
{
    [JsonProperty("account_name")]
    public string AccountName { get; set; } = default!;

    [JsonProperty("sort_code", NullValueHandling = NullValueHandling.Ignore)]
    public string? SortCode { get; set; }

    [JsonProperty("account_number", NullValueHandling = NullValueHandling.Ignore)]
    public string? AccountNumber { get; set; }

    [JsonProperty("currency", NullValueHandling = NullValueHandling.Ignore)]
    public string? Currency { get; set; }

    [JsonProperty("reference", NullValueHandling = NullValueHandling.Ignore)]
    public string? Reference { get; set; }

    [JsonProperty("custom_data", NullValueHandling = NullValueHandling.Ignore)]
    public string? CustomData { get; set; }
}
