using Acquired.Services.Tools;
using Microsoft.AspNetCore.Mvc;

namespace Acquired.Api.Controllers;

[ApiController]
[Route("v1/tools")]
public class ToolsController : ControllerBase
{
    private readonly IToolService _service;
    public ToolsController(IToolService service) => _service = service;

    [HttpPost("confirmation-of-payee")]
    public async Task<IActionResult> ConfirmPayee([FromBody] object request)
    {
        var result = await _service.ConfirmPayeeAsync<object>(request);
        return Created("", result);
    }
}
