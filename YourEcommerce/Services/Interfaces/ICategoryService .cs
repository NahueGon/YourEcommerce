using YourEcommerce.DTOs.CategoryDtos;

namespace YourEcommerce.Services.Interfaces;

public interface ICategoryService
{
    Task<List<CategoryDto>> GetAllForTable();
    Task<CategoryUpdateDto?> GetForEdit(int id);
    Task<CategoryDto?> Create(CategoryCreateDto categoryDto);
    Task<CategoryDto?> Update(int id, CategoryUpdateDto categoryDto);
    Task<bool> Delete(int id);
}