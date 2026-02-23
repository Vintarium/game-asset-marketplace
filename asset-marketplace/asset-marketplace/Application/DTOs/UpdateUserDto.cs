using asset_marketplace.Domain.Enums;

namespace asset_marketplace.Application.DTOs;
public record UpdateUserDto
{
    public required Guid Id { get; init; }
    public required string Email { get; init; }
    public required UserRole Role { get; init; }
}
