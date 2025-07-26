using Microsoft.EntityFrameworkCore;
using YourEcommerceApi.Context;
using YourEcommerceApi.DTOs.ProductColorDtos;
using YourEcommerceApi.Extensions;
using YourEcommerceApi.Models.Products;
using YourEcommerceApi.Services.Interfaces;

namespace YourEcommerceApi.Services;

public class ProductColorService : IProductColorService
{
    private readonly AppDbContext _context;

    public ProductColorService(AppDbContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<ProductColorResponseDto>> GetAll()
    {
        var productColors = await _context.ProductColors
            .ToListAsync();

        return productColors.Select(pc => pc.ToDto()).ToList();
    }
    
    public async Task<ProductColorResponseDto?> Get(int id)
    {
        var productColor = await _context.ProductColors
            .FirstOrDefaultAsync(pc => pc.Id == id);
        if (productColor == null) return null;

        return productColor.ToDto();
    }
    
    public async Task<ProductColorResponseDto> Save(ProductColorCreateDto productColorDto)
    {
        var productColor = new ProductColor
        {
            Name = productColorDto.Name
        };

        _context.ProductColors.Add(productColor);
        await _context.SaveChangesAsync();

        return productColor.ToDto();
    }
    
    public async Task<bool> Update(int id, ProductColorUpdateDto productColorDto)
    {
        var currentProductColor = await _context.ProductColors.FindAsync(id);
        if (currentProductColor == null) return false;

        currentProductColor.Name = productColorDto.Name;

        await _context.SaveChangesAsync();

        return true;
    }
    
    public async Task<bool> Delete(int id)
    {
        var currentProductColor = await _context.ProductColors.FindAsync(id);
        if (currentProductColor == null) return false;

        _context.Remove(currentProductColor);
        await _context.SaveChangesAsync();

        return true;
    }
}