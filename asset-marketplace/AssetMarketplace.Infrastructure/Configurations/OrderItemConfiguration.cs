using AssetMarketplace.Infrastructure.Constants;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using AssetMarketplace.Domain.Entities;

namespace AssetMarketplace.Infrastructure.Configurations;
public class OrderItemConfiguration : IEntityTypeConfiguration<OrderItem>
{
    public void Configure(EntityTypeBuilder<OrderItem> builder)
    {
        builder.HasKey(orderItem => orderItem.Id);

        builder.Property(orderItem => orderItem.UnitPrice)
            .HasColumnType(DbConstants.MoneyType)
            .IsRequired();

        builder.HasOne(orderItem => orderItem.Asset)
            .WithMany()
            .HasForeignKey(orderItem => orderItem.AssetId)
            .OnDelete(DeleteBehavior.Restrict);
    }
}
