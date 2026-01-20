using asset_marketplace.Domain.Constants;
using asset_marketplace.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace asset_marketplace.Infrastructure.Configurations
{
    public class AssetConfiguration : IEntityTypeConfiguration<Asset>
    {
        public void Configure(EntityTypeBuilder<Asset> builder)
        {
            builder.HasKey(a => a.Id);

            builder.Property(a => a.Name)
                .HasMaxLength(ApplicationConstants.MaxNameLength)
                .IsRequired();

            builder.Property(a => a.Description)
                .HasMaxLength(ApplicationConstants.MaxDescriptionLength);

            builder.Property(a => a.Price)
                .HasColumnType(ApplicationConstants.MoneyType)
                .IsRequired();

            builder.Property(a => a.FilePath)
                .HasMaxLength(ApplicationConstants.MaxUrlLength)
                .IsRequired();

            builder.HasOne(a => a.Seller)
                .WithMany(u => u.Assets)
                .HasForeignKey(a => a.SellerId)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
