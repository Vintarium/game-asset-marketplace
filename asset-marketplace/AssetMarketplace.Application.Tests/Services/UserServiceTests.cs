using AssetMarketplace.Application.DTOs;
using AssetMarketplace.Application.Services;
using AssetMarketplace.Domain.Constants;
using AssetMarketplace.Domain.Entities;
using AssetMarketplace.Domain.Enums;
using AssetMarketplace.Domain.Interfaces;
using AutoMapper;
using FluentAssertions;
using Moq;

namespace AssetMarketplace.Application.Tests.Services;

public class UserServiceTests
{
    private readonly Mock<IUserRepository> _userRepositoryMock;
    private readonly Mock<IPasswordHasher> _passwordHasherMock;
    private readonly Mock<IMapper> _mapperMock;
    private readonly UserService _userService;

    public UserServiceTests()
    {
        _userRepositoryMock = new Mock<IUserRepository>();
        _passwordHasherMock = new Mock<IPasswordHasher>();
        _mapperMock = new Mock<IMapper>();

        _userService = new UserService(
            _userRepositoryMock.Object,
            _passwordHasherMock.Object,
            _mapperMock.Object);
    }

    [Fact]
    public async Task CreateAsync_WhenEmailIsUnique_ShouldReturnSuccess()
    {
        // Arrange
        var dto = new CreateUserDto { Email = "new@test.com", Password = "password", Role = UserRole.Seller };
        var user = new User { Id = Guid.NewGuid(), Email = dto.Email, Role = dto.Role, PasswordHash = "test_hash" };
        var expectedDto = new UserDto { Id = user.Id, Email = user.Email, Role = user.Role };

        _userRepositoryMock.Setup(x => x.GetByEmailAsync(dto.Email, It.IsAny<CancellationToken>()))
            .ReturnsAsync((User?)null);

        _mapperMock.Setup(m => m.Map<User>(dto)).Returns(user);
        _passwordHasherMock.Setup(h => h.HashPassword(dto.Password)).Returns("hashed_pass");
        _mapperMock.Setup(m => m.Map<UserDto>(user)).Returns(expectedDto);

        // Act
        var result = await _userService.CreateAsync(dto, CancellationToken.None);

        // Assert
        result.IsSuccess.Should().BeTrue();
        result.Value.Email.Should().Be(dto.Email);
        _userRepositoryMock.Verify(x => x.AddAsync(It.IsAny<User>(), It.IsAny<CancellationToken>()), Times.Once);
    }

    [Fact]
    public async Task CreateAsync_WhenEmailAlreadyExists_ShouldReturnFailure()
    {
        // Arrange
        var dto = new CreateUserDto { Email = "exists@test.com", Password = "password", Role = UserRole.Seller };
        var existingUser = new User { Email = dto.Email, PasswordHash = "test_hash", Role = dto.Role };

        _userRepositoryMock.Setup(x => x.GetByEmailAsync(dto.Email, It.IsAny<CancellationToken>()))
            .ReturnsAsync(existingUser);

        // Act
        var result = await _userService.CreateAsync(dto, CancellationToken.None);

        // Assert
        result.IsSuccess.Should().BeFalse();
        result.Error.Should().Be(UserErrors.EmailNotUnique);

        _userRepositoryMock.Verify(x => x.AddAsync(It.IsAny<User>(), It.IsAny<CancellationToken>()), Times.Never);
    }

    [Fact]
    public async Task GetByIdAsync_WhenUserDoesNotExist_ShouldReturnFailure()
    {
        // Arrange
        var userId = Guid.NewGuid();
        _userRepositoryMock.Setup(x => x.GetByIdAsync(userId, It.IsAny<CancellationToken>(), true))
            .ReturnsAsync((User?)null);

        // Act
        var result = await _userService.GetByIdAsync(userId, CancellationToken.None);

        // Assert
        result.IsSuccess.Should().BeFalse();
        result.Error.Should().Be(UserErrors.NotFound);
    }

    [Fact]
    public async Task GetByIdAsync_WhenUserExists_ShouldReturnSuccess()
    {
        // Arrange
        var userId = Guid.NewGuid();
        var user = new User { Id = userId, Email = "test@test.com", Role = UserRole.Seller, PasswordHash = "test_hash" };
        var userDto = new UserDto { Id = userId, Email = "test@test.com", Role = user.Role };

        _userRepositoryMock.Setup(x => x.GetByIdAsync(userId, It.IsAny<CancellationToken>(), true))
            .ReturnsAsync(user);

        _mapperMock.Setup(m => m.Map<UserDto>(user)).Returns(userDto);

        // Act
        var result = await _userService.GetByIdAsync(userId, CancellationToken.None);

        // Assert
        result.IsSuccess.Should().BeTrue();
        result.Value.Id.Should().Be(userId);
    }
}
