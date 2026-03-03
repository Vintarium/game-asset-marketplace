using AssetMarketplace.Application.DTOs;
using AssetMarketplace.Application.Interfaces;
using AssetMarketplace.Domain.Constants;
using FluentValidation;
using Microsoft.AspNetCore.Mvc;

namespace AssetMarketplace.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class UserController(IUserService userService, IValidator<CreateUserDto> _validator) : ControllerBase
{
    [HttpGet]
    public async Task<ActionResult<IReadOnlyCollection<UserDto>>> GetAll(
        [FromQuery] int page = PaginationConstants.DefaultPageNumber,
        [FromQuery] int size = PaginationConstants.DefaultPageSize,
        CancellationToken cancellationToken = default)
    {
        return Ok(await userService.GetAllAsync(page, size, cancellationToken));
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
    public async Task<ActionResult<UserDto>> Create(CreateUserDto createUserDto, CancellationToken cancellationToken)
    {
        var validationResult = await _validator.ValidateAsync(createUserDto, cancellationToken);
        if (!validationResult.IsValid)
        {
            return BadRequest(validationResult.Errors);
        }

        var result = await userService.CreateAsync(createUserDto, cancellationToken);

        return Ok(result);
    }

    [HttpPut("{id:guid}")]
    public async Task<ActionResult<UserDto>> Update(Guid id, [FromBody] UpdateUserDto updateUserDto, CancellationToken cancellationToken)
    {
        var result = await userService.UpdateAsync(id, updateUserDto, cancellationToken);

        return result is not null ? Ok(result) : NotFound();
    }

    [HttpDelete("{id:guid}")]
    public async Task<ActionResult> Delete(Guid id, CancellationToken cancellationToken)
    {
        var deleted = await userService.DeleteAsync(id, cancellationToken);

        return deleted ? NoContent() : NotFound();
    }
}
