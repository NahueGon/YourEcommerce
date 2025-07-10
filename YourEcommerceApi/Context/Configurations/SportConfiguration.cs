using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using YourEcommerceApi.Models;

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

        builder.HasMany(s => s.Shoes)
            .WithOne(sh => sh.Sport)
            .HasForeignKey(sh => sh.SportId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.HasMany(s => s.Clothes)
            .WithOne(c => c.Sport)
            .HasForeignKey(c => c.SportId)
            .OnDelete(DeleteBehavior.Restrict);
    }
}
