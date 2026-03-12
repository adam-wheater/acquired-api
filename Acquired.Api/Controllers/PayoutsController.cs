using Acquired.Models.FasterPayments;
using Acquired.Services.FasterPayments;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;

namespace Acquired.Api.Controllers;

[ApiController]
[Route("api/payouts")]
public class PayoutsController : ControllerBase
{
    private readonly IPayoutsService _service;

    public PayoutsController(IPayoutsService service)
    {
        _service = service;
    }

    [HttpPost]
    public async Task<IActionResult> Create(
        [FromHeader(Name = "X-Company-Id")][Required] string companyId,
        [FromBody] CreatePayoutRequest request,
        CancellationToken ct)
    {
        var result = await _service.CreateAsync(request, ct);
        return Created(string.Empty, result);
    }
}
