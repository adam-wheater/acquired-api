using Acquired.Models.Transactions;
using Acquired.Services.Transactions;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;

namespace Acquired.Api.Controllers;

[ApiController]
[Route("api/transactions")]
public class TransactionsController : ControllerBase
{
    private readonly ITransactionsService _service;

    public TransactionsController(ITransactionsService service)
    {
        _service = service;
    }

    [HttpGet("{transactionId}")]
    public async Task<IActionResult> Get(
        [FromHeader(Name = "X-Company-Id")][Required] string companyId,
        string transactionId,
        CancellationToken ct)
    {
        var result = await _service.GetAsync(transactionId, ct);
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

    [HttpPost("refund")]
    public async Task<IActionResult> Refund(
        [FromHeader(Name = "X-Company-Id")][Required] string companyId,
        [FromBody] RefundRequest request,
        CancellationToken ct)
    {
        var result = await _service.RefundAsync(request, ct);
        return Ok(result);
    }

    [HttpPost("void")]
    public async Task<IActionResult> Void(
        [FromHeader(Name = "X-Company-Id")][Required] string companyId,
        [FromBody] VoidRequest request,
        CancellationToken ct)
    {
        var result = await _service.VoidAsync(request, ct);
        return Ok(result);
    }

    [HttpPost("capture")]
    public async Task<IActionResult> Capture(
        [FromHeader(Name = "X-Company-Id")][Required] string companyId,
        [FromBody] CaptureRequest request,
        CancellationToken ct)
    {
        var result = await _service.CaptureAsync(request, ct);
        return Ok(result);
    }

    [HttpPost("reversal")]
    public async Task<IActionResult> Reversal(
        [FromHeader(Name = "X-Company-Id")][Required] string companyId,
        [FromBody] ReversalRequest request,
        CancellationToken ct)
    {
        var result = await _service.ReversalAsync(request, ct);
        return Ok(result);
    }
}
