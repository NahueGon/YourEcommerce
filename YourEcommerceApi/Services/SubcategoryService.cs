using Microsoft.EntityFrameworkCore;
using YourEcommerceApi.Context;
using YourEcommerceApi.DTOs.Category;
using YourEcommerceApi.DTOs.Product;
using YourEcommerceApi.DTOs.ProductDtos;
using YourEcommerceApi.DTOs.SubCategory;
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
            .Include(sc => sc.Category)
            .Include(sc => sc.Products)
            .ToListAsync();

        return subcategories.Select(sc => new SubcategoryResponseDto
        {
            Id = sc.Id,
            Name = sc.Name,
            Description = sc.Description,
            Category = new CategoryDto
            {
                Id = sc.CategoryId,
                Name = sc.Category?.Name ?? string.Empty
            },
            Products = sc.Products?
                .Select(p => new ProductDto
                {
                    Id = p.Id,
                    Name = p.Name
                }).ToList() ?? new List<ProductDto>()
        });
    }

    public async Task<SubcategoryResponseDto?> Get(int id)
    {
        var subcategory = await _context.SubCategories
            .Include(sc => sc.Category)
            .Include(sc => sc.Products)
            .FirstOrDefaultAsync(sc => sc.Id == id);

        if (subcategory == null)
            return null;

        return new SubcategoryResponseDto
        {
            Id = subcategory.Id,
            Name = subcategory.Name,
            Description = subcategory.Description,
            Category = new CategoryDto
            {
                Id = subcategory.CategoryId,
                Name = subcategory.Category?.Name ?? string.Empty
            },
            Products = subcategory.Products?
                .Select(p => new ProductDto
                {
                    Id = p.Id,
                    Name = p.Name
                }).ToList() ?? new List<ProductDto>()
        };
    }

    public async Task<SubcategoryResponseDto> Save(SubcategoryCreateDto subcategoryDto)
    {
        var category = await _context.Categories.FindAsync(subcategoryDto.CategoryId);

        if (category == null)
            throw new Exception("Categoria no encontrada");

        var subcategory = new SubCategory
        {
            Name = subcategoryDto.Name,
            Description = subcategoryDto.Description,
            Category = category
        };

        _context.SubCategories.Add(subcategory);
        await _context.SaveChangesAsync();

        return new SubcategoryResponseDto
        {
            Id = subcategory.Id,
            Name = subcategory.Name,
            Description = subcategory.Description,
            Category = new CategoryDto
            {
                Id = category.Id,
                Name = category.Name
            },
            Products = new List<ProductDto>()
        };
    }

    public async Task<bool> Update(int id, SubcategoryUpdateDto subcategoryDto)
    {
        var currentSubcategory = await _context.SubCategories.FindAsync(id);

        if (currentSubcategory == null)
            return false;

        var newCategory = await _context.Categories.FindAsync(subcategoryDto.CategoryId);

        if (newCategory == null)
            return false;

        currentSubcategory.Name = subcategoryDto.Name;
        currentSubcategory.Description = subcategoryDto.Description;
        currentSubcategory.Category = newCategory;
        
        await _context.SaveChangesAsync();

        return true;
    }

    public async Task<bool> Delete(int id)
    {
        var currentSubcategory = await _context.SubCategories.FindAsync(id);

        if (currentSubcategory == null)
            return false;

        _context.Remove(currentSubcategory);
        await _context.SaveChangesAsync();

        return true;
    }

}
