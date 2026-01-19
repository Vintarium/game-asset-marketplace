namespace asset_marketplace.Domain.Entities
{
    public class Order : BaseEntity
    {
        public required decimal TotalAmount { get; set; }
        public required Guid BuyerId { get; set; }
        public User? Buyer { get; set; }
        public ICollection<OrderItem> Items { get; set; } = new List<OrderItem>();

    }
}
