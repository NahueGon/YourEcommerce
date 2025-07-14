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
        var products = await _context.Products.ToListAsync();

        return products.Select(p => new ProductResponseDto
        {
            Id = p.Id,
            Name = p.Name,
            Description = p.Description,
            Price = p.Price,
            Stock = p.Stock
        });
    }

    public async Task<ProductResponseDto?> Get(int id)
    {
        var product = await _context.Products.FirstOrDefaultAsync(p => p.Id == id);

        if (product == null)
            return null;

        return new ProductResponseDto
        {
            Id = product.Id,
            Name = product.Name,
            Description = product.Description,
            Price = product.Price,
            Stock = product.Stock
        };
    }

    public async Task<ProductResponseDto> SaveAccessory(AccessoryCreateDto accesoryDto)
    {
        var brand = await _context.Brands.FindAsync(accesoryDto.BrandId);

        if (brand == null)
            throw new Exception("Marca no encontrada");

        var accessory = new Accessory
        {
            Name = accesoryDto.Name,
            Description = accesoryDto.Description,
            Price = accesoryDto.Price,
            Stock = accesoryDto.Stock,
            BrandId = accesoryDto.BrandId,
            SubcategoryId = accesoryDto.SubcategoryId,
            Gender = accesoryDto.Gender,
            Type = accesoryDto.Type
        };

        _context.Products.Add(accessory);
        await _context.SaveChangesAsync();

        return accessory.ToDto();
    }

    public async Task<ProductResponseDto> SaveShoe(ShoeCreateDto shoeDto)
    {
        var brand = await _context.Brands.FindAsync(shoeDto.BrandId);

        if (brand == null)
            throw new Exception("Marca no encontrada");

        var sport = await _context.Sports.FindAsync(shoeDto.SportId);

        if (sport == null)
            throw new Exception("Deporte no encontrado");

        var shoe = new Shoe
        {
            Name = shoeDto.Name,
            Description = shoeDto.Description,
            Price = shoeDto.Price,
            Stock = shoeDto.Stock,
            BrandId = shoeDto.BrandId,
            SubcategoryId = shoeDto.SubcategoryId,
            Gender = shoeDto.Gender,
        };

        _context.Products.Add(shoe);
        await _context.SaveChangesAsync();

        return shoe.ToDto();
    }

    public async Task<ProductResponseDto> SaveCloth(ClothCreateDto clothDto)
    {
        var brand = await _context.Brands.FindAsync(clothDto.BrandId);

        if (brand == null)
            throw new Exception("Marca no encontrada");

        var sport = await _context.Sports.FindAsync(clothDto.SportId);

        if (sport == null)
            throw new Exception("Deporte no encontrado");

        var cloth = new Cloth
        {
            Name = clothDto.Name,
            Description = clothDto.Description,
            Price = clothDto.Price,
            Stock = clothDto.Stock,
            BrandId = clothDto.BrandId,
            SubcategoryId = clothDto.SubcategoryId,
            Gender = clothDto.Gender,
            SportId = clothDto.SportId
        };

        _context.Products.Add(cloth);
        await _context.SaveChangesAsync();

        return cloth.ToDto();
    }
    
}
