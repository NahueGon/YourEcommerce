using Microsoft.EntityFrameworkCore;
using YourEcommerceApi.Context;
using YourEcommerceApi.DTOs.ProductVariantDtos;
using YourEcommerceApi.Extensions;
using YourEcommerceApi.Models.Products;
using YourEcommerceApi.Services.Interfaces;

namespace YourEcommerceApi.Services;

public class ProductVariantService : IProductVariantService
{
    private readonly AppDbContext _context;

    public ProductVariantService(AppDbContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<ProductVariantResponseDto>> GetAll()
    {
        var productVariants = await _context.ProductVariants
            .ToListAsync();

        return productVariants.Select(pv => pv.ToDto()).ToList();
    }
    
    public async Task<ProductVariantResponseDto?> Get(int id)
    {
        var productVariant = await _context.ProductVariants
            .FirstOrDefaultAsync(pv => pv.Id == id);
        if (productVariant == null) return null;

        return productVariant.ToDto();
    }
    
    public async Task<ProductVariantResponseDto> Save(ProductVariantCreateDto productVariantDto)
    {
        var productVariant = new ProductVariant
        {
            Size = productVariantDto.Size,
            Stock = productVariantDto.Stock,
            ProductId = productVariantDto.ProductId
        };

        _context.ProductVariants.Add(productVariant);
        await _context.SaveChangesAsync();

        return productVariant.ToDto();
    }
    
    public async Task<bool> Update(int id, ProductVariantUpdateDto productVariantDto)
    {
        var currentProductVariant = await _context.ProductVariants.FindAsync(id);
        if (currentProductVariant == null) return false;

        currentProductVariant.Size = productVariantDto.Size;
        currentProductVariant.Stock = productVariantDto.Stock;
        currentProductVariant.ProductId = productVariantDto.ProductId;

        await _context.SaveChangesAsync();

        return true;
    }
    
    public async Task<bool> Delete(int id)
    {
        var currentProductVariant = await _context.ProductVariants.FindAsync(id);
        if (currentProductVariant == null) return false;

        _context.Remove(currentProductVariant);
        await _context.SaveChangesAsync();

        return true;
    }
}