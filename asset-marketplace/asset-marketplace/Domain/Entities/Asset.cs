namespace asset_marketplace.Domain.Entities
{
    public class Asset
    {
        public required Guid Id { get; set; }
        public required string Name { get; set; }
        public string? Description { get; set; }
        public required decimal Price { get; set; }
        public required string FilePath { get; set; }
        public required Guid SellerId { get; set; }
        public User? Buyer { get; set; }
    }
}
