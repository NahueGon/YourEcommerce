using Microsoft.EntityFrameworkCore;
using YourEcommerceApi.Context;
using YourEcommerceApi.DTOs.SubCategory;
using YourEcommerceApi.Extensions;
using YourEcommerceApi.Models;

namespace YourEcommerceApi.Services.Interfaces;

public class SubcategoryService : ISubcategoryService
{
    AppDbContext _context;

    public SubcategoryService(AppDbContext dbContext)
    {
        _context = dbContext;
    }

    public async Task<IEnumerable<SubcategoryResponseDto>> GetAll()
    {
        var subcategories = await _context.SubCategories
            .Include(sc => sc.ProductType)
            .Include(sc => sc.Products)
            .ToListAsync();

        return subcategories.Select(sc => sc.ToDto());
    }

    public async Task<SubcategoryResponseDto?> Get(int id)
    {
        var subcategory = await _context.SubCategories
            .Include(sc => sc.ProductType)
            .Include(sc => sc.Products)
            .FirstOrDefaultAsync(sc => sc.Id == id);
        if (subcategory == null) return null;

        return subcategory.ToDto();
    }

    public async Task<SubcategoryResponseDto> Save(SubcategoryCreateDto subcategoryDto)
    {
        var productType = await _context.ProductTypes.FindAsync(subcategoryDto.ProductTypeId);
        if (productType == null) throw new Exception("Tipo no encontrado");

        var subcategory = new SubCategory
        {
            Name = subcategoryDto.Name,
            Description = subcategoryDto.Description,
            ProductTypeId = productType.Id,
            ProductType = productType
        };

        _context.SubCategories.Add(subcategory);
        await _context.SaveChangesAsync();

        return subcategory.ToDto();
    }

    public async Task<SubcategoryResponseDto?> Update(int id, SubcategoryUpdateDto? subcategoryDto)
    {
        if (subcategoryDto == null) return null;

        var currentSubcategory = await _context.SubCategories
            .Include(sc => sc.Products)
            .FirstOrDefaultAsync(sc => sc.Id == id);
        if (currentSubcategory == null) return null;

        var newProductType = await _context.ProductTypes.FindAsync(subcategoryDto?.ProductTypeId);
        if (newProductType == null) return null;

        if (!string.IsNullOrWhiteSpace(subcategoryDto?.Name) && subcategoryDto.Name != currentSubcategory.Name)
            currentSubcategory.Name = subcategoryDto.Name;
        if (!string.IsNullOrWhiteSpace(subcategoryDto?.Description) && subcategoryDto.Description != currentSubcategory.Description)
            currentSubcategory.Description = subcategoryDto.Description;
        if (subcategoryDto?.ProductTypeId != currentSubcategory.ProductTypeId)
        {
            var productType = await _context.ProductTypes.FindAsync(subcategoryDto?.ProductTypeId);
            if (productType == null) return null;

            currentSubcategory.ProductTypeId = newProductType.Id;
            currentSubcategory.ProductType = newProductType;
        }
        
        await _context.SaveChangesAsync();

        return currentSubcategory.ToDto();
    }

    public async Task<bool> Delete(int id)
    {
        var currentSubcategory = await _context.SubCategories.FindAsync(id);
        if (currentSubcategory == null) return false;

        _context.Remove(currentSubcategory);
        await _context.SaveChangesAsync();

        return true;
    }
}
