using AssetMarketplace.API.Controllers;
using AssetMarketplace.Application.DTOs;
using AssetMarketplace.Application.Interfaces;
using AssetMarketplace.Domain.Abstractions;
using AssetMarketplace.Domain.Constants;
using AssetMarketplace.Domain.Enums;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc;
using Moq;
using Xunit;

namespace AssetMarketplace.API.Tests.Controllers;

public class UserControllerTests
{
    private readonly Mock<IUserService> _userServiceMock;
    private readonly UserController _controller;

    public UserControllerTests()
    {
        _userServiceMock = new Mock<IUserService>();
        _controller = new UserController(_userServiceMock.Object);
    }

    [Fact]
    public async Task GetById_WhenUserExists_ShouldReturnOk()
    {
        // Arrange
        var userId = Guid.NewGuid();
        var userDto = new UserDto { Id = userId, Email = "test@test.com", Role = UserRole.Seller };

        _userServiceMock
            .Setup(s => s.GetByIdAsync(userId, It.IsAny<CancellationToken>()))
            .ReturnsAsync(Result<UserDto>.Success(userDto));

        // Act 
        var result = await _controller.GetById(userId);

        // Assert 
        var okResult = result.Should().BeOfType<OkObjectResult>().Subject;
        okResult.Value.Should().BeEquivalentTo(userDto);
    }

    [Fact]
    public async Task GetById_WhenUserDoesNotExist_ShouldReturnNotFound()
    {
        // Arrange
        var userId = Guid.NewGuid();

        _userServiceMock
            .Setup(s => s.GetByIdAsync(userId, It.IsAny<CancellationToken>()))
            .ReturnsAsync(Result<UserDto>.Failure(UserErrors.NotFound));

        // Act
        var result = await _controller.GetById(userId);

        // Assert
        result.Should().BeOfType<NotFoundObjectResult>();
    }

    [Fact]
    public async Task Create_WhenEmailIsAlreadyTaken_ShouldReturnConflict()
    {
        // Arrange
        var dto = new CreateUserDto { Email = "duplicate@test.com", Password = "123", Role = UserRole.Seller };

        _userServiceMock
            .Setup(s => s.CreateAsync(dto, It.IsAny<CancellationToken>()))
            .ReturnsAsync(Result<UserDto>.Failure(UserErrors.EmailNotUnique));

        // Act
        var result = await _controller.Create(dto);

        // Assert
        result.Should().BeOfType<ConflictObjectResult>();
    }
}
