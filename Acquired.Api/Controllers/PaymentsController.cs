using Acquired.Models.Payments;
using Acquired.Services.Payments;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;

namespace Acquired.Api.Controllers;

[ApiController]
[Route("api/payments")]
public class PaymentsController : ControllerBase
{
    private readonly IPaymentsService _service;

    public PaymentsController(IPaymentsService service)
    {
        _service = service;
    }

    [HttpPost]
    public async Task<IActionResult> Create(
        [FromHeader(Name = "X-Company-Id")][Required] string companyId,
        [FromBody] CreatePaymentRequest request,
        CancellationToken ct)
    {
        var result = await _service.CreateAsync(request, ct);

        return result.Status switch
        {
            "tds_pending" => Accepted(result),
            _ => Created($"api/transactions/{result.TransactionId}", result)
        };
    }
}
