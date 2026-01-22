using asset_marketplace.Domain.Constants;
using asset_marketplace.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace asset_marketplace.Infrastructure.Configurations;
public class OrderItemConfiguration : IEntityTypeConfiguration<OrderItem>
{
    public void Configure(EntityTypeBuilder<OrderItem> builder)
    {
        builder.HasKey(orderItem => orderItem.Id);

        builder.Property(orderItem => orderItem.UnitPrice)
            .HasColumnType(ApplicationConstants.MoneyType)
            .IsRequired();

        builder.HasOne(orderItem => orderItem.Asset)
            .WithMany()
            .HasForeignKey(orderItem => orderItem.AssetId)
            .OnDelete(DeleteBehavior.Restrict);
    }
}
