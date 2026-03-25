using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace AssetMarketplace.API.Filters;

public class ExeptionFilter : IExceptionFilter
{
    public void OnException(ExceptionContext context)
    {
        if (context.Exception is InvalidOperationException)
        {
            context.Result = new BadRequestObjectResult(new
            {
                error = context.Exception.Message,
                timestamp = DateTime.UtcNow
            });
        }
    }
}
