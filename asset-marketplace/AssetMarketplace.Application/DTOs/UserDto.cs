using AssetMarketplace.Domain.Enums;

namespace AssetMarketplace.Application.DTOs;
public record UserDto
{
    public required Guid Id { get; init; }
    public required string Email { get; init; }
    public required UserRole Role { get; init; }
}
