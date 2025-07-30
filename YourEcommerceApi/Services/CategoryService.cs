using AutoMapper;
using Microsoft.EntityFrameworkCore;
using YourEcommerceApi.Context;
using YourEcommerceApi.DTOs.CategoryDtos;
using YourEcommerceApi.Models.Products;
using YourEcommerceApi.Services.Interfaces;

namespace YourEcommerceApi.Services;

public class CategoryService : ICategoryService
{
    private readonly AppDbContext _context;
    private readonly IMapper  _mapper;

    public CategoryService(AppDbContext dbContext, IMapper mapper)
    {
        _context = dbContext;
        _mapper = mapper;
    }

    public async Task<IEnumerable<CategoryResponseDto>> GetAll()
    {
        var categories = await _context.Categories
            .Include(c => c.Products)
            .ToListAsync();

        return _mapper.Map<List<CategoryResponseDto>>(categories);
    }

    public async Task<CategoryResponseDto?> Get(int id)
    {
        var category = await _context.Categories
            .Include(c => c.Products)
            .FirstOrDefaultAsync(c => c.Id == id);
        if (category == null) return null;

        return _mapper.Map<CategoryResponseDto>(category);
    }

    public async Task<CategoryResponseDto> Save(CategoryCreateDto categoryDto)
    {
        var category = _mapper.Map<Category>(categoryDto);

        _context.Categories.Add(category);
        await _context.SaveChangesAsync();

        return _mapper.Map<CategoryResponseDto>(category);
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

        return _mapper.Map<CategoryResponseDto>(currentCategory);
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
