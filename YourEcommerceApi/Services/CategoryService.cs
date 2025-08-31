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
    private readonly IMapper _mapper;
    private readonly IWebHostEnvironment _env;

    public CategoryService(
        AppDbContext dbContext, 
        IMapper mapper,  
        IWebHostEnvironment env)
    {
        _context = dbContext;
        _mapper = mapper;
        _env = env;
    }

    public async Task<IEnumerable<CategoryResponseDto>> GetAll()
    {
        var categories = await _context.Categories
            .Include(c => c.CategoryGenders)
                .ThenInclude(cg => cg.Gender)
            .ToListAsync();

        return _mapper.Map<List<CategoryResponseDto>>(categories);
    }

    public async Task<CategoryResponseDto?> Get(int id)
    {
        var category = await _context.Categories
            .Include(c => c.CategoryGenders) 
                .ThenInclude(cg => cg.Gender) 
            .FirstOrDefaultAsync(c => c.Id == id);

        if (category == null) return null;

        return _mapper.Map<CategoryResponseDto>(category);
    }

    public async Task<CategoryResponseDto> Save(CategoryCreateDto categoryDto)
    {
        if (categoryDto == null) throw new ArgumentNullException(nameof(categoryDto));

        var category = _mapper.Map<Category>(categoryDto);
        
        _context.Categories.Add(category);
        await _context.SaveChangesAsync();

        if (categoryDto.CategoryImage != null && categoryDto.CategoryImage.Length > 0)
        {
            category.CategoryImage = await FileUploadHelper.SaveFileAsync(
                _env,
                categoryDto.CategoryImage,
                "images/categories",
                $"{category.Id}_frontpage",
                width: 1920,
                height: 1080
            );

            await _context.SaveChangesAsync();
        }

        var categoryWithGenders = await _context.Categories
            .FirstOrDefaultAsync(c => c.Id == category.Id);

        return _mapper.Map<CategoryResponseDto>(categoryWithGenders)!;
    }

    public async Task<CategoryResponseDto?> Update(int id, CategoryUpdateDto? categoryDto)
    {
        if (categoryDto == null) return null;

        var category = await _context.Categories
            .FirstOrDefaultAsync(c => c.Id == id);

        if (category == null) return null;

        if (!string.IsNullOrWhiteSpace(categoryDto.Name) && categoryDto.Name != category.Name)
            category.Name = categoryDto.Name;

        if (!string.IsNullOrWhiteSpace(categoryDto.Description) && categoryDto.Description != category.Description)
            category.Description = categoryDto.Description;

        if (categoryDto.CategoryImage != null)
        {
            if (!string.IsNullOrEmpty(category.CategoryImage))
            {
                var oldImagePath = Path.Combine(_env.WebRootPath!, category.CategoryImage.Replace("/", Path.DirectorySeparatorChar.ToString()));
                if (File.Exists(oldImagePath))
                {
                    File.Delete(oldImagePath);
                }
            }

            category.CategoryImage = await FileUploadHelper.SaveFileAsync(
                _env,
                categoryDto.CategoryImage,
                $"img/sports/{category.Id}_sport",
                $"{category.Id}_frontpage",
                width: 1920,
                height: 1080
            );
        }

        await _context.SaveChangesAsync();
        return _mapper.Map<CategoryResponseDto>(category);
    }

    public async Task<bool> Delete(int id)
    {
        var category = await _context.Categories.FindAsync(id);
        if (category == null) return false;

        _context.Remove(category);
        await _context.SaveChangesAsync();

        return true;
    }
}