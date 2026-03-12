using Acquired.Models.PaymentLinks;
using Acquired.Services.PaymentLinks;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;

namespace Acquired.Api.Controllers;

[ApiController]
[Route("api/payment-links")]
public class PaymentLinksController : ControllerBase
{
    private readonly IPaymentLinksService _service;

    public PaymentLinksController(IPaymentLinksService service)
    {
        _service = service;
    }

    [HttpPost]
    public async Task<IActionResult> Create(
        [FromHeader(Name = "X-Company-Id")][Required] string companyId,
        [FromBody] CreatePaymentLinkRequest request,
        CancellationToken ct)
    {
        var result = await _service.CreateAsync(request, ct);
        return Created(string.Empty, result);
    }

    [HttpPost("{linkId}/send")]
    public async Task<IActionResult> Send(
        [FromHeader(Name = "X-Company-Id")][Required] string companyId,
        string linkId,
        [FromBody] SendPaymentLinkRequest request,
        CancellationToken ct)
    {
        await _service.SendAsync(linkId, request, ct);
        return NoContent();
    }
}
