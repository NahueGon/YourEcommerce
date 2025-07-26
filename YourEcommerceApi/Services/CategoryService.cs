using Microsoft.EntityFrameworkCore;
using YourEcommerceApi.Context;
using YourEcommerceApi.DTOs;
using YourEcommerceApi.DTOs.Category;
using YourEcommerceApi.Extensions;
using YourEcommerceApi.Models.Products;
using YourEcommerceApi.Services.Interfaces;

namespace YourEcommerceApi.Services;

public class CategoryService : ICategoryService
{
    private readonly AppDbContext _context;

    public CategoryService(AppDbContext dbContext)
    {
        _context = dbContext;
    }

    public async Task<IEnumerable<CategoryResponseDto>> GetAll()
    {
        var categories = await _context.Categories
            .Include(c => c.Products)
            .ToListAsync();

        return categories.Select(c => c.ToDto());
    }

    public async Task<CategoryResponseDto?> Get(int id)
    {
        var category = await _context.Categories
            .Include(c => c.Products)
            .FirstOrDefaultAsync(c => c.Id == id);
        if (category == null) return null;

        return category.ToDto();
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

        return category.ToDto();
    }

    public async Task<CategoryResponseDto?> Update(int id, CategoryUpdateDto? categoryDto)
    {
        if (categoryDto == null) return null;

        var currentCategory = await _context.Categories
            .Include(c => c.Products)
            .FirstOrDefaultAsync(c => c.Id == id);
        if (currentCategory == null) return null;

        if (!string.IsNullOrWhiteSpace(categoryDto.Name) && categoryDto.Name != currentCategory.Name)
            currentCategory.Name = categoryDto.Name;
        if (!string.IsNullOrWhiteSpace(categoryDto.Description) && categoryDto.Description != currentCategory.Description)
            currentCategory.Description = categoryDto.Description;

        await _context.SaveChangesAsync();

        return currentCategory.ToDto();
    }

    public async Task<bool> Delete(int id)
    {
        var currentCategory = await _context.Categories.FindAsync(id);
        if (currentCategory == null) return false;

        _context.Remove(currentCategory);
        await _context.SaveChangesAsync();

        return true;
    }
}
