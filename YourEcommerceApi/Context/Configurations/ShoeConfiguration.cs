using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using YourEcommerceApi.Models;

namespace YourEcommerceApi.Context.Configurations;

public class ShoeConfiguration : IEntityTypeConfiguration<Shoe>
{
    public void Configure(EntityTypeBuilder<Shoe> builder)
    {
        // builder.ToTable("Shoes");

        builder.Property(sh => sh.Size)
            .IsRequired(false);

        builder.Property(sh => sh.Model)
            .IsRequired(false);

        builder.HasOne(sh => sh.Sport)
            .WithMany(s => s.Shoes)
            .HasForeignKey(sh => sh.SportId);  
    }
}