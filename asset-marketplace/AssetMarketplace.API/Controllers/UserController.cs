using AssetMarketplace.API.Application.DTOs;
using AssetMarketplace.API.Application.Interfaces;
using AssetMarketplace.API.Domain.Constants;
using Microsoft.AspNetCore.Mvc;

namespace AssetMarketplace.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class UserController(IUserService userService) : ControllerBase
{
    [HttpGet]
    public async Task<IReadOnlyCollection<UserDto>> GetAll(
        [FromQuery] int page = PaginationConstants.DefaultPageNumber,
        [FromQuery] int size = PaginationConstants.DefaultPageSize,
        CancellationToken cancellationToken = default)
    {
        return await userService.GetAllAsync(page, size, cancellationToken);
    }

    [HttpGet("{id:guid}")]
    public async Task<ActionResult<UserDto>> GetById(
        Guid id,
        CancellationToken cancellationToken = default)
    {
        var user = await userService.GetByIdAsync(id, cancellationToken);

        return user is not null ? Ok(user) : NotFound();
    }

    [HttpPost]
    public async Task<IActionResult> Create(CreateUserDto createUserDto, CancellationToken cancellationToken)
    {
        var result = await userService.CreateAsync(createUserDto, cancellationToken);

        return Ok(result);
    }

    [HttpPut("{id:guid}")]
    public async Task<ActionResult<UserDto>> Update(Guid id, [FromBody] UpdateUserDto updateUserDto, CancellationToken cancellationToken)
    {
        var updatedDto = updateUserDto with { Id = id };
        var result = await userService.UpdateAsync(updatedDto, cancellationToken);

        return result is not null ? Ok(result) : NotFound();
    }

    [HttpDelete("{id:guid}")]
    public async Task<IActionResult> Delete(Guid id, CancellationToken cancellationToken)
    {
        var deleted = await userService.DeleteAsync(id, cancellationToken);

        return deleted ? NoContent() : NotFound();
    }
}
