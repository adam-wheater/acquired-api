using Acquired.Services.FasterPayments;
using Microsoft.AspNetCore.Mvc;

namespace Acquired.Api.Controllers;

[ApiController]
[Route("v1")]
public class FasterPaymentsController : ControllerBase
{
    private readonly IFasterPaymentService _service;
    public FasterPaymentsController(IFasterPaymentService service) => _service = service;

    [HttpPost("customers/{customerId}/payees")]
    public async Task<IActionResult> CreatePayee(string customerId, [FromBody] object request)
    {
        var result = await _service.CreatePayeeAsync<object>(customerId, request);
        return Created("", result);
    }

    [HttpGet("payees")]
    public async Task<IActionResult> ListPayees()
    {
        var result = await _service.ListPayeesAsync<object>();
        return Ok(result);
    }

    [HttpPost("pay-out")]
    public async Task<IActionResult> CreatePayout([FromBody] object request)
    {
        var result = await _service.CreatePayoutAsync<object>(request);
        return Created("", result);
    }

    [HttpPost("accounts")]
    public async Task<IActionResult> CreateAccount([FromBody] object request)
    {
        var result = await _service.CreateAccountAsync<object>(request);
        return Created("", result);
    }

    [HttpGet("accounts")]
    public async Task<IActionResult> ListAccounts()
    {
        var result = await _service.ListAccountsAsync<object>();
        return Ok(result);
    }

    [HttpGet("accounts/{mid}")]
    public async Task<IActionResult> GetAccount(string mid)
    {
        var result = await _service.GetAccountByMidAsync<object>(mid);
        return Ok(result);
    }
}
