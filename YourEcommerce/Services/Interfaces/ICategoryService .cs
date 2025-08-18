using YourEcommerce.DTOs.CategoryDtos;

namespace YourEcommerce.Services.Interfaces;

public interface ICategoryService
{
    Task<List<CategoryDto>> GetAll();
    Task<IEnumerable<CategoryDto>> GetAllFlat();
    Task<CategoryDto?> Get(int id);
    Task<bool> Delete(int id);
}