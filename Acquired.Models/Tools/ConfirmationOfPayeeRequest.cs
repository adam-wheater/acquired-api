using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations;

namespace Acquired.Models.Tools;

public class ConfirmationOfPayeeRequest
{
    [JsonProperty("name")]
    [Required]
    public string Name { get; set; } = null!;

    [JsonProperty("account_number")]
    [Required]
    [StringLength(8)]
    public string AccountNumber { get; set; } = null!;

    [JsonProperty("sort_code")]
    [Required]
    [StringLength(6)]
    public string SortCode { get; set; } = null!;
}
