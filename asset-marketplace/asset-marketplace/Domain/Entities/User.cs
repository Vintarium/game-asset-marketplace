namespace asset_marketplace.Domain.Entities
{
    public class User
    {
        public Guid Id { get; set; }
        public required string Email { get; set; }
        public required string Password { get; set; }
        public Guid RoleId { get; set; }
        public Role Role { get; set; } = null!;
    }
}
