using Acquired.Models.PaymentSessions;
using Acquired.Services.PaymentSessions;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;

namespace Acquired.Api.Controllers;

[ApiController]
[Route("api/payment-sessions")]
public class PaymentSessionsController : ControllerBase
{
    private readonly IPaymentSessionsService _service;

    public PaymentSessionsController(IPaymentSessionsService service)
    {
        _service = service;
    }

    [HttpPost]
    public async Task<IActionResult> Create(
        [FromHeader(Name = "X-Company-Id")][Required] string companyId,
        [FromBody] CreatePaymentSessionRequest request,
        CancellationToken ct)
    {
        var result = await _service.CreateAsync(request, ct);
        return Created(string.Empty, result);
    }
}
