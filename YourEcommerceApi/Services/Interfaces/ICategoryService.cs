using YourEcommerceApi.DTOs;
using YourEcommerceApi.DTOs.Category;

namespace YourEcommerceApi.Services.Interfaces;

public interface ICategoryService
{
    Task<IEnumerable<CategoryResponseDto>> GetAll();
    Task<CategoryResponseDto?> Get(int id);
    Task<CategoryResponseDto> Save(CategoryCreateDto categoryDto);
    Task<CategoryResponseDto?> Update(int id, CategoryUpdateDto? categoryDto);
    Task<bool> Delete(int id);
}
