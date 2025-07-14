using YourEcommerceApi.DTOs.Brand;

namespace YourEcommerceApi.Services.Interfaces;

public interface IBrandService
{
    Task<IEnumerable<BrandResponseDto>> GetAll();
    Task<BrandResponseDto?> Get(int id);
    Task<BrandResponseDto> Save(BrandCreateDto brandDto);
    Task<bool> Update(int id, BrandUpdateDto brandDto);
    Task<bool> Delete(int id);
}
