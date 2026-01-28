
namespace asset_marketplace.Application.DTOs
{
    public record UserResponseDto(
        Guid Id,
        string Email,
        string Role
    );
}
