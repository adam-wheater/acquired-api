using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations;

namespace Acquired.Models.FasterPayments;

public class CreatePayeeRequest
{
    [JsonProperty("payee_details")]
    [Required]
    public PayeeDetails PayeeDetails { get; set; } = null!;
}

public class PayeeDetails
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

    [JsonProperty("country_code")]
    public string? CountryCode { get; set; }
}
