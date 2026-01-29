using asset_marketplace.Application.DTOs;
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
            [FromQuery] int page = 1,
            [FromQuery] int size = 10,
            CancellationToken cancellationToken = default)
        {
            var users = await userService.GetAllAsync(page, size, cancellationToken);
            return Ok(users);
        }

        [HttpGet("{id:guid}")]
        public async Task<IActionResult> GetById(
            Guid id,
            CancellationToken cancellationToken = default)
        {
            var user = await userService.GetByIdAsync(id, cancellationToken);
            if (user is null)
            {
                return NotFound();
            }
            return Ok(user);
        }

        [HttpPost]
        public async Task<IActionResult> Create(CreateUserDto createUserDto, CancellationToken cancellationToken)
        {
            var result = await userService.CreateAsync(createUserDto, cancellationToken);
            return CreatedAtAction(nameof(GetById), new { id = result.Id }, result);
        }

        [HttpPut("{id:guid}")]
        public async Task<IActionResult> Update(Guid id, UpdateUserDto updateUserDto, CancellationToken cancellationToken)
        {
            var result = await userService.UpdateAsync(id, updateUserDto, cancellationToken);

            if (result is null) 
            {
                return NotFound();
            }
            return Ok(result);
        }
    }
}
