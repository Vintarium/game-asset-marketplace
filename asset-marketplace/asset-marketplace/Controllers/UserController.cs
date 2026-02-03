using asset_marketplace.Application.DTOs;
using asset_marketplace.Application.Interfaces;
using asset_marketplace.Domain.Constants;
using Microsoft.AspNetCore.Mvc;

namespace asset_marketplace.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UserController(IUserService userService) : ControllerBase
    {
        [HttpGet]
        public async Task<List<ResponseUserDto>> GetAll(
            [FromQuery] int page = PaginationConstants.DefaultPageNumber,
            [FromQuery] int size = PaginationConstants.DefaultPageSize,
            CancellationToken cancellationToken = default)
        {
            return await userService.GetAllAsync(page, size, cancellationToken);
        }

        [HttpGet("{id:guid}")]
        public async Task<ActionResult<ResponseUserDto>> GetById(
            Guid id,
            CancellationToken cancellationToken = default)
        {
            var user = await userService.GetByIdAsync(id, cancellationToken);
            if (user is null)
            {
                return NotFound();
            }
            return user;
        }

        [HttpPost]
        public async Task<IActionResult> Create(CreateUserDto createUserDto, CancellationToken cancellationToken)
        {
            var result = await userService.CreateAsync(createUserDto, cancellationToken);
            return CreatedAtAction(nameof(GetById), new { id = result.Id }, result);
        }

        [HttpPut("{id:guid}")]
        public async Task<ActionResult<ResponseUserDto>> Update(Guid id, UpdateUserDto updateUserDto, CancellationToken cancellationToken)
        {
            var result = await userService.UpdateAsync(id, updateUserDto, cancellationToken);

            if (result is null)
            {
                return NotFound();
            }
            return result;
        }

        [HttpDelete("{id:guid}")]
        public async Task<IActionResult> Delete(Guid id, CancellationToken cancellationToken)
        {
            var deleted = await userService.DeleteAsync(id, cancellationToken);
            if (!deleted)
            {
                return NotFound();
            }
            return NoContent();
        }
    }
}
