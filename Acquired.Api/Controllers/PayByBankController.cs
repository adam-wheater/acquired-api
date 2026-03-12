using Acquired.Models.PayByBank;
using Acquired.Services.PayByBank;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;

namespace Acquired.Api.Controllers;

[ApiController]
[Route("api/pay-by-bank")]
public class PayByBankController : ControllerBase
{
    private readonly IPayByBankService _service;

    public PayByBankController(IPayByBankService service)
    {
        _service = service;
    }

    [HttpGet("banks")]
    public async Task<IActionResult> ListBanks(
        [FromHeader(Name = "X-Company-Id")][Required] string companyId,
        CancellationToken ct)
    {
        var result = await _service.ListBanksAsync(ct);
        return Ok(result);
    }

    [HttpPost("single-immediate-payment")]
    public async Task<IActionResult> CreateSingleImmediatePayment(
        [FromHeader(Name = "X-Company-Id")][Required] string companyId,
        [FromBody] CreateSingleImmediatePaymentRequest request,
        CancellationToken ct)
    {
        var result = await _service.CreateSingleImmediatePaymentAsync(request, ct);
        return Created(string.Empty, result);
    }
}
