using Acquired.Services.Http;
using Microsoft.AspNetCore.Mvc;

namespace Acquired.Api.Controllers;

[ApiController]
[Route("v1/payment-methods")]
public class PaymentMethodsController : ControllerBase
{
    private readonly IAcquiredHttpClient _httpClient;
    public PaymentMethodsController(IAcquiredHttpClient httpClient) => _httpClient = httpClient;

    [HttpPost("apple-pay/session")]
    public async Task<IActionResult> CreateApplePaySession([FromBody] object request)
    {
        var result = await _httpClient.PostAsync<object>("/v1/payment-methods/apple-pay/session", request);
        return Created("", result);
    }
}
