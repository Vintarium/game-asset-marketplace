using asset_marketplace.Domain.Constants;
using asset_marketplace.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace asset_marketplace.Infrastructure.Configurations;
public class AssetConfiguration : IEntityTypeConfiguration<Asset>
{
    public void Configure(EntityTypeBuilder<Asset> builder)
    {
        builder.HasKey(asset => asset.Id);

        builder.Property(asset => asset.Name)
            .HasMaxLength(ApplicationConstants.MaxNameLength)
            .IsRequired();

        builder.Property(asset => asset.Description)
            .HasMaxLength(ApplicationConstants.MaxDescriptionLength);

        builder.Property(asset => asset.Price)
            .HasColumnType(ApplicationConstants.MoneyType)
            .IsRequired();

        builder.Property(asset => asset.FilePath)
            .HasMaxLength(ApplicationConstants.MaxUrlLength)
            .IsRequired();

        builder.HasOne(asset => asset.Seller)
            .WithMany(user => user.Assets)
            .HasForeignKey(asset => asset.SellerId)
            .OnDelete(DeleteBehavior.Restrict);
    }
}
