using Acquired.Models.Cards;
using Acquired.Models.Common;
using Acquired.Services.Cards;
using Microsoft.AspNetCore.Mvc;

namespace Acquired.Api.Controllers;

[ApiController]
[Route("v1")]
public class CardsController : ControllerBase
{
    private readonly ICardService _service;
    public CardsController(ICardService service) => _service = service;

    [HttpGet("customers/{customerId}/cards")]
    public async Task<IActionResult> GetByCustomer(string customerId, [FromQuery] PaginationQuery query)
    {
        var result = await _service.GetByCustomerIdAsync(customerId, query);
        return Ok(result);
    }

    [HttpGet("cards")]
    public async Task<IActionResult> List([FromQuery] PaginationQuery query)
    {
        var result = await _service.GetAllAsync(query);
        return Ok(result);
    }

    [HttpGet("cards/{cardId}")]
    public async Task<IActionResult> Get(string cardId)
    {
        var result = await _service.GetByIdAsync(cardId);
        return Ok(result);
    }

    [HttpPut("cards/{cardId}")]
    public async Task<IActionResult> Update(string cardId, [FromBody] UpdateCardRequest request)
    {
        var result = await _service.UpdateAsync(cardId, request);
        return Ok(result);
    }
}
