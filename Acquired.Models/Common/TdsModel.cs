using Newtonsoft.Json;

namespace Acquired.Models.Common;

public class TdsModel
{
    [JsonProperty("is_active", NullValueHandling = NullValueHandling.Ignore)]
    public bool? IsActive { get; set; }

    [JsonProperty("challenge_preference", NullValueHandling = NullValueHandling.Ignore)]
    public string? ChallengePreference { get; set; }

    [JsonProperty("challenge_window_size", NullValueHandling = NullValueHandling.Ignore)]
    public string? ChallengeWindowSize { get; set; }

    [JsonProperty("contact_url", NullValueHandling = NullValueHandling.Ignore)]
    public string? ContactUrl { get; set; }

    [JsonProperty("redirect_url", NullValueHandling = NullValueHandling.Ignore)]
    public string? RedirectUrl { get; set; }

    [JsonProperty("webhook_url", NullValueHandling = NullValueHandling.Ignore)]
    public string? WebhookUrl { get; set; }
}
