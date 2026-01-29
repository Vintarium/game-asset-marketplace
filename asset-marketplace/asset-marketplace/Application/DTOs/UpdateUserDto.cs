using asset_marketplace.Domain.Enums;

namespace asset_marketplace.Application.DTOs
{
    public record UpdateUserDto(
        string Email,
        UserRole Role
    );
}
