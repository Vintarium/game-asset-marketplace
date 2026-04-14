using AssetMarketplace.Application.Interfaces;
using System.Net;
using System.Text.Json;

namespace AssetMarketplace.API.Middleware;

public class GlobalExceptionMiddleware(RequestDelegate next, ILogger<GlobalExceptionMiddleware> logger, IDateTimeProvider dateTimeProvider)
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
                exception, "An unhandled exception has occurred while executing the request. Method: {Method}, Path: {Path}",
                context.Request.Method,
                context.Request.Path);

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
            timestamp = dateTimeProvider.UtcNow
        };

        var json = JsonSerializer.Serialize(response);
        await context.Response.WriteAsync(json);
    }
}
