using AssetMarketplace.Domain.Abstractions;
using AssetMarketplace.Domain.Constants;
using Microsoft.AspNetCore.Mvc;

namespace AssetMarketplace.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public abstract class BaseController : ControllerBase
{

    protected IActionResult HandleResult<T>(Result<T> result)
    {
        if (result.IsSuccess)
        {
            return Ok(result.Value);
        }

        return MapErrorToResponse(result.Error);
    }

    protected IActionResult HandleResult(Result result)
    {
        if (result.IsSuccess)
        {
            return NoContent();
        }

        return MapErrorToResponse(result.Error);
    }

    private IActionResult MapErrorToResponse(Error error)
    {
        return error switch
        {
            var e when e == UserErrors.NotFound =>
                NotFound(new { error = e.Code, message = e.Message }),

            var e when e == UserErrors.EmailNotUnique =>
                Conflict(new { error = e.Code, message = e.Message }),

            _ => BadRequest(new { error = error.Code, message = error.Message })
        };
    }
}
