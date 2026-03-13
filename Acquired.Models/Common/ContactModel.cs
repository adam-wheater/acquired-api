using Newtonsoft.Json;

namespace Acquired.Models.Common;

public class ContactModel
{
    [JsonProperty("email", NullValueHandling = NullValueHandling.Ignore)]
    public string? Email { get; set; }

    [JsonProperty("phone", NullValueHandling = NullValueHandling.Ignore)]
    public PhoneModel? Phone { get; set; }
}
