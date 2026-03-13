using Acquired.Services.Payments;
using Microsoft.AspNetCore.Mvc;

namespace Acquired.Api.Controllers;

[ApiController]
[Route("v1/payments")]
public class PaymentsController : ControllerBase
{
    private readonly IPaymentService _service;
    public PaymentsController(IPaymentService service) => _service = service;

    [HttpPost]
    public async Task<IActionResult> Create([FromBody] object request)
    {
        var result = await _service.CreatePaymentAsync<object>(request);
        return Created("", result);
    }

    [HttpPost("reuse")]
    public async Task<IActionResult> CreateReuse([FromBody] object request)
    {
        var result = await _service.CreateReusePaymentAsync<object>(request);
        return Created("", result);
    }

    [HttpPost("apple-pay")]
    public async Task<IActionResult> CreateApplePay([FromBody] object request)
    {
        var result = await _service.CreateApplePayAsync<object>(request);
        return Created("", result);
    }

    [HttpPost("google-pay")]
    public async Task<IActionResult> CreateGooglePay([FromBody] object request)
    {
        var result = await _service.CreateGooglePayAsync<object>(request);
        return Created("", result);
    }

    [HttpPost("recurring")]
    public async Task<IActionResult> CreateRecurring([FromBody] object request)
    {
        var result = await _service.CreateRecurringAsync<object>(request);
        return Created("", result);
    }

    [HttpPost("credit")]
    public async Task<IActionResult> CreateCredit([FromBody] object request)
    {
        var result = await _service.CreateCreditAsync<object>(request);
        return Created("", result);
    }

    [HttpPost("internal-transfer")]
    public async Task<IActionResult> CreateInternalTransfer([FromBody] object request)
    {
        var result = await _service.CreateInternalTransferAsync<object>(request);
        return Created("", result);
    }

    [HttpPost("collections")]
    public async Task<IActionResult> CreateCollection([FromBody] object request)
    {
        var result = await _service.CreateCollectionAsync<object>(request);
        return Created("", result);
    }
}
