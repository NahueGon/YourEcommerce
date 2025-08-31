using YourEcommerceApi.DTOs.CategoryGender;

namespace YourEcommerceApi.Services.Interfaces;

public interface ICategoryGendersService
{
    Task<IEnumerable<CategoryGendersResponseDto>> GetAll();
    Task<CategoryGendersResponseDto?> Get(int id);
    Task<CategoryGendersResponseDto?> Save(CategoryGenderCreateDto categoryGenderDto);
    Task<CategoryGendersResponseDto?> Update(int id, CategoryGendersUpdateDto dto);
    Task<bool> Delete(int id);
}