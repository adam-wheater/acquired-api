using Acquired.Models.Common;
using Acquired.Services.OpenBanking;
using Microsoft.AspNetCore.Mvc;

namespace Acquired.Api.Controllers;

[ApiController]
[Route("v1/open-banking")]
public class OpenBankingController : ControllerBase
{
    private readonly IOpenBankingService _service;
    public OpenBankingController(IOpenBankingService service) => _service = service;

    [HttpGet("supported-banks")]
    public async Task<IActionResult> GetSupportedBanks()
    {
        var result = await _service.GetSupportedBanksAsync<object>();
        return Ok(result);
    }

    [HttpPost("mandates")]
    public async Task<IActionResult> CreateMandate([FromBody] object request)
    {
        var result = await _service.CreateMandateAsync<object>(request);
        return Created("", result);
    }

    [HttpGet("mandates")]
    public async Task<IActionResult> ListMandates([FromQuery] PaginationQuery query)
    {
        var result = await _service.ListMandatesAsync<object>(query);
        return Ok(result);
    }

    [HttpGet("mandates/{mandateId}")]
    public async Task<IActionResult> GetMandate(string mandateId)
    {
        var result = await _service.GetMandateAsync<object>(mandateId);
        return Ok(result);
    }

    [HttpPost("vrps")]
    public async Task<IActionResult> CreateVrp([FromBody] object request)
    {
        var result = await _service.CreateVrpAsync<object>(request);
        return Created("", result);
    }

    [HttpGet("vrps")]
    public async Task<IActionResult> ListVrps([FromQuery] PaginationQuery query)
    {
        var result = await _service.ListVrpsAsync<object>(query);
        return Ok(result);
    }

    [HttpPost("mandates/{mandateId}/confirm-funds")]
    public async Task<IActionResult> ConfirmFunds(string mandateId, [FromBody] object request)
    {
        var result = await _service.ConfirmFundsAsync<object>(mandateId, request);
        return Ok(result);
    }
}
