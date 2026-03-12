using Acquired.Services.Reports;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;

namespace Acquired.Api.Controllers;

[ApiController]
[Route("api/reports")]
public class ReportsController : ControllerBase
{
    private readonly IReportsService _service;

    public ReportsController(IReportsService service)
    {
        _service = service;
    }

    [HttpGet("reconciliations")]
    public async Task<IActionResult> ListReconciliations(
        [FromHeader(Name = "X-Company-Id")][Required] string companyId,
        [FromQuery] Dictionary<string, string?> filters,
        CancellationToken ct)
    {
        var result = await _service.ListReconciliationsAsync(filters, ct);
        return Ok(result);
    }

    [HttpGet("reconciliations/{id}")]
    public async Task<IActionResult> GetReconciliation(
        [FromHeader(Name = "X-Company-Id")][Required] string companyId,
        string id,
        CancellationToken ct)
    {
        var result = await _service.GetReconciliationAsync(id, ct);
        return Ok(result);
    }
}
