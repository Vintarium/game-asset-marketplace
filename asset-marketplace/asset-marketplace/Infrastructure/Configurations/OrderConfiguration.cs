using asset_marketplace.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace asset_marketplace.Infrastructure.Configurations
{
    public class OrderConfiguration : IEntityTypeConfiguration<Order>
    {
        public void Configure(EntityTypeBuilder<Order> builder)
        {
            builder.HasOne(o => o.Buyer)
                   .WithMany(u => u.Orders)
                   .HasForeignKey(o => o.BuyerId)
                   .OnDelete(DeleteBehavior.Cascade);

            builder.HasOne(o => o.Asset)
                   .WithMany() 
                   .HasForeignKey(o => o.AssetId)
                   .OnDelete(DeleteBehavior.Restrict);

            builder.Property(o => o.Amount)
                   .HasColumnType("decimal(18,2)")
                   .IsRequired();
        }
    }
}