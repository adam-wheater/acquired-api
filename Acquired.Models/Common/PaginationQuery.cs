namespace Acquired.Models.Common;

public class PaginationQuery
{
    public int? Offset { get; set; }
    public int? Limit { get; set; }
    public string? Filter { get; set; }
}
