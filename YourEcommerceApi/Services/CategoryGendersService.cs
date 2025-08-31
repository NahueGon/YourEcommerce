using AutoMapper;
using Microsoft.EntityFrameworkCore;
using YourEcommerceApi.Context;
using YourEcommerceApi.DTOs.CategoryGender;
using YourEcommerceApi.Models.Products;
using YourEcommerceApi.Services.Interfaces;

namespace YourEcommerceApi.Services;

public class CategoryGendersService : ICategoryGendersService
{
    private readonly AppDbContext _context;
    private readonly IMapper _mapper;
    private readonly IWebHostEnvironment _env;

    public CategoryGendersService(AppDbContext dbContext, IMapper mapper, IWebHostEnvironment env)
    {
        _context = dbContext;
        _mapper = mapper;
        _env = env;
    }

    public async Task<IEnumerable<CategoryGendersResponseDto>> GetAll()
    {
        var categoryGenders = await _context.CategoryGenders
            .Include(cg => cg.Category)
            .Include(cg => cg.Gender)
            .ToListAsync();

        return _mapper.Map<List<CategoryGendersResponseDto>>(categoryGenders);
    }

    public async Task<CategoryGendersResponseDto?> Get(int id)
    {
        var categoryGender = await _context.CategoryGenders
            .Include(cg => cg.Category)
            .Include(cg => cg.Gender)
            .FirstOrDefaultAsync(cg => cg.Id == id);

        if (categoryGender == null) return null;

        return _mapper.Map<CategoryGendersResponseDto>(categoryGender);
    }

    public async Task<CategoryGendersResponseDto?> Save(CategoryGenderCreateDto categoryGenderDto)
    {
        if (categoryGenderDto == null) throw new ArgumentNullException(nameof(categoryGenderDto));

        var existingCategoryGender = await _context.CategoryGenders
            .FirstOrDefaultAsync(cg => cg.CategoryId == categoryGenderDto.CategoryId
                                    && cg.GenderId == categoryGenderDto.GenderId);

        if (existingCategoryGender != null)
        {
            var existingWithIncludes = await _context.CategoryGenders
                .Include(cg => cg.Category)
                .Include(cg => cg.Gender)
                .FirstOrDefaultAsync(cg => cg.Id == existingCategoryGender.Id);

            return _mapper.Map<CategoryGendersResponseDto>(existingWithIncludes);
        }

        var categoryGender = _mapper.Map<CategoryGender>(categoryGenderDto);

        if (categoryGenderDto.CategoryGenderImage != null)
        {
            categoryGender.CategoryGenderImage = await FileUploadHelper.SaveFileAsync(
                _env,
                categoryGenderDto.CategoryGenderImage,
                "images/categories",
                "cat"
            );
        }

        _context.CategoryGenders.Add(categoryGender);
        await _context.SaveChangesAsync();

        var savedCategoryGender = await _context.CategoryGenders
            .Include(cg => cg.Category)
            .Include(cg => cg.Gender)
            .FirstOrDefaultAsync(cg => cg.Id == categoryGender.Id);

        if (savedCategoryGender == null) return null;

        return _mapper.Map<CategoryGendersResponseDto>(savedCategoryGender);
    }

    public async Task<CategoryGendersResponseDto?> Update(int id, CategoryGendersUpdateDto dto)
    {
        var categoryGender = await _context.CategoryGenders
            .Include(cg => cg.Category)
            .Include(cg => cg.Gender)
            .FirstOrDefaultAsync(cg => cg.Id == id);

        if (categoryGender == null) return null;

        if (!string.IsNullOrWhiteSpace(dto.Name))
            categoryGender.Name = dto.Name;

        if (dto.CategoryGenderImage != null)
        {
            var imagePath = await FileUploadHelper.SaveFileAsync(
                _env,
                dto.CategoryGenderImage,
                folderPathRelative: "uploads/category-genders",
                filePrefix: $"categorygender_{categoryGender.Id}"
            );

            categoryGender.CategoryGenderImage = imagePath;
        }

        categoryGender.UpdatedAt = DateTime.Now;

        await _context.SaveChangesAsync();

        return _mapper.Map<CategoryGendersResponseDto>(categoryGender);
    }

    public async Task<bool> Delete(int id)
    {
        var categoryGender = await _context.CategoryGenders.FindAsync(id);
        if (categoryGender == null) return false;

        _context.Remove(categoryGender);
        await _context.SaveChangesAsync();

        return true;
    }
}