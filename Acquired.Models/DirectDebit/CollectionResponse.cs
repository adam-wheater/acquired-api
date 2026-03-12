using Newtonsoft.Json;

namespace Acquired.Models.DirectDebit;

public class CollectionResponse
{
    [JsonProperty("collection_id")]
    public string? CollectionId { get; set; }

    [JsonProperty("mandate_id")]
    public string? MandateId { get; set; }

    [JsonProperty("amount")]
    public decimal? Amount { get; set; }

    [JsonProperty("currency")]
    public string? Currency { get; set; }

    [JsonProperty("status")]
    public string? Status { get; set; }

    [JsonProperty("reference")]
    public string? Reference { get; set; }

    [JsonProperty("created")]
    public string? Created { get; set; }
}
