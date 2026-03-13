using Acquired.Services.PaymentLinks;
using Microsoft.AspNetCore.Mvc;

namespace Acquired.Api.Controllers;

[ApiController]
[Route("v1/payment-links")]
public class PaymentLinksController : ControllerBase
{
    private readonly IPaymentLinkService _service;
    public PaymentLinksController(IPaymentLinkService service) => _service = service;

    [HttpPost]
    public async Task<IActionResult> Create([FromBody] object request)
    {
        var result = await _service.CreatePaymentLinkAsync<object>(request);
        return Created("", result);
    }

    [HttpPost("{linkId}/send")]
    public async Task<IActionResult> Send(string linkId, [FromBody] object request)
    {
        var result = await _service.SendPaymentLinkAsync<object>(linkId, request);
        return Ok(result);
    }
}
