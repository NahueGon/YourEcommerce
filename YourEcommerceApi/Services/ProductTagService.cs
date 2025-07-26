using Microsoft.EntityFrameworkCore;
using YourEcommerceApi.Context;
using YourEcommerceApi.DTOs.ProductTagDtos;
using YourEcommerceApi.Extensions;
using YourEcommerceApi.Models.Products;
using YourEcommerceApi.Services.Interfaces;

namespace YourEcommerceApi.Services;

public class ProductTagService : IProductTagService
{
    private readonly AppDbContext _context;

    public ProductTagService(AppDbContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<ProductTagResponseDto>> GetAll() 
    {
        var productTags = await _context.ProductTags
            .ToListAsync();

        return productTags.Select(pt => pt.ToDto()).ToList();
    }
    
    public async Task<ProductTagResponseDto?> Get(int productId, int tagId)
    {
        var productTag = await _context.ProductTags
            .FirstOrDefaultAsync(pt => pt.ProductId == productId && pt.TagId == tagId);
        if (productTag == null) return null;

        return productTag.ToDto();
    }
    
    public async Task<ProductTagResponseDto> Save(ProductTagCreateDto productTagDto)
    {
        var product = await _context.Products.FindAsync(productTagDto.ProductId)
            ?? throw new InvalidOperationException($"No se encontró el producto con ID {productTagDto.ProductId}");
        var tag = await _context.Tags.FindAsync(productTagDto.TagId)
            ?? throw new InvalidOperationException($"No se encontró el tag con ID {productTagDto.TagId}");
        
        var productTag = new ProductTag
        {
            ProductId = productTagDto.ProductId,
            TagId = productTagDto.TagId
        };

        _context.ProductTags.Add(productTag);
        await _context.SaveChangesAsync();

        return productTag.ToDto();
    }
    
    public async Task<bool> Update(int productId, int tagId, ProductTagUpdateDto productTagDto)
    {
        var current = await _context.ProductTags
            .FirstOrDefaultAsync(pt => pt.ProductId == productId && pt.TagId == tagId);
        if (current == null) return false;

        current.TagId = productTagDto.NewTagId;

        await _context.SaveChangesAsync();
        return true;
    }

    public async Task<bool> Delete(int productId, int tagId) 
    {
        var currentProductTag = await _context.ProductTags.FindAsync(productId, tagId);
        if (currentProductTag == null) return false;

        _context.ProductTags.Remove(currentProductTag);
        await _context.SaveChangesAsync();

        return true;
    }
}