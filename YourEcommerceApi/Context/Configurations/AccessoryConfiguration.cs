using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using YourEcommerceApi.Models;

namespace YourEcommerceApi.Context.Configurations;

public class AccessoryConfiguration : IEntityTypeConfiguration<Accessory>
{
    public void Configure(EntityTypeBuilder<Accessory> builder)
    {
        // builder.ToTable("Accessories");

        builder.Property(a => a.Type)
            .IsRequired();
    }
}
