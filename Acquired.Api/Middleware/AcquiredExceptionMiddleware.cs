using Acquired.Api.Models;
using Acquired.Services.Exceptions;
using Microsoft.AspNetCore.Http;
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
        var correlationId = context.Request.Headers["X-Correlation-Id"].FirstOrDefault()
            ?? Guid.NewGuid().ToString();
        context.Items["CorrelationId"] = correlationId;

        // Extract Company-Id and Mid headers for downstream propagation
        var companyId = context.Request.Headers["X-Company-Id"].FirstOrDefault();
        if (!string.IsNullOrEmpty(companyId))
            context.Items["CompanyId"] = companyId;

        var mid = context.Request.Headers["X-Mid"].FirstOrDefault();
        if (!string.IsNullOrEmpty(mid))
            context.Items["Mid"] = mid;

        try
        {
            await _next(context);
        }
        catch (AcquiredApiException ex)
        {
            var statusCode = MapAcquiredHttpStatus(ex.AcquiredHttpStatusCode);
            _logger.LogWarning(ex, "Acquired API error: {StatusCode} {ErrorType} {Title}",
                ex.AcquiredHttpStatusCode, ex.ErrorType, ex.Title);

            await WriteErrorResponse(context, statusCode, new AcquiredErrorResponse
            {
                StatusCode = statusCode,
                ErrorCode = ex.ErrorType,
                Message = ex.Title,
                AcquiredErrorType = ex.ErrorType,
                CorrelationId = correlationId
            });
        }
        catch (AcquiredDeclinedException ex)
        {
            _logger.LogWarning(ex, "Payment declined: {Status} {Reason}", ex.Status, ex.DeclineReason);

            await WriteErrorResponse(context, 422, new AcquiredErrorResponse
            {
                StatusCode = 422,
                ErrorCode = "PAYMENT_DECLINED",
                Message = ex.Message,
                AcquiredErrorType = ex.Status,
                CorrelationId = correlationId
            });
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Unhandled exception");

            await WriteErrorResponse(context, 500, new AcquiredErrorResponse
            {
                StatusCode = 500,
                ErrorCode = "INTERNAL_ERROR",
                Message = "An unexpected error occurred",
                CorrelationId = correlationId
            });
        }
    }

    private static int MapAcquiredHttpStatus(int acquiredStatus) => acquiredStatus switch
    {
        400 => 400,
        401 or 403 => 502,
        404 => 404,
        409 => 409,
        429 => 429,
        _ when acquiredStatus >= 500 => 502,
        _ => 502
    };

    private static async Task WriteErrorResponse(HttpContext context, int statusCode, AcquiredErrorResponse error)
    {
        context.Response.StatusCode = statusCode;
        context.Response.ContentType = "application/json";
        var json = JsonConvert.SerializeObject(error, new JsonSerializerSettings
        {
            NullValueHandling = NullValueHandling.Ignore
        });
        await context.Response.WriteAsync(json);
    }
}
