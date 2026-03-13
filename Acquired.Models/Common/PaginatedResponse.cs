using Newtonsoft.Json;

namespace Acquired.Models.Common;

public class PaginatedResponse<T>
{
    [JsonProperty("meta", NullValueHandling = NullValueHandling.Ignore)]
    public PaginationMeta? Meta { get; set; }

    [JsonProperty("data", NullValueHandling = NullValueHandling.Ignore)]
    public List<T>? Data { get; set; }
}

public class PaginationMeta
{
    [JsonProperty("count")]
    public int Count { get; set; }

    [JsonProperty("offset")]
    public int Offset { get; set; }

    [JsonProperty("limit")]
    public int Limit { get; set; }

    [JsonProperty("total")]
    public int Total { get; set; }

    [JsonProperty("links", NullValueHandling = NullValueHandling.Ignore)]
    public List<PaginationLink>? Links { get; set; }
}

public class PaginationLink
{
    [JsonProperty("rel")]
    public string? Rel { get; set; }

    [JsonProperty("href")]
    public string? Href { get; set; }

    [JsonProperty("title")]
    public string? Title { get; set; }
}
