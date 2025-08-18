using YourEcommerce.DTOs.BrandDtos;

namespace YourEcommerce.Services.Interfaces;

public interface IBrandService
{
    Task<List<BrandDto>> GetAll();
    Task<IEnumerable<BrandDto>> GetAllFlat();
    Task<BrandDto?> Get(int id);
    Task<bool> Delete(int id);
}
