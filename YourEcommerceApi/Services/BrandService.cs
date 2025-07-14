using Microsoft.EntityFrameworkCore;
using YourEcommerceApi.Context;
using YourEcommerceApi.DTOs.Brand;
using YourEcommerceApi.DTOs.Product;
using YourEcommerceApi.Models;
using YourEcommerceApi.Services.Interfaces;

namespace YourEcommerceApi.Services;

public class BrandService : IBrandService
{
    AppDbContext _context;

    public BrandService(AppDbContext dbContext)
    {
        _context = dbContext;
    }

    public async Task<IEnumerable<BrandResponseDto>> GetAll()
    {
        var brands = await _context.Brands
            .Include(b => b.Products)
            .ToListAsync();

        return brands.Select(b => new BrandResponseDto
        {
            Id = b.Id,
            Name = b.Name,
            Description = b.Description,
            Products = b.Products?
                .Select(p => new ProductResponseDto
                {
                    Id = p.Id,
                    Name = p.Name,
                    Description = p.Description,
                    Price = p.Price,
                    Stock = p.Stock
                }).ToList() ?? new List<ProductResponseDto>()
        });
    }

    public async Task<BrandResponseDto?> Get(int id)
    {
        var brand = await _context.Brands
            .Include(b => b.Products)
            .FirstOrDefaultAsync(b => b.Id == id);

        if (brand == null)
            return null;

        return new BrandResponseDto
        {
            Id = brand.Id,
            Name = brand.Name,
            Description = brand.Description,
            Products = brand.Products?
                .Select(p => new ProductResponseDto
                {
                    Id = p.Id,
                    Name = p.Name,
                    Description = p.Description,
                    Price = p.Price,
                    Stock = p.Stock
                }).ToList() ?? new List<ProductResponseDto>()
        };
    }

    public async Task<BrandResponseDto> Save(BrandCreateDto brandDto)
    {
        var brand = new Brand
        {
            Name = brandDto.Name,
            Description = brandDto.Description,
        };

        _context.Brands.Add(brand);
        await _context.SaveChangesAsync();

        return new BrandResponseDto
        {
            Id = brand.Id,
            Name = brand.Name,
            Description = brand.Description,
        };
    }

    public async Task<bool> Update(int id, BrandUpdateDto brandDto)
    {
        var currentBrand = await _context.Brands.FindAsync(id);

        if (currentBrand == null)
            return false;

        currentBrand.Name = brandDto.Name;
        currentBrand.Description = brandDto.Description;

        await _context.SaveChangesAsync();

        return true;
    }

    public async Task<bool> Delete(int id)
    {
        var currentBrand = await _context.Brands.FindAsync(id);

        if (currentBrand == null)
            return false;
            
        _context.Remove(currentBrand);
        await _context.SaveChangesAsync();

        return true;
    }

    
}
