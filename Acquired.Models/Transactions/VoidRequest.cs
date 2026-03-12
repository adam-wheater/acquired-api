using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations;

namespace Acquired.Models.Transactions;

public class VoidRequest
{
    [JsonProperty("transaction_id")]
    [Required]
    public string TransactionId { get; set; } = null!;

    [JsonProperty("reference")]
    public string? Reference { get; set; }
}
