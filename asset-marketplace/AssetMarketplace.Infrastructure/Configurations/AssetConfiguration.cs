using AssetMarketplace.Infrastructure.Constants;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using AssetMarketplace.Domain.Constants;
using AssetMarketplace.Domain.Entities;

namespace AssetMarketplace.Infrastructure.Configurations;
public class AssetConfiguration : IEntityTypeConfiguration<Asset>
{
    public void Configure(EntityTypeBuilder<Asset> builder)
    {
        builder.HasKey(asset => asset.Id);

        builder.Property(asset => asset.Name)
            .HasMaxLength(ValidationConstants.MaxNameLength)
            .IsRequired();

        builder.Property(asset => asset.Description)
            .HasMaxLength(ValidationConstants.MaxDescriptionLength);

        builder.Property(asset => asset.Price)
            .HasColumnType(DbConstants.MoneyType)
            .IsRequired();

        builder.Property(asset => asset.FilePath)
            .HasMaxLength(ValidationConstants.MaxUrlLength)
            .IsRequired();

        builder.HasOne(asset => asset.Seller)
            .WithMany(user => user.Assets)
            .HasForeignKey(asset => asset.SellerId)
            .OnDelete(DeleteBehavior.Restrict);
    }
}
