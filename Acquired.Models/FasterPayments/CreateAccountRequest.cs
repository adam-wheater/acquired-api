using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations;

namespace Acquired.Models.FasterPayments;

public class CreateAccountRequest
{
    [JsonProperty("account_name")]
    [Required]
    public string AccountName { get; set; } = null!;

    [JsonProperty("currency")]
    [Required]
    public string Currency { get; set; } = null!;
}
