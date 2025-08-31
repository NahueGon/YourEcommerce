using Microsoft.EntityFrameworkCore;
using YourEcommerceApi.Models.Users;
using YourEcommerceApi.Models.Products;

namespace YourEcommerceApi.Context;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

    // Users
    public DbSet<User> Users { get; set; }

    // Products
    public DbSet<Product> Products { get; set; }
    public DbSet<ProductVariant> ProductVariants { get; set; }
    public DbSet<ProductColor> ProductColors { get; set; }
    public DbSet<ProductImage> ProductImages { get; set; }
    public DbSet<Category> Categories { get; set; }
    public DbSet<CategoryGender> CategoryGenders { get; set; }
    public DbSet<Brand> Brands { get; set; }
    public DbSet<Gender> Genders { get; set; }
    public DbSet<Sport> Sports { get; set; }

    // Tags y Attributes
    public DbSet<Tag> Tags { get; set; }
    public DbSet<ProductTag> ProductTags { get; set; }
    public DbSet<ProductAttribute> ProductAttributes { get; set; }  


    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(AppDbContext).Assembly);
    }
}
