using asset_marketplace.Domain.Enums;

namespace asset_marketplace.Application.DTOs
{
    public record ResponseUserDto(
        Guid Id,
        string Email,
        UserRole Role
    );
}
