using Acquired.Models.Tools;
using Acquired.Services.Tools;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;

namespace Acquired.Api.Controllers;

[ApiController]
[Route("api/tools")]
public class ToolsController : ControllerBase
{
    private readonly IToolsService _service;

    public ToolsController(IToolsService service)
    {
        _service = service;
    }

    [HttpPost("confirmation-of-payee")]
    public async Task<IActionResult> ConfirmPayee(
        [FromHeader(Name = "X-Company-Id")][Required] string companyId,
        [FromBody] ConfirmationOfPayeeRequest request,
        CancellationToken ct)
    {
        var result = await _service.ConfirmPayeeAsync(request, ct);
        return Ok(result);
    }
}
