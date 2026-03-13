using Newtonsoft.Json;

namespace Acquired.Models.DirectDebit;

public class MandateResponse
{
    [JsonProperty("mandate_id")]
    public string? MandateId { get; set; }

    [JsonProperty("customer_id")]
    public string? CustomerId { get; set; }

    [JsonProperty("mandate_reference")]
    public string? MandateReference { get; set; }

    [JsonProperty("scheme")]
    public string? Scheme { get; set; }

    [JsonProperty("status")]
    public string? Status { get; set; }

    [JsonProperty("bank_account", NullValueHandling = NullValueHandling.Ignore)]
    public BankAccountDetails? BankAccount { get; set; }

    [JsonProperty("created")]
    public string? Created { get; set; }

    [JsonProperty("last_updated")]
    public string? LastUpdated { get; set; }
}
