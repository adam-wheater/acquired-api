using Newtonsoft.Json;

namespace Acquired.Models.Tools;

public class ConfirmationOfPayeeRequest
{
    [JsonProperty("account_name")]
    public string AccountName { get; set; } = default!;

    [JsonProperty("sort_code")]
    public string SortCode { get; set; } = default!;

    [JsonProperty("account_number")]
    public string AccountNumber { get; set; } = default!;

    [JsonProperty("account_type")]
    public string AccountType { get; set; } = default!;
}
