using YourEcommerce.DTOs.BrandDtos;

namespace YourEcommerce.Services.Interfaces;

public interface IBrandService
{
    Task<List<BrandDto>> GetAllForTable();
    Task<BrandUpdateDto?> GetForEdit(int id);
    Task<BrandDto?> Create(BrandCreateDto brandDto);
    Task<BrandDto?> Update(int id, BrandUpdateDto brandDto);
    Task<bool> Delete(int id);
}