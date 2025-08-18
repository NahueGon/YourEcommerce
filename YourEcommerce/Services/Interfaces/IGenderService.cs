using YourEcommerce.DTOs.GenderDtos;

namespace YourEcommerce.Services.Interfaces;

public interface IGenderService
{
    Task<List<GenderDto>> GetAll();
    Task<IEnumerable<GenderDto>> GetAllFlat();
    Task<GenderDto?> Get(int id);
    Task<bool> Delete(int id);
}