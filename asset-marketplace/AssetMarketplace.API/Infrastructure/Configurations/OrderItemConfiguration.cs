using AssetMarketplace.API.Infrastructure.Constants;
using AssetMarketplace.API.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AssetMarketplace.API.Infrastructure.Configurations;
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
