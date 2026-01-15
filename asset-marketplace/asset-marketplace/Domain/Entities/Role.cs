namespace asset_marketplace.Domain.Entities
{
    public class Role
    {
        public Guid Id { get; set; }
        public required string RoleName { get; set; }
    }
}
