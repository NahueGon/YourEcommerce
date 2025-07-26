using Microsoft.EntityFrameworkCore;
using YourEcommerceApi.Context;
using YourEcommerceApi.DTOs.Product;
using YourEcommerceApi.DTOs.ProductDtos;
using YourEcommerceApi.Extensions;
using YourEcommerceApi.Models.Products;
using YourEcommerceApi.Services.Interfaces;

namespace YourEcommerceApi.Services;

public class ProductService : IProductService
{
    private readonly AppDbContext _context;

    public ProductService(AppDbContext dbContext)
    {
        _context = dbContext;
    }

    public async Task<IEnumerable<ProductResponseDto>> GetAll()
    {
        var products = await _context.Products
            .Include(p => p.Category)
            .Include(p => p.Brand)
            .Include(p => p.Sport)
            .Include(p => p.ProductTags)
                .ThenInclude(pt => pt.Tag)
            .Include(p => p.ProductAttributes)
            .Include(p => p.ProductVariants)
            .ToListAsync();

        return products.Select(p => p.ToDto()).ToList();
    }

    public async Task<ProductResponseDto?> Get(int id)
    {
        var product = await _context.Products
            .Include(p => p.Category)
            .Include(p => p.Brand)
            .Include(p => p.Sport)
            .Include(p => p.ProductTags)
                .ThenInclude(pt => pt.Tag)
            .Include(p => p.ProductAttributes)
            .Include(p => p.ProductVariants)
            .FirstOrDefaultAsync(p => p.Id == id);

        if (product == null) return null;

        return product.ToDto();
    }

    public async Task<ProductResponseDto> Save(ProductCreateDto productDto)
    {
        Brand? brand = null;
        Sport? sport = null;
        Category? category = null;

        if (productDto.CategoryId is > 0)
        {
            category = await _context.Categories.FindAsync(productDto.CategoryId)
                ?? throw new Exception("Categoria no encontrada.");
        }

        if (productDto.BrandId is > 0)
        {
            brand = await _context.Brands.FindAsync(productDto.BrandId)
                ?? throw new Exception("Marca no encontrada.");
        }

        if (productDto.SportId is > 0)
        {
            sport = await _context.Sports.FindAsync(productDto.SportId)
                ?? throw new Exception("Deporte no encontrado.");
        }

        var product = new Product
        {
            Name = productDto.Name,
            Description = productDto.Description,
            Price = productDto.Price,
            Stock = productDto.Stock,
            Gender = productDto.Gender,
            CategoryId = category?.Id,
            BrandId = brand?.Id,
            SportId = sport?.Id,
            Category = category,
            Brand = brand,
            Sport = sport
        };

        _context.Products.Add(product);
        await _context.SaveChangesAsync();

        return product.ToDto();
    }

    public async Task<ProductResponseDto?> Update(int id, ProductUpdateDto productDto)
    {
        if (productDto == null) return null;

        var currentProduct = await _context.Products.FindAsync(id);
        if (currentProduct == null) return null;

        if (productDto.CategoryId.HasValue)
        {
            var categoryExists = await _context.Categories.AnyAsync(c => c.Id == productDto.CategoryId);
            if (!categoryExists) throw new Exception("Categoria no encontrada.");
        }

        if (productDto.BrandId.HasValue)
        {
            var brandExists = await _context.Brands.AnyAsync(b => b.Id == productDto.BrandId);
            if (!brandExists) throw new Exception("Marca no encontrada.");
        }

        if (productDto.SportId.HasValue)
        {
            var sportExists = await _context.Sports.AnyAsync(s => s.Id == productDto.SportId);
            if (!sportExists) throw new Exception("Deporte no encontrado.");
        }

        if (!string.IsNullOrWhiteSpace(productDto.Name) && productDto.Name != currentProduct.Name)
            currentProduct.Name = productDto.Name;

        if (!string.IsNullOrWhiteSpace(productDto.Description) && productDto.Description != currentProduct.Description)
            currentProduct.Description = productDto.Description;

        if (productDto.Price != 0 && productDto.Price != currentProduct.Price)
            currentProduct.Price = productDto.Price;

        if (productDto.Gender != currentProduct.Gender)
            currentProduct.Gender = productDto.Gender;

        if (productDto.CategoryId.HasValue && productDto.CategoryId != currentProduct.CategoryId)
            currentProduct.CategoryId = productDto.CategoryId;

        if (productDto.BrandId.HasValue && productDto.BrandId != currentProduct.BrandId)
            currentProduct.BrandId = productDto.BrandId;

        if (productDto.SportId.HasValue && productDto.SportId != currentProduct.SportId)
            currentProduct.SportId = productDto.SportId;

        try
        {
            await _context.SaveChangesAsync();
        }
        catch (DbUpdateException ex)
        {
            throw new Exception("No se pudo actualizar el producto. Verifica que la marca, categoría y deporte existan.", ex);
        }

        var productWithRelations = await _context.Products
        .Include(p => p.Category)
        .Include(p => p.Brand)
        .Include(p => p.Sport)
        .FirstOrDefaultAsync(p => p.Id == id);

        return productWithRelations?.ToDto();
    }

    public async Task<bool> Delete(int id)
    {
        var currentProduct = await _context.Products.FindAsync(id);
        if (currentProduct == null) return false;

        _context.Remove(currentProduct);
        await _context.SaveChangesAsync();

        return true;
    }
}