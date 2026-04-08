using System.Net;
using System.Text.Json;

namespace AssetMarketplace.API.Middleware;

public class GlobalExceptionMiddleware(RequestDelegate next, ILogger<GlobalExceptionMiddleware> logger)
{
    public async Task InvokeAsync(HttpContext context)
    {
        try
        {
            await next(context);
        }
        catch (Exception exception)
        {
            var location = exception.StackTrace?.Split('\n')
                .FirstOrDefault(line => line.Contains("AssetMarketplace"));

            logger.LogError(
                exception, "CRITICAL ERROR: {Method} | PATH: {Path} | LOCATION: {Location}",
                context.Request.Method,
                context.Request.Path,
                location ?? "Unknown");

            await HandleExceptionAsync(context, exception);
        }
    }

    private async Task HandleExceptionAsync(HttpContext context, Exception exception)
    {
        context.Response.ContentType = "application/json";

        var (statusCode, errorCode, resultMessage) = exception switch
        {
            InvalidOperationException ex => (
                HttpStatusCode.BadRequest,
                "Operation.Invalid",
                ex.Message),

            _ => (
                HttpStatusCode.InternalServerError,
                "Server.Error",
                "An unexpected error occurred on the server.")
        };

        context.Response.StatusCode = (int)statusCode;

        var response = new
        {
            error = errorCode,
            message = resultMessage,
            timestamp = DateTime.UtcNow
        };

        var json = JsonSerializer.Serialize(response);
        await context.Response.WriteAsync(json);
    }
}
