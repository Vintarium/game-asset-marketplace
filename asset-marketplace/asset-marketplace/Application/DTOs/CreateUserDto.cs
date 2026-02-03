using asset_marketplace.Domain.Enums;

namespace asset_marketplace.Application.DTOs;
public record CreateUserDto(
    string Email,
    string Password,
    UserRole Role
);
