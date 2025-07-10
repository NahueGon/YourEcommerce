using Microsoft.EntityFrameworkCore;
using YourEcommerceApi.Context;
using YourEcommerceApi.DTOs.Product;
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
            CategoryId = sc.CategoryId,
            CategoryName = sc.Category?.Name,
            Products = sc.Products?
                .Select(sc => new ProductResponseDto
                {
                    Id = sc.Id,
                    Name = sc.Name,
                    Description = sc.Description,
                    Price = sc.Price,
                    Stock = sc.Stock
                }).ToList() ?? new List<ProductResponseDto>()
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
            CategoryId = subcategory.CategoryId,
            CategoryName = subcategory.Category?.Name,
            Products = subcategory.Products?
                .Select(sc => new ProductResponseDto
                {
                    Id = sc.Id,
                    Name = sc.Name,
                    Description = sc.Description,
                    Price = sc.Price,
                    Stock = sc.Stock
                }).ToList() ?? new List<ProductResponseDto>()
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
            CategoryId = category.Id,
            CategoryName = category.Name,
            Products = new List<ProductResponseDto>()
        };
    }

    public async Task<bool> Update(int id, SubcategoryUpdateDto subcategoryDto)
    {
        var currentSubcategory = await _context.SubCategories.FindAsync(id);

        if (currentSubcategory == null)
            return false;

        currentSubcategory.Name = subcategoryDto.Name;
        currentSubcategory.Description = subcategoryDto.Description;
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
