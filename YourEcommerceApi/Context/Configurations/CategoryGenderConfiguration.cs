using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using YourEcommerceApi.Models.Products;

namespace YourEcommerceApi.Context.Configurations;

public class CategoryGenderConfiguration : IEntityTypeConfiguration<CategoryGender>
{
    public void Configure(EntityTypeBuilder<CategoryGender> builder)
    {
        builder.ToTable("CategoryGenders");
        builder.HasKey(cg => cg.Id);
        builder.Property(cg => cg.Id).ValueGeneratedOnAdd();

        builder.Property(cg => cg.CategoryGenderImage)
            .HasMaxLength(250);

        builder.HasOne(cg => cg.Category)
            .WithMany(c => c.CategoryGenders)
            .HasForeignKey(cg => cg.CategoryId);

        builder.HasOne(cg => cg.Gender)
            .WithMany(g => g.CategoryGenders)
            .HasForeignKey(cg => cg.GenderId);
    }
}