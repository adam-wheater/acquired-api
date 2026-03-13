using Acquired.Models.Common;
using Acquired.Models.Transactions;
using Acquired.Services.Transactions;
using Microsoft.AspNetCore.Mvc;

namespace Acquired.Api.Controllers;

[ApiController]
[Route("v1/transactions")]
public class TransactionsController : ControllerBase
{
    private readonly ITransactionService _service;
    public TransactionsController(ITransactionService service) => _service = service;

    [HttpGet("{transactionId}")]
    public async Task<IActionResult> Get(string transactionId)
    {
        var result = await _service.GetByIdAsync<object>(transactionId);
        return Ok(result);
    }

    [HttpGet]
    public async Task<IActionResult> List([FromQuery] PaginationQuery query)
    {
        var result = await _service.GetAllAsync<object>(query);
        return Ok(result);
    }

    [HttpPost("{transactionId}/refund")]
    public async Task<IActionResult> Refund(string transactionId, [FromBody] RefundRequest request)
    {
        var result = await _service.RefundAsync<object>(transactionId, request);
        return Created("", result);
    }

    [HttpPost("{transactionId}/void")]
    public async Task<IActionResult> Void(string transactionId)
    {
        var result = await _service.VoidAsync<object>(transactionId);
        return Ok(result);
    }

    [HttpPost("{transactionId}/capture")]
    public async Task<IActionResult> Capture(string transactionId, [FromBody] CaptureRequest request)
    {
        var result = await _service.CaptureAsync<object>(transactionId, request);
        return Ok(result);
    }

    [HttpPost("{transactionId}/reversal")]
    public async Task<IActionResult> Reversal(string transactionId, [FromBody] ReversalRequest request)
    {
        var result = await _service.ReversalAsync<object>(transactionId, request);
        return Created("", result);
    }

    [HttpPost("{transactionId}/cancel")]
    public async Task<IActionResult> Cancel(string transactionId)
    {
        var result = await _service.CancelAsync<object>(transactionId);
        return Ok(result);
    }

    [HttpPost("{transactionId}/retry")]
    public async Task<IActionResult> Retry(string transactionId, [FromBody] RetryRequest request)
    {
        var result = await _service.RetryAsync<object>(transactionId, request);
        return Created("", result);
    }
}
