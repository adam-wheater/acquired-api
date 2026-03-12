using Newtonsoft.Json;

namespace Acquired.Models.Cards;

public class CardResponse
{
    [JsonProperty("card_id")]
    public string? CardId { get; set; }

    [JsonProperty("customer_id")]
    public string? CustomerId { get; set; }

    [JsonProperty("holder_name")]
    public string? HolderName { get; set; }

    [JsonProperty("scheme")]
    public string? Scheme { get; set; }

    [JsonProperty("number")]
    public string? Number { get; set; }

    [JsonProperty("expiry_month")]
    public int? ExpiryMonth { get; set; }

    [JsonProperty("expiry_year")]
    public int? ExpiryYear { get; set; }

    [JsonProperty("is_active")]
    public bool? IsActive { get; set; }

    [JsonProperty("created")]
    public string? Created { get; set; }

    [JsonProperty("updated")]
    public string? Updated { get; set; }
}
