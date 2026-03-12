using Acquired.Models.DirectDebit;
using Acquired.Services.DirectDebit;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;

namespace Acquired.Api.Controllers;

[ApiController]
[Route("api/mandates")]
public class MandatesController : ControllerBase
{
    private readonly IMandatesService _service;

    public MandatesController(IMandatesService service)
    {
        _service = service;
    }

    [HttpPost]
    public async Task<IActionResult> Create(
        [FromHeader(Name = "X-Company-Id")][Required] string companyId,
        [FromBody] CreateMandateRequest request,
        CancellationToken ct)
    {
        var result = await _service.CreateAsync(request, ct);
        return Created($"api/mandates/{result.MandateId}", result);
    }

    [HttpGet("{mandateId}")]
    public async Task<IActionResult> Get(
        [FromHeader(Name = "X-Company-Id")][Required] string companyId,
        string mandateId,
        CancellationToken ct)
    {
        var result = await _service.GetAsync(mandateId, ct);
        return Ok(result);
    }

    [HttpGet]
    public async Task<IActionResult> List(
        [FromHeader(Name = "X-Company-Id")][Required] string companyId,
        [FromQuery] Dictionary<string, string?> filters,
        CancellationToken ct)
    {
        var result = await _service.ListAsync(filters, ct);
        return Ok(result);
    }

    [HttpPost("{mandateId}/cancel")]
    public async Task<IActionResult> Cancel(
        [FromHeader(Name = "X-Company-Id")][Required] string companyId,
        string mandateId,
        CancellationToken ct)
    {
        await _service.CancelAsync(mandateId, ct);
        return NoContent();
    }
}
