namespace asset_marketplace.Domain.Entities
{
    public class Order
    {
        public required Guid Id { get; set; }
        public required DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public required decimal Amount { get; set; }
        public required Guid BuyerId { get; set; }
        public User? Buyer { get; set; }
        public required Guid AssetId { get; set; }
        public Asset? Asset { get; set; }
    }
}
