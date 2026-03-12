using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations;

namespace Acquired.Models.Cards;

public class UpdateCardRequest
{
    [JsonProperty("holder_name")]
    [StringLength(50)]
    public string? HolderName { get; set; }

    [JsonProperty("number")]
    [StringLength(19)]
    public string? Number { get; set; }

    [JsonProperty("expiry_month")]
    [Range(1, 12)]
    public int? ExpiryMonth { get; set; }

    [JsonProperty("expiry_year")]
    public int? ExpiryYear { get; set; }

    [JsonProperty("is_active")]
    public bool? IsActive { get; set; }
}
