using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using YourEcommerceApi.Models;

namespace YourEcommerceApi.Context.Configurations;

public class UserConfiguration : IEntityTypeConfiguration<User>
{
    public void Configure(EntityTypeBuilder<User> builder)
    {
       builder.ToTable("Users");
       builder.HasKey(u => u.Id);

       builder.Property(u => u.Name)
            .IsRequired()
            .HasMaxLength(100);

       builder.Property(u => u.Email)
            .IsRequired()
            .HasMaxLength(150);

       builder.Property(u => u.Password)
            .IsRequired();

       builder.Property(u => u.Role)
            .IsRequired();

       builder.Property(u => u.PhoneNumber)
            .HasMaxLength(20) 
            .IsRequired(false);

       builder.Property(u => u.Address)
            .HasMaxLength(200)
            .IsRequired(false);
               
       builder.Property(u => u.CreatedAt)
            .HasDefaultValueSql("GETDATE()");

       builder.Property(u => u.UpdatedAt)
            .HasDefaultValueSql("GETDATE()")
            .ValueGeneratedOnAddOrUpdate();       
    }
}
