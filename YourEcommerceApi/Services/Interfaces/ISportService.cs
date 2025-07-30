using YourEcommerceApi.DTOs.SportDtos;

namespace YourEcommerceApi.Services.Interfaces;

public interface ISportService
{
    Task<IEnumerable<SportResponseDto>> GetAll();
    Task<SportResponseDto?> Get(int id);
    Task<SportResponseDto> Save(SportCreateDto sportDto);
    Task<SportResponseDto?> Update(int id, SportUpdateDto sportDto);
    Task<bool> Delete(int id);
}
