using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using YourEcommerceApi.Models.Products;

namespace YourEcommerceApi.Context.Configurations;
public class TagConfiguration : IEntityTypeConfiguration<Tag>
{
    public void Configure(EntityTypeBuilder<Tag> builder)
    {
        builder.ToTable("Tags");
        builder.HasKey(t => t.Id);

        builder.Property(t => t.Name)
            .IsRequired()
            .HasMaxLength(150);

        builder.Property(t => t.Group);

        builder.HasMany(t => t.ProductTags)
            .WithOne(pt => pt.Tag)
            .HasForeignKey(p => p.TagId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}