using YourEcommerce.DTOs.SportDtos;

namespace YourEcommerce.Services.Interfaces;

public interface ISportService
{
    Task<List<SportDto>> GetAllForTable();
    Task<SportUpdateDto?> GetForEdit(int id);
    Task<SportDto?> Create(SportCreateDto sportDto);
    Task<SportDto?> Update(int id, SportUpdateDto sportDto);
    Task<bool> Delete(int id);
}