using Acquired.Services.PayByBank;
using Microsoft.AspNetCore.Mvc;

namespace Acquired.Api.Controllers;

[ApiController]
[Route("v1")]
public class PayByBankController : ControllerBase
{
    private readonly IPayByBankService _service;
    public PayByBankController(IPayByBankService service) => _service = service;

    [HttpGet("aspsps")]
    public async Task<IActionResult> GetAspsps()
    {
        var result = await _service.GetAspspsAsync<object>();
        return Ok(result);
    }

    [HttpPost("single-immediate-payment")]
    public async Task<IActionResult> CreateSingleImmediatePayment([FromBody] object request)
    {
        var result = await _service.CreateSingleImmediatePaymentAsync<object>(request);
        return Created("", result);
    }
}
