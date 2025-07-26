using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using YourEcommerceApi.Models.Products;

namespace YourEcommerceApi.Context.Configurations;
public class ProductColorConfiguration : IEntityTypeConfiguration<ProductColor>
{
    public void Configure(EntityTypeBuilder<ProductColor> builder)
    {
        builder.ToTable("ProductColors");
        builder.HasKey(pc => pc.Id);

        builder.Property(pc => pc.Name)
            .HasMaxLength(250);

        builder.HasOne(pc => pc.ProductVariant)
            .WithMany(pv => pv.Colors)
            .HasForeignKey(pc => pc.ProductVariantId);
    }
}