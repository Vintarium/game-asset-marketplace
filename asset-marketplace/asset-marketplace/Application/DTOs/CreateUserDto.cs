using asset_marketplace.Domain.Enums;

namespace asset_marketplace.Application.DTOs;
public record CreateUserDto
{
    public required string Email { get; init; }
    public required string Password { get; init; }
    public required UserRole Role { get; init; }
}
