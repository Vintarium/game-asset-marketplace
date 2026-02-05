using asset_marketplace.Domain.Enums;

namespace asset_marketplace.Application.DTOs;
public record UserDto(
    Guid Id,
    string Email,
    UserRole Role
);
