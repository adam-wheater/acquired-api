using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace Acquired.Models.Reports;

public class ReconciliationResponse
{
    [JsonProperty("id")]
    public string? Id { get; set; }

    [JsonProperty("status")]
    public string? Status { get; set; }

    [JsonProperty("data")]
    public JObject? Data { get; set; }

    [JsonProperty("created")]
    public string? Created { get; set; }
}
