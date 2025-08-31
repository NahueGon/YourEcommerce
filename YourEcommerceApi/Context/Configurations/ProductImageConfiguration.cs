using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using YourEcommerceApi.Models.Products;

namespace YourEcommerceApi.Context.Configurations;
public class ProductImageConfiguration : IEntityTypeConfiguration<ProductImage>
{
    public void Configure(EntityTypeBuilder<ProductImage> builder)
    {
        builder.ToTable("ProductImages");
        builder.HasKey(pi => pi.Id);

        builder.Property(pi => pi.ImageUrl)
            .HasMaxLength(250)
            .IsRequired();

         builder.HasOne(pi => pi.ProductVariant)
            .WithMany(pv => pv.Images)
            .HasForeignKey(pi => pi.ProductVariantId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}