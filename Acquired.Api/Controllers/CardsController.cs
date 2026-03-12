using Acquired.Models.Cards;
using Acquired.Services.Cards;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;

namespace Acquired.Api.Controllers;

[ApiController]
[Route("api/cards")]
public class CardsController : ControllerBase
{
    private readonly ICardsService _service;

    public CardsController(ICardsService service)
    {
        _service = service;
    }

    [HttpGet("{cardId}")]
    public async Task<IActionResult> Get(
        [FromHeader(Name = "X-Company-Id")][Required] string companyId,
        string cardId,
        CancellationToken ct)
    {
        var result = await _service.GetAsync(cardId, ct);
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

    [HttpPut("{cardId}")]
    public async Task<IActionResult> Update(
        [FromHeader(Name = "X-Company-Id")][Required] string companyId,
        string cardId,
        [FromBody] UpdateCardRequest request,
        CancellationToken ct)
    {
        var result = await _service.UpdateAsync(cardId, request, ct);
        return Ok(result);
    }
}

[ApiController]
[Route("api/customers/{customerId}/cards")]
public class CustomerCardsController : ControllerBase
{
    private readonly ICardsService _service;

    public CustomerCardsController(ICardsService service)
    {
        _service = service;
    }

    [HttpGet]
    public async Task<IActionResult> List(
        [FromHeader(Name = "X-Company-Id")][Required] string companyId,
        string customerId,
        CancellationToken ct)
    {
        var result = await _service.ListByCustomerAsync(customerId, ct);
        return Ok(result);
    }
}
