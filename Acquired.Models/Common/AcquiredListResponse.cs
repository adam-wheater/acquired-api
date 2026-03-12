using Newtonsoft.Json;

namespace Acquired.Models.Common;

public class AcquiredListResponse<T>
{
    [JsonProperty("meta")]
    public AcquiredMeta? Meta { get; set; }

    [JsonProperty("data")]
    public List<T>? Data { get; set; }
}

public class AcquiredMeta
{
    [JsonProperty("count")]
    public int Count { get; set; }

    [JsonProperty("offset")]
    public int Offset { get; set; }

    [JsonProperty("limit")]
    public int Limit { get; set; }

    [JsonProperty("total")]
    public int Total { get; set; }
}
