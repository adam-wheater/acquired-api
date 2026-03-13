using Newtonsoft.Json;

namespace Acquired.Models.Auth;

public class LoginRequest
{
    [JsonProperty("app_id")]
    public string AppId { get; set; } = default!;

    [JsonProperty("app_key")]
    public string AppKey { get; set; } = default!;
}
