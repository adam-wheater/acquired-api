using Newtonsoft.Json;
using Acquired.Models.Common;

namespace Acquired.Models.Transactions;

public class TransactionResponse
{
    [JsonProperty("transaction_id")]
    public string? TransactionId { get; set; }

    [JsonProperty("status")]
    public string? Status { get; set; }

    [JsonProperty("issuer_response_code", NullValueHandling = NullValueHandling.Ignore)]
    public string? IssuerResponseCode { get; set; }

    [JsonProperty("links", NullValueHandling = NullValueHandling.Ignore)]
    public List<LinkModel>? Links { get; set; }
}
