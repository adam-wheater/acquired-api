using System.Net;
using Acquired.Models.Common;
using Newtonsoft.Json;

namespace Acquired.Services.Exceptions;

public class AcquiredException : Exception
{
    public int StatusCode { get; }
    public AcquiredErrorResponse? ErrorResponse { get; }
    public string Content { get; }

    public AcquiredException(HttpResponseMessage response, string content)
        : base($"Acquired API error {(int)response.StatusCode}: {content}")
    {
        StatusCode = (int)response.StatusCode;
        Content = content;

        try
        {
            ErrorResponse = JsonConvert.DeserializeObject<AcquiredErrorResponse>(content);
        }
        catch
        {
            ErrorResponse = null;
        }
    }
}
