using YourEcommerce.DTOs.GenderDtos;

namespace YourEcommerce.Repositories.Interfaces;

public interface IGenderRepository
{
    Task<List<GenderResponseDto>> GetAll();
    Task<GenderResponseDto?> GetById(int id);
    Task<GenderResponseDto?> Create(GenderCreateDto genderDto);
    Task<GenderResponseDto?> Update(int id, GenderUpdateDto genderDto);
    Task<bool> Delete(int id);
}