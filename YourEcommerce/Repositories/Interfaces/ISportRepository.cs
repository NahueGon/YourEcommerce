using YourEcommerce.DTOs.SportDtos;

namespace YourEcommerce.Repositories.Interfaces;

public interface ISportRepository
{
    Task<List<SportResponseDto>> GetAll();
    Task<SportResponseDto?> GetById(int id);
    Task<SportResponseDto?> Create(SportCreateDto sportDto);
    Task<SportResponseDto?> Update(int id, SportUpdateDto sportDto);
    Task<bool> Delete(int id);
}