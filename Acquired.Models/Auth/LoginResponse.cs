using Newtonsoft.Json;

namespace Acquired.Models.Auth;

public class LoginResponse
{
    [JsonProperty("token_type")]
    public string? TokenType { get; set; }

    [JsonProperty("expires_in")]
    public int ExpiresIn { get; set; }

    [JsonProperty("access_token")]
    public string? AccessToken { get; set; }
}
