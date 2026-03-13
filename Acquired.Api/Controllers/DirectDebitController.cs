using Acquired.Services.DirectDebit;
using Microsoft.AspNetCore.Mvc;

namespace Acquired.Api.Controllers;

[ApiController]
[Route("v1/mandates")]
public class DirectDebitController : ControllerBase
{
    private readonly IDirectDebitService _service;
    public DirectDebitController(IDirectDebitService service) => _service = service;

    [HttpPost]
    public async Task<IActionResult> CreateMandate([FromBody] object request)
    {
        var result = await _service.CreateMandateAsync<object>(request);
        return Created("", result);
    }

    [HttpGet("{mandateId}")]
    public async Task<IActionResult> GetMandate(string mandateId)
    {
        var result = await _service.GetMandateAsync<object>(mandateId);
        return Ok(result);
    }

    [HttpPost("{mandateId}/cancel")]
    public async Task<IActionResult> CancelMandate(string mandateId)
    {
        var result = await _service.CancelMandateAsync<object>(mandateId);
        return Ok(result);
    }
}
