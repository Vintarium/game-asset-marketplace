using asset_marketplace.Domain.Constants;
using asset_marketplace.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace asset_marketplace.Infrastructure.Configurations
{
    public class OrderConfiguration : IEntityTypeConfiguration<Order>
    {
        public void Configure(EntityTypeBuilder<Order> builder)
        {
            builder.HasKey(order => order.Id);

            builder.Property(order => order.TotalAmount)
                .HasColumnType(ApplicationConstants.MoneyType)
                .IsRequired();

            builder.HasOne(order => order.Buyer)
                .WithMany(user => user.Orders)
                .HasForeignKey(order => order.BuyerId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasMany(order => order.Items)
                .WithOne(OrderItem => OrderItem.Order)
                .HasForeignKey(orderItem => orderItem.OrderId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
