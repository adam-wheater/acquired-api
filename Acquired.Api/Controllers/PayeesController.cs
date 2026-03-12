using Acquired.Models.FasterPayments;
using Acquired.Services.FasterPayments;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;

namespace Acquired.Api.Controllers;

[ApiController]
[Route("api/customers/{customerId}/payees")]
public class PayeesController : ControllerBase
{
    private readonly IPayeesService _service;

    public PayeesController(IPayeesService service)
    {
        _service = service;
    }

    [HttpPost]
    public async Task<IActionResult> Create(
        [FromHeader(Name = "X-Company-Id")][Required] string companyId,
        string customerId,
        [FromBody] CreatePayeeRequest request,
        CancellationToken ct)
    {
        var result = await _service.CreateAsync(customerId, request, ct);
        return Created(string.Empty, result);
    }
}

[ApiController]
[Route("api/payees")]
public class PayeesListController : ControllerBase
{
    private readonly IPayeesService _service;

    public PayeesListController(IPayeesService service)
    {
        _service = service;
    }

    [HttpGet]
    public async Task<IActionResult> List(
        [FromHeader(Name = "X-Company-Id")][Required] string companyId,
        [FromQuery] Dictionary<string, string?> filters,
        CancellationToken ct)
    {
        var result = await _service.ListAsync(filters, ct);
        return Ok(result);
    }
}
