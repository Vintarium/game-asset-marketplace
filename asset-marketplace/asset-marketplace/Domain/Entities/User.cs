using asset_marketplace.Domain.Enums;

namespace asset_marketplace.Domain.Entities
{
    public class User
    {
        public required Guid Id { get; set; }
        public required string Email { get; set; }
        public required string Password { get; set; }
        public required Guid RoleId { get; set; }
        public UserRole Role { get; set; } = UserRole.User;
    }
}
