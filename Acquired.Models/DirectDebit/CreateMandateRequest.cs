using Newtonsoft.Json;

namespace Acquired.Models.DirectDebit;

public class CreateMandateRequest
{
    [JsonProperty("customer_id")]
    public string CustomerId { get; set; } = default!;

    [JsonProperty("bank_account")]
    public BankAccountDetails BankAccount { get; set; } = default!;

    [JsonProperty("mandate_reference", NullValueHandling = NullValueHandling.Ignore)]
    public string? MandateReference { get; set; }

    [JsonProperty("scheme", NullValueHandling = NullValueHandling.Ignore)]
    public string? Scheme { get; set; }
}

public class BankAccountDetails
{
    [JsonProperty("holder_name")]
    public string HolderName { get; set; } = default!;

    [JsonProperty("account_number", NullValueHandling = NullValueHandling.Ignore)]
    public string? AccountNumber { get; set; }

    [JsonProperty("sort_code", NullValueHandling = NullValueHandling.Ignore)]
    public string? SortCode { get; set; }

    [JsonProperty("iban", NullValueHandling = NullValueHandling.Ignore)]
    public string? Iban { get; set; }
}
