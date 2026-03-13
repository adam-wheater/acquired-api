using Acquired.Models.Common;
using Acquired.Models.Customers;
using Acquired.Services.Customers;
using Microsoft.AspNetCore.Mvc;

namespace Acquired.Api.Controllers;

[ApiController]
[Route("v1/customers")]
public class CustomersController : ControllerBase
{
    private readonly ICustomerService _service;
    public CustomersController(ICustomerService service) => _service = service;

    [HttpPost]
    public async Task<IActionResult> Create([FromBody] CustomerRequest request)
    {
        var result = await _service.CreateAsync(request);
        return Created("", result);
    }

    [HttpGet]
    public async Task<IActionResult> List([FromQuery] PaginationQuery query)
    {
        var result = await _service.GetAllAsync(query);
        return Ok(result);
    }

    [HttpGet("{customerId}")]
    public async Task<IActionResult> Get(string customerId)
    {
        var result = await _service.GetByIdAsync(customerId);
        return Ok(result);
    }

    [HttpPut("{customerId}")]
    public async Task<IActionResult> Update(string customerId, [FromBody] CustomerRequest request)
    {
        var result = await _service.UpdateAsync(customerId, request);
        return Ok(result);
    }
}
