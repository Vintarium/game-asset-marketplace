using asset_marketplace.Domain.Constants;
using asset_marketplace.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace asset_marketplace.Infrastructure.Configurations
{
    public class OrderItemConfiguration : IEntityTypeConfiguration<OrderItem>
    {
        public void Configure(EntityTypeBuilder<OrderItem> builder)
        {
            builder.HasKey(oi => oi.Id);

            builder.Property(oi => oi.UnitPrice)
                .HasColumnType(ApplicationConstants.MoneyType)
                .IsRequired();

            builder.HasOne(oi => oi.Asset)
                .WithMany()
                .HasForeignKey(oi => oi.AssetId)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
