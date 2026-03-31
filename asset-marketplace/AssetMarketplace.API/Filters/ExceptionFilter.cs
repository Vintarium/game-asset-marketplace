using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace AssetMarketplace.API.Filters;

public class ExceptionFilter : IExceptionFilter
{
    public void OnException(ExceptionContext context)
    {
        context.Result = context.Exception switch
        {
            InvalidOperationException exception => new BadRequestObjectResult(new
            {
                error = exception.Message,
                timestamp = DateTime.UtcNow
            }),

            _ => null
        };

        if (context.Result is not null)
        {
            context.ExceptionHandled = true;
        }
    }
}
