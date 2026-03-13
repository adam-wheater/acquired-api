using Acquired.Models.Common;
using Acquired.Services.Exceptions;
using Newtonsoft.Json;

namespace Acquired.Api.Middleware;

public class AcquiredExceptionMiddleware
{
    private readonly RequestDelegate _next;
    private readonly ILogger<AcquiredExceptionMiddleware> _logger;

    public AcquiredExceptionMiddleware(RequestDelegate next, ILogger<AcquiredExceptionMiddleware> logger)
    {
        _next = next;
        _logger = logger;
    }

    public async Task InvokeAsync(HttpContext context)
    {
        // Extract headers and store in HttpContext.Items for propagation
        if (context.Request.Headers.TryGetValue("Company-Id", out var companyId))
            context.Items["Company-Id"] = companyId.ToString();

        if (context.Request.Headers.TryGetValue("Mid", out var mid))
            context.Items["Mid"] = mid.ToString();

        var correlationId = context.Request.Headers.TryGetValue("X-Correlation-Id", out var existingCorrelation)
            ? existingCorrelation.ToString()
            : Guid.NewGuid().ToString();
        context.Items["CorrelationId"] = correlationId;
        context.Response.Headers["X-Correlation-Id"] = correlationId;

        try
        {
            await _next(context);
        }
        catch (AcquiredException ex)
        {
            _logger.LogWarning(ex, "Acquired API error {StatusCode}", ex.StatusCode);
            context.Response.StatusCode = ex.StatusCode;
            context.Response.ContentType = "application/json";

            var body = ex.ErrorResponse is not null
                ? JsonConvert.SerializeObject(ex.ErrorResponse, new JsonSerializerSettings { NullValueHandling = NullValueHandling.Ignore })
                : ex.Content;

            await context.Response.WriteAsync(body);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Unhandled exception");
            context.Response.StatusCode = 500;
            context.Response.ContentType = "application/json";

            var error = new AcquiredErrorResponse
            {
                Status = "error",
                ErrorType = "internal_server_error",
                Title = "An unexpected error occurred.",
                Instance = context.Request.Path
            };

            var json = JsonConvert.SerializeObject(error, new JsonSerializerSettings { NullValueHandling = NullValueHandling.Ignore });
            await context.Response.WriteAsync(json);
        }
    }
}
