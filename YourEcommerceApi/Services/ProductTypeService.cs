using Microsoft.EntityFrameworkCore;
using YourEcommerceApi.Context;
using YourEcommerceApi.DTOs.Category;
using YourEcommerceApi.DTOs.ProductType;
using YourEcommerceApi.DTOs.SubCategory;
using YourEcommerceApi.Extensions;
using YourEcommerceApi.Models;

namespace YourEcommerceApi.Services.Interfaces;

public class ProductTypeService : IProductTypeService
{
    AppDbContext _context;

    public ProductTypeService(AppDbContext dbContext)
    {
        _context = dbContext;
    }

    public async Task<IEnumerable<ProductTypeResponseDto>> GetAll()
    {
        var productTypes = await _context.ProductTypes
            .Include(pt => pt.Category)
            .Include(pt => pt.Subcategories)
            .ToListAsync();

        return productTypes.Select(pt => pt.ToDto());
    }

    public async Task<ProductTypeResponseDto?> Get(int id)
    {
        var productType = await _context.ProductTypes
            .Include(pt => pt.Category)
            .Include(pt => pt.Subcategories)
            .FirstOrDefaultAsync(pt => pt.Id == id);
        if (productType == null) return null;

        return productType.ToDto();
    }

    public async Task<ProductTypeResponseDto> Save(ProductTypeCreateDto productTypeDto)
    {
        var category = await _context.Categories.FindAsync(productTypeDto.CategoryId);
        if (category == null) throw new Exception("Categoria no encontrada");

        var productType = new ProductType
        {
            Name = productTypeDto.Name,
            CategoryId = category.Id,
            Category = category
        };

        _context.ProductTypes.Add(productType);
        await _context.SaveChangesAsync();

        return productType.ToDto();
    }

    public async Task<ProductTypeResponseDto?> Update(int id, ProductTypeUpdateDto productTypeDto)
    {
        var currentProductType = await _context.ProductTypes
            .Include(pt => pt.Subcategories)
            .FirstOrDefaultAsync(c => c.Id == id);
        if (currentProductType == null) return null;

        var newCategory = await _context.Categories.FindAsync(productTypeDto.CategoryId);
        if (newCategory == null) return null;

        if (!string.IsNullOrWhiteSpace(productTypeDto.Name) && productTypeDto.Name != currentProductType.Name)
            currentProductType.Name = productTypeDto.Name;

        if (productTypeDto?.CategoryId != currentProductType.CategoryId)
        {
            var productType = await _context.ProductTypes.FindAsync(productTypeDto?.CategoryId);
            if (productType == null) return null;

            currentProductType.CategoryId = newCategory.Id;
            currentProductType.Category = newCategory;
        }
        currentProductType.Category = newCategory;
        
        await _context.SaveChangesAsync();

        return currentProductType.ToDto();
    }

    public async Task<bool> Delete(int id)
    {
        var currentProductType = await _context.ProductTypes.FindAsync(id);
        if (currentProductType == null) return false;

        _context.Remove(currentProductType);
        await _context.SaveChangesAsync();

        return true;
    }
}
