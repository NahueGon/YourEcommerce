using Microsoft.EntityFrameworkCore;
using YourEcommerceApi.Context;
using YourEcommerceApi.DTOs;
using YourEcommerceApi.DTOs.Category;
using YourEcommerceApi.DTOs.SubCategory;
using YourEcommerceApi.Models;
using YourEcommerceApi.Services.Interfaces;

namespace YourEcommerceApi.Services;

public class CategoryService : ICategoryService
{
    AppDbContext _context;

    public CategoryService(AppDbContext dbContext)
    {
        _context = dbContext;
    }

    public async Task<IEnumerable<CategoryResponseDto>> GetAll()
    {
        var categories = await _context.Categories
            .Include(c => c.Subcategories)
            .ToListAsync();

        return categories.Select(c => new CategoryResponseDto
        {
            Id = c.Id,
            Name = c.Name,
            Description = c.Description,
            Subcategories = c.Subcategories?
                .Select(sc => new SubcategoryResponseDto
                {
                    Id = sc.Id,
                    Name = sc.Name,
                    Description = sc.Description,
                    CategoryId = sc.CategoryId,
                    CategoryName = sc.Category?.Name,
                    Products = []
                }).ToList() ?? new List<SubcategoryResponseDto>()
        });
    }

    public async Task<CategoryResponseDto?> Get(int id)
    {
        var category = await _context.Categories
            .Include(c => c.Subcategories)
            .FirstOrDefaultAsync(c => c.Id == id);

        if (category == null)
            return null;

        return new CategoryResponseDto
        {
            Id = category.Id,
            Name = category.Name,
            Description = category.Description,
            Subcategories = category.Subcategories?
                .Select(sc => new SubcategoryResponseDto
                {
                    Id = sc.Id,
                    Name = sc.Name,
                    Description = sc.Description,
                    CategoryId = sc.CategoryId,
                    CategoryName = sc.Category?.Name,
                    Products = []
                }).ToList() ?? new List<SubcategoryResponseDto>()
        };
    }

    public async Task<CategoryResponseDto> Save(CategoryCreateDto categoryDto)
    {
        var category = new Category
        {
            Name = categoryDto.Name,
            Description = categoryDto.Description
        };

        _context.Categories.Add(category);
        await _context.SaveChangesAsync();

        return new CategoryResponseDto
        {
            Id = category.Id,
            Name = category.Name,
            Description = category.Description,
            Subcategories = new List<SubcategoryResponseDto>()
        };
    }

    public async Task<bool> Update(int id, CategoryUpdateDto categoryDto)
    {
        var currentCategory = await _context.Categories.FindAsync(id);

        if (currentCategory == null)
            return false;

        currentCategory.Name = categoryDto.Name;
        currentCategory.Description = categoryDto.Description;
        await _context.SaveChangesAsync();

        return true;
    }

    public async Task<bool> Delete(int id)
    {
        var currentCategory = await _context.Categories.FindAsync(id);

        if (currentCategory == null)
            return false;

        _context.Remove(currentCategory);
        await _context.SaveChangesAsync();

        return true;
    }

}
