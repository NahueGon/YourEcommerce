using AutoMapper;
using YourEcommerce.DTOs.CategoryDtos;
using YourEcommerce.Repositories.Interfaces;
using YourEcommerce.Services.Interfaces;

namespace YourEcommerce.Services;

public class CategoryService : ICategoryService
{
    private readonly ICategoryRepository _categoryRepository;
    private readonly IMapper _mapper;

    public CategoryService(ICategoryRepository repository, IMapper mapper)
    {
        _categoryRepository = repository;
        _mapper = mapper;
    }

    public async Task<List<CategoryDto>> GetAllForTable()
    {
        var categories = await _categoryRepository.GetAll();
        return categories.Select(s => _mapper.Map<CategoryDto>(s)).ToList();
    }

    public async Task<CategoryUpdateDto?> GetForEdit(int id)
    {
        var category = await _categoryRepository.GetById(id);
        if (category is null) return null;
        return _mapper.Map<CategoryUpdateDto>(category);
    }

    public async Task<CategoryDto?> Create(CategoryCreateDto categoryDto)
    {
        var created = await _categoryRepository.Create(categoryDto);
        if (created == null) return null;
        return _mapper.Map<CategoryDto>(created);
    }

    public async Task<CategoryDto?> Update(int id, CategoryUpdateDto categoryDto)
    {
        var updated = await _categoryRepository.Update(id, categoryDto);
        if (updated == null) return null;
        return _mapper.Map<CategoryDto>(updated);
    }

    public Task<bool> Delete(int id)
    {
        return _categoryRepository.Delete(id);
    }
}