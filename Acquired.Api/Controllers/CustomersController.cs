using Acquired.Models.Customers;
using Acquired.Services.Customers;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;

namespace Acquired.Api.Controllers;

[ApiController]
[Route("api/customers")]
public class CustomersController : ControllerBase
{
    private readonly ICustomersService _service;

    public CustomersController(ICustomersService service)
    {
        _service = service;
    }

    [HttpPost]
    public async Task<IActionResult> Create(
        [FromHeader(Name = "X-Company-Id")][Required] string companyId,
        [FromBody] CreateCustomerRequest request,
        CancellationToken ct)
    {
        var result = await _service.CreateAsync(request, ct);
        return Created($"api/customers/{result.CustomerId}", result);
    }

    [HttpGet("{customerId}")]
    public async Task<IActionResult> Get(
        [FromHeader(Name = "X-Company-Id")][Required] string companyId,
        string customerId,
        CancellationToken ct)
    {
        var result = await _service.GetAsync(customerId, ct);
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

    [HttpPut("{customerId}")]
    public async Task<IActionResult> Update(
        [FromHeader(Name = "X-Company-Id")][Required] string companyId,
        string customerId,
        [FromBody] UpdateCustomerRequest request,
        CancellationToken ct)
    {
        var result = await _service.UpdateAsync(customerId, request, ct);
        return Ok(result);
    }
}
