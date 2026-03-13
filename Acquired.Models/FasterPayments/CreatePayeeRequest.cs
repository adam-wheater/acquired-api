using Newtonsoft.Json;

namespace Acquired.Models.FasterPayments;

public class CreatePayeeRequest
{
    [JsonProperty("account_name")]
    public string AccountName { get; set; } = default!;

    [JsonProperty("sort_code")]
    public string SortCode { get; set; } = default!;

    [JsonProperty("account_number")]
    public string AccountNumber { get; set; } = default!;

    [JsonProperty("reference", NullValueHandling = NullValueHandling.Ignore)]
    public string? Reference { get; set; }

    [JsonProperty("email", NullValueHandling = NullValueHandling.Ignore)]
    public string? Email { get; set; }

    [JsonProperty("custom_data", NullValueHandling = NullValueHandling.Ignore)]
    public string? CustomData { get; set; }
}
