namespace asset_marketplace.Domain.Entities
{
    public class OrderItem : BaseEntity
    {
        public required Guid OrderId { get; set; }
        public Order Order { get; set; } = null!;
        public required Guid AssetId { get; set; }
        public Asset Asset { get; set; } = null!;
        public required decimal UnitPrice { get; set; }
    }
}
