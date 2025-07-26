using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using YourEcommerceApi.Models.Products;

namespace YourEcommerceApi.Context.Configurations;
public class SportConfiguration : IEntityTypeConfiguration<Sport>
{
    public void Configure(EntityTypeBuilder<Sport> builder)
    {
        builder.ToTable("Sports");
        builder.HasKey(s => s.Id);

        builder.Property(s => s.Name)
            .IsRequired()
            .HasMaxLength(150);

        builder.Property(s => s.Description)
            .IsRequired(false)
            .HasMaxLength(250);

        builder.HasMany(s => s.Products)
            .WithOne(p => p.Sport)
            .HasForeignKey(p => p.SportId)
            .OnDelete(DeleteBehavior.Restrict);
    }
}
