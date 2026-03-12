using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations;

namespace Acquired.Models.Auth;

public class LoginRequest
{
    [JsonProperty("app_id")]
    [Required]
    [StringLength(8)]
    public string AppId { get; set; } = null!;

    [JsonProperty("app_key")]
    [Required]
    [StringLength(32)]
    public string AppKey { get; set; } = null!;
}
