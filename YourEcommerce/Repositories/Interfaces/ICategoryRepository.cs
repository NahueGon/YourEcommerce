using YourEcommerce.DTOs.CategoryDtos;

namespace YourEcommerce.Repositories.Interfaces;

public interface ICategoryRepository
{
    Task<List<CategoryResponseDto>> GetAll();
    Task<CategoryResponseDto?> GetById(int id);
    Task<CategoryResponseDto?> Create(CategoryCreateDto categoryDto);
    Task<CategoryResponseDto?> Update(int id, CategoryUpdateDto categoryDto);
    Task<bool> Delete(int id);
}