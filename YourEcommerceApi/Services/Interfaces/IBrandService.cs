using YourEcommerceApi.DTOs.BrandDtos;

namespace YourEcommerceApi.Services.Interfaces;

public interface IBrandService
{
    Task<IEnumerable<BrandResponseDto>> GetAll();
    Task<BrandResponseDto?> Get(int id);
    Task<BrandResponseDto> Save(BrandCreateDto brandDto);
    Task<BrandResponseDto?> Update(int id, BrandUpdateDto brandDto);
    Task<bool> Delete(int id);
}