using Newtonsoft.Json;

namespace Acquired.Models.Common;

public class AcquiredContact
{
    [JsonProperty("address")]
    public AcquiredAddress? Address { get; set; }

    [JsonProperty("email")]
    public string? Email { get; set; }

    [JsonProperty("phone")]
    public AcquiredPhone? Phone { get; set; }
}
