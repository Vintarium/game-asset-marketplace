using asset_marketplace.Application.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace asset_marketplace.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UserController(IUserService userService) : ControllerBase
    {
        [HttpGet]
        public async Task<IActionResult> GetAll(
            [FromQuery]int page = 1,
            [FromQuery] int size = 10,
            CancellationToken cancellationToken = default)
        {
            var users = await userService.GetAllAsync(page, size, cancellationToken);
            return Ok(users);
        }

        [HttpGet("{id:guid}")]
        public async Task<IActionResult> GetById(Guid id, CancellationToken cancellationToken = default)
        {
            var user = await userService.GetByIdAsync(id, cancellationToken);
            if (user is null)
            {
                return NotFound();
            }
            return Ok(user);
        }
    }
}
