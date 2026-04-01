namespace AssetMarketplace.Application.DTOs;

public record UpdateUserDto
{
    public required string Email { get; init; }
}
