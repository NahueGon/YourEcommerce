using YourEcommerce.DTOs.SportDtos;

namespace YourEcommerce.Services.Interfaces;

public interface ISportService
{
    Task<List<SportDto>> GetAll();
    Task<IEnumerable<SportDto>> GetAllFlat();
    Task<SportDto?> Get(int id);
    Task<bool> Delete(int id);
}