using Newtonsoft.Json;

namespace Acquired.Models.Cards;

public class CardResponse
{
    [JsonProperty("card_id")]
    public string? CardId { get; set; }

    [JsonProperty("customer_id")]
    public string? CustomerId { get; set; }

    [JsonProperty("card", NullValueHandling = NullValueHandling.Ignore)]
    public CardDetails? Card { get; set; }

    [JsonProperty("bin", NullValueHandling = NullValueHandling.Ignore)]
    public BinDetails? Bin { get; set; }

    [JsonProperty("source")]
    public string? Source { get; set; }

    [JsonProperty("created")]
    public string? Created { get; set; }

    [JsonProperty("last_updated")]
    public string? LastUpdated { get; set; }

    [JsonProperty("is_active")]
    public bool? IsActive { get; set; }
}

public class CardDetails
{
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
}

public class BinDetails
{
    [JsonProperty("number")]
    public string? Number { get; set; }

    [JsonProperty("scheme")]
    public string? Scheme { get; set; }

    [JsonProperty("type")]
    public string? Type { get; set; }

    [JsonProperty("product")]
    public string? Product { get; set; }

    [JsonProperty("card_level")]
    public string? CardLevel { get; set; }

    [JsonProperty("issuer")]
    public string? Issuer { get; set; }

    [JsonProperty("issuer_country")]
    public string? IssuerCountry { get; set; }

    [JsonProperty("issuer_country_code")]
    public string? IssuerCountryCode { get; set; }

    [JsonProperty("eea")]
    public bool? Eea { get; set; }

    [JsonProperty("non_reloadable")]
    public bool? NonReloadable { get; set; }
}
