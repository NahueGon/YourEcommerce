using Microsoft.EntityFrameworkCore;
using YourEcommerceApi.Context;
using YourEcommerceApi.DTOs.Product;
using YourEcommerceApi.Models;
using YourEcommerceApi.Services.Interfaces;

namespace YourEcommerceApi.Services;

public class ProductService : IProductService
{
    AppDbContext _context;

    public ProductService(AppDbContext dbContext)
    {
        _context = dbContext;
    }

    public async Task<IEnumerable<ProductResponseDto>> GetAll()
    {
        var clothes = await _context.Products
            .OfType<Cloth>()
            .Include(p => p.Brand)
            .Include(p => p.Subcategory)
            .Include(p => p.Sport)
            .ToListAsync();

        var shoes = await _context.Products
            .OfType<Shoe>()
            .Include(p => p.Brand)
            .Include(p => p.Subcategory)
            .Include(p => p.Sport)
            .ToListAsync();

        var accessories = await _context.Products
            .OfType<Accessory>()
            .Include(p => p.Brand)
            .Include(p => p.Subcategory)
            .ToListAsync();

        var products = clothes.Cast<Product>()
            .Concat(shoes)
            .Concat(accessories)
            .ToList();

        var response = products.Select(p => p.ToDto()).ToList();

        return response;
    }

    public async Task<ProductResponseDto?> Get(int id)
    {
        var product = await _context.Products
            .FirstOrDefaultAsync(p => p.Id == id);

        if (product == null)
            return null;

        return product.ToDto();
    }

    public async Task<IEnumerable<ProductResponseDto>> GetAllByType(string type)
    {
        List<Product> products;

        switch (type.ToLower())
        {
            case "cloth":
                products = await _context.Products
                    .OfType<Cloth>()
                    .Include(p => p.Brand)
                    .Include(p => p.Subcategory)
                    .Include(p => p.Sport)
                    .Cast<Product>()
                    .ToListAsync();
                break;

            case "shoe":
                products = await _context.Products
                    .OfType<Shoe>()
                    .Include(p => p.Brand)
                    .Include(p => p.Subcategory)
                    .Include(p => p.Sport)
                    .Cast<Product>()
                    .ToListAsync();
                break;

            case "accessory":
                products = await _context.Products
                    .Where(p => !(p is Cloth) && !(p is Shoe))
                    .Include(p => p.Brand)
                    .Include(p => p.Subcategory)
                    .ToListAsync();
                break;

            default:
                products = new List<Product>();
                break;
        }

        return products.Select(p => p.ToDto()).ToList();
    }

    public async Task<ProductResponseDto> SaveCloth(ClothCreateDto clothDto)
    {
        var subcategory = await _context.SubCategories.FindAsync(clothDto.SubcategoryId)
            ?? throw new Exception("Subcategoría no encontrada");

        var brand = await _context.Brands.FindAsync(clothDto.BrandId)
            ?? throw new Exception("Marca no encontrada");

        var sport = await _context.Sports.FindAsync(clothDto.SportId)
            ?? throw new Exception("Deporte no encontrado");

        var cloth = new Cloth
        {
            Name = clothDto.Name,
            Description = clothDto.Description,
            Price = clothDto.Price,
            Stock = clothDto.Stock,
            Gender = clothDto.Gender,
            Size = clothDto.Size,
            BrandId = clothDto.BrandId,
            SubcategoryId = clothDto.SubcategoryId,
            SportId = clothDto.SportId,
            Brand = brand,
            Subcategory = subcategory,
            Sport = sport
        };

        _context.Products.Add(cloth);
        await _context.SaveChangesAsync();
        return cloth.ToDto();
    }

    public async Task<ProductResponseDto> SaveShoe(ShoeCreateDto shoeDto)
    {
        var subcategory = await _context.SubCategories.FindAsync(shoeDto.SubcategoryId)
            ?? throw new Exception("Subcategoría no encontrada");

        var brand = await _context.Brands.FindAsync(shoeDto.BrandId)
            ?? throw new Exception("Marca no encontrada");

        var sport = await _context.Sports.FindAsync(shoeDto.SportId)
            ?? throw new Exception("Deporte no encontrado");

        var shoe = new Shoe
        {
            Name = shoeDto.Name,
            Description = shoeDto.Description,
            Price = shoeDto.Price,
            Stock = shoeDto.Stock,
            Gender = shoeDto.Gender,
            Model = shoeDto.Model,
            Size = shoeDto.Size,
            BrandId = shoeDto.BrandId,
            SubcategoryId = shoeDto.SubcategoryId,
            SportId = shoeDto.SportId,
            Brand = brand,
            Subcategory = subcategory,
            Sport = sport
        };

        _context.Products.Add(shoe);
        await _context.SaveChangesAsync();
        return shoe.ToDto();
    }

    public async Task<ProductResponseDto> SaveAccessory(AccessoryCreateDto accessoryDto)
    {
        var subcategory = await _context.SubCategories.FindAsync(accessoryDto.SubcategoryId)
            ?? throw new Exception("Subcategoría no encontrada");

        var brand = await _context.Brands.FindAsync(accessoryDto.BrandId)
            ?? throw new Exception("Marca no encontrada");

        var accessory = new Accessory
        {
            Name = accessoryDto.Name,
            Description = accessoryDto.Description,
            Price = accessoryDto.Price,
            Stock = accessoryDto.Stock,
            Gender = accessoryDto.Gender,
            BrandId = accessoryDto.BrandId,
            SubcategoryId = accessoryDto.SubcategoryId,
            Type = accessoryDto.Type,
            Brand = brand,
            Subcategory = subcategory
        };

        _context.Products.Add(accessory);
        await _context.SaveChangesAsync();
        return accessory.ToDto();
    }

    public async Task<bool> Delete(int id)
    {
        var currentProduct = await _context.Products.FindAsync(id);

        if (currentProduct == null)
            return false;

        _context.Remove(currentProduct);
        await _context.SaveChangesAsync();

        return true;
    }
}
