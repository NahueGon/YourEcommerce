using YourEcommerceApi.DTOs.SubCategory;

namespace YourEcommerceApi.Services.Interfaces;

public interface ISubcategoryService
{
    Task<IEnumerable<SubcategoryResponseDto>> GetAll();
    Task<SubcategoryResponseDto?> Get(int id);
    Task<SubcategoryResponseDto> Save(SubcategoryCreateDto subcategoryDto);
    Task<SubcategoryResponseDto?> Update(int id, SubcategoryUpdateDto? subcategoryDto);
    Task<bool> Delete(int id);
}
