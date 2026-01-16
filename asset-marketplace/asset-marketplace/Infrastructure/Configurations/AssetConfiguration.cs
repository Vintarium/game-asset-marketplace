using asset_marketplace.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace asset_marketplace.Infrastructure.Configurations
{
    public class AssetConfiguration : IEntityTypeConfiguration<Asset>
    {
        public void Configure(EntityTypeBuilder<Asset> builder)
        {
            builder.HasOne(a => a.Seller)
               .WithMany(u => u.Assets)
               .HasForeignKey(a => a.SellerId)
               .OnDelete(DeleteBehavior.Cascade);

            builder.Property(a => a.Name)
                   .HasMaxLength(100)
                   .IsRequired();

            builder.Property(a => a.FilePath)
                   .HasMaxLength(500);

            builder.Property(a => a.Price)
                   .HasColumnType("decimal(18,2)");
        }
    }
}