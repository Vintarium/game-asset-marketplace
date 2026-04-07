using AssetMarketplace.Application.DTOs;
using AssetMarketplace.Application.Interfaces;
using AssetMarketplace.Domain.Constants;
using Microsoft.AspNetCore.Mvc;

namespace AssetMarketplace.API.Controllers;

public class UserController(IUserService userService) : BaseController
{
    [HttpGet]
    public async Task<IActionResult> GetAll(
        [FromQuery] int pageNumber = PaginationConstants.DefaultPageNumber,
        [FromQuery] int pageSize = PaginationConstants.DefaultPageSize,
        CancellationToken cancellationToken = default)
    {
        var allUsers = await userService.GetAllAsync(pageNumber, pageSize, cancellationToken);

        return HandleResult(allUsers);
    }

    [HttpGet("{id:guid}")]
    public async Task<IActionResult> GetById(
        Guid id,
        CancellationToken cancellationToken = default)
    {
        var user = await userService.GetByIdAsync(id, cancellationToken);

        return HandleResult(user);
    }

    [HttpPost]
    public async Task<IActionResult> Create(CreateUserDto createUserDto, CancellationToken cancellationToken = default)
    {
        var result = await userService.CreateAsync(createUserDto, cancellationToken);

        return HandleResult(result);
    }

    [HttpPut("{id:guid}")]
    public async Task<IActionResult> Update(Guid id, [FromBody] UpdateUserDto updateUserDto, CancellationToken cancellationToken = default)
    {
        var result = await userService.UpdateAsync(id, updateUserDto, cancellationToken);

        return HandleResult(result);
    }

    [HttpDelete("{id:guid}")]
    public async Task<IActionResult> Delete(Guid id, CancellationToken cancellationToken = default)
    {
        var result = await userService.DeleteAsync(id, cancellationToken);

        return HandleResult(result);
    }
}
