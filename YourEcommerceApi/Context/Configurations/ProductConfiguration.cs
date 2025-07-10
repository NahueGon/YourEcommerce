using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using YourEcommerceApi.Models;

namespace YourEcommerceApi.Context.Configurations;

public class ProductConfiguration  : IEntityTypeConfiguration<Product>
{
    public void Configure(EntityTypeBuilder<Product> builder)
    {
        builder.ToTable("Products");
        builder.HasKey(p => p.Id);

        builder.Property(p => p.Name)
            .IsRequired()
            .HasMaxLength(150);

        builder.Property(p => p.Description)
            .IsRequired(false)
            .HasMaxLength(250);

        builder.Property(p => p.Price)
            .IsRequired()
            .HasPrecision(18, 2);

        builder.Property(p => p.Stock)
            .IsRequired();

        builder.Property(p => p.Gender)
            .IsRequired();

        builder.Property(p => p.CreatedAt)
            .HasDefaultValueSql("GETDATE()");

        builder.Property(p => p.UpdatedAt)
             .HasDefaultValueSql("GETDATE()")
             .ValueGeneratedOnAddOrUpdate();

        builder.HasOne(p => p.Subcategory)
            .WithMany(sb => sb.Products)
            .HasForeignKey(p => p.SubcategoryId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.HasOne(p => p.Brand)
            .WithMany(sb => sb.Products)
            .HasForeignKey(p => p.BrandId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.HasDiscriminator<string>("ProductType")
            .HasValue<Shoe>("Shoe")
            .HasValue<Cloth>("Cloth")
            .HasValue<Accessory>("Accessory");
    }
    
}
