using Microsoft.EntityFrameworkCore;
using YourEcommerceApi.Context;
using YourEcommerceApi.DTOs.ProductAttributeDtos;
using YourEcommerceApi.Extensions;
using YourEcommerceApi.Models.Products;
using YourEcommerceApi.Services.Interfaces;

namespace YourEcommerceApi.Services;

public class ProductAttributeService : IProductAttributeService
{
    private readonly AppDbContext _context;

    public ProductAttributeService(AppDbContext context)
    {
        _context = context;
    }
    
    public async Task<IEnumerable<ProductAttributeResponseDto>> GetAll()
    {
        var productAttributes = await _context.ProductAttributes
            .ToListAsync();

        return productAttributes.Select(pa => pa.ToDto()).ToList();
    }

    public async Task<ProductAttributeResponseDto?> Get(int id)
    {
        var productAttribute = await _context.ProductAttributes
            .FirstOrDefaultAsync(pa => pa.Id == id);
        if (productAttribute == null) return null;

        return productAttribute.ToDto();
    }

    public async Task<ProductAttributeResponseDto> Save(ProductAttributeCreateDto productAttributeDto)
    {
        var productAttribute = new ProductAttribute
        {
            Key = productAttributeDto.Key,
            Value = productAttributeDto.Value,
            ProductId = productAttributeDto.ProductId
        };

        _context.ProductAttributes.Add(productAttribute);
        await _context.SaveChangesAsync();

        return productAttribute.ToDto();
    }

    public async Task<bool> Update(int id, ProductAttributeUpdateDto productAttributeDto)
    {
        var currentProductAttribute = await _context.ProductAttributes.FindAsync(id);
        if (currentProductAttribute == null) return false;

        currentProductAttribute.Key = productAttributeDto.Key;
        currentProductAttribute.Value = productAttributeDto.Value;
        currentProductAttribute.ProductId = productAttributeDto.ProductId;

        await _context.SaveChangesAsync();

        return true;
    }

    public async Task<bool> Delete(int id)
    {
        var currentProductAttribute = await _context.ProductAttributes.FindAsync(id);
        if (currentProductAttribute == null) return false;

        _context.Remove(currentProductAttribute);
        await _context.SaveChangesAsync();

        return true;
    }
}