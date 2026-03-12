using Acquired.Models.FasterPayments;
using Acquired.Services.FasterPayments;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;

namespace Acquired.Api.Controllers;

[ApiController]
[Route("api/accounts")]
public class AccountsController : ControllerBase
{
    private readonly IAccountsService _service;

    public AccountsController(IAccountsService service)
    {
        _service = service;
    }

    [HttpPost]
    public async Task<IActionResult> Create(
        [FromHeader(Name = "X-Company-Id")][Required] string companyId,
        [FromBody] CreateAccountRequest request,
        CancellationToken ct)
    {
        var result = await _service.CreateAsync(request, ct);
        return Created($"api/accounts/{result.Mid}", result);
    }

    [HttpGet("{mid}")]
    public async Task<IActionResult> Get(
        [FromHeader(Name = "X-Company-Id")][Required] string companyId,
        string mid,
        CancellationToken ct)
    {
        var result = await _service.GetAsync(mid, ct);
        return Ok(result);
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
