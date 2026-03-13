using Acquired.Models.Common;

namespace Acquired.Models.Reports;

public class ReconQuery : PaginationQuery
{
    public string? DateFrom { get; set; }
    public string? DateTo { get; set; }
}
