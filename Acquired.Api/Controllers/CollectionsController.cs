using Acquired.Models.DirectDebit;
using Acquired.Services.DirectDebit;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;

namespace Acquired.Api.Controllers;

[ApiController]
[Route("api/collections")]
public class CollectionsController : ControllerBase
{
    private readonly ICollectionsService _service;

    public CollectionsController(ICollectionsService service)
    {
        _service = service;
    }

    [HttpPost]
    public async Task<IActionResult> Create(
        [FromHeader(Name = "X-Company-Id")][Required] string companyId,
        [FromBody] CreateCollectionRequest request,
        CancellationToken ct)
    {
        var result = await _service.CreateAsync(request, ct);
        return Created($"api/collections/{result.CollectionId}", result);
    }

    [HttpGet("{collectionId}")]
    public async Task<IActionResult> Get(
        [FromHeader(Name = "X-Company-Id")][Required] string companyId,
        string collectionId,
        CancellationToken ct)
    {
        var result = await _service.GetAsync(collectionId, ct);
        return Ok(result);
    }

    [HttpPost("{collectionId}/cancel")]
    public async Task<IActionResult> Cancel(
        [FromHeader(Name = "X-Company-Id")][Required] string companyId,
        string collectionId,
        CancellationToken ct)
    {
        await _service.CancelAsync(collectionId, ct);
        return NoContent();
    }
}
