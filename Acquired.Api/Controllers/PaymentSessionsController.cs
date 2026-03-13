using Acquired.Services.PaymentSessions;
using Microsoft.AspNetCore.Mvc;

namespace Acquired.Api.Controllers;

[ApiController]
[Route("v1/payment-sessions")]
public class PaymentSessionsController : ControllerBase
{
    private readonly IPaymentSessionService _service;
    public PaymentSessionsController(IPaymentSessionService service) => _service = service;

    [HttpPost]
    public async Task<IActionResult> Create([FromBody] object request)
    {
        var result = await _service.CreateSessionAsync<object>(request);
        return Created("", result);
    }

    [HttpPut("{sessionId}")]
    public async Task<IActionResult> Update(string sessionId, [FromBody] object request)
    {
        var result = await _service.UpdateSessionAsync<object>(sessionId, request);
        return Ok(result);
    }
}
