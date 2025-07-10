using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using YourEcommerceApi.Models;

namespace YourEcommerceApi.Context.Configurations;

public class ClothConfiguration : IEntityTypeConfiguration<Cloth>
{
    public void Configure(EntityTypeBuilder<Cloth> builder)
    {
        // builder.ToTable("Clothes");

        builder.Property(c => c.Size)
            .IsRequired(false);

        builder.HasOne(c => c.Sport)
            .WithMany(s => s.Clothes)
            .HasForeignKey(c => c.SportId);  
    }
}
