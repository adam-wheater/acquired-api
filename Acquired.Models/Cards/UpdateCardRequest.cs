using Newtonsoft.Json;

namespace Acquired.Models.Cards;

public class UpdateCardRequest
{
    [JsonProperty("holder_name", NullValueHandling = NullValueHandling.Ignore)]
    public string? HolderName { get; set; }

    [JsonProperty("number", NullValueHandling = NullValueHandling.Ignore)]
    public string? Number { get; set; }

    [JsonProperty("expiry_month", NullValueHandling = NullValueHandling.Ignore)]
    public int? ExpiryMonth { get; set; }

    [JsonProperty("expiry_year", NullValueHandling = NullValueHandling.Ignore)]
    public int? ExpiryYear { get; set; }

    [JsonProperty("is_active", NullValueHandling = NullValueHandling.Ignore)]
    public bool? IsActive { get; set; }
}
