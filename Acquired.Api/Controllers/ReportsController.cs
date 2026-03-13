using Acquired.Models.Reports;
using Acquired.Services.Reports;
using Microsoft.AspNetCore.Mvc;

namespace Acquired.Api.Controllers;

[ApiController]
[Route("v1/reports")]
public class ReportsController : ControllerBase
{
    private readonly IReportService _service;
    public ReportsController(IReportService service) => _service = service;

    [HttpGet("reconciliations")]
    public async Task<IActionResult> List([FromQuery] ReconQuery query)
    {
        var result = await _service.ListReconciliationsAsync<object>(query);
        return Ok(result);
    }

    [HttpGet("reconciliations/{reconciliationId}")]
    public async Task<IActionResult> Get(string reconciliationId)
    {
        var result = await _service.GetReconciliationAsync<object>(reconciliationId);
        return Ok(result);
    }
}
