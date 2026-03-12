using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations;

namespace Acquired.Models.Common;

public class AcquiredCard
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

    [JsonProperty("cvv")]
    [StringLength(4)]
    public string? Cvv { get; set; }
}
