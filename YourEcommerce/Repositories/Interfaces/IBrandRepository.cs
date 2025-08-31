using YourEcommerce.DTOs.BrandDtos;

namespace YourEcommerce.Repositories.Interfaces;

public interface IBrandRepository
{
    Task<List<BrandResponseDto>> GetAll();
    Task<BrandResponseDto?> GetById(int id);
    Task<BrandResponseDto?> Create(BrandCreateDto brandDto);
    Task<BrandResponseDto?> Update(int id, BrandUpdateDto brandDto);
    Task<bool> Delete(int id);
}