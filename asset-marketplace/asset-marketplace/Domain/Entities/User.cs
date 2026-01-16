using asset_marketplace.Domain.Enums;

namespace asset_marketplace.Domain.Entities
{
    public class User
    {
        public required Guid Id { get; set; }
        public required string Email { get; set; }
        public required string PasswordHash { get; set; }
        public  UserRole Role { get; set; } = UserRole.None;
        public ICollection<Asset> Assets { get; set; } = new List<Asset>();
        public ICollection<Order> Orders { get; set; } = new List<Order>();
    }
}
