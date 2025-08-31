using YourEcommerce.DTOs.GenderDtos;

namespace YourEcommerce.Services.Interfaces;

public interface IGenderService
{
    Task<List<GenderDto>> GetAllForTable();
    Task<GenderUpdateDto?> GetForEdit(int id);
    Task<GenderDto?> Create(GenderCreateDto GenderDto);
    Task<GenderDto?> Update(int id, GenderUpdateDto GenderDto);
    Task<bool> Delete(int id);
}