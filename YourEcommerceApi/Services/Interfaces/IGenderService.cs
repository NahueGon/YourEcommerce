using YourEcommerceApi.DTOs.GenderDtos;

namespace YourEcommerceApi.Services.Interfaces;

public interface IGenderService
{
    Task<IEnumerable<GenderResponseDto>> GetAll();
    Task<GenderResponseDto?> Get(int id);
    Task<GenderResponseDto> Save(GenderCreateDto genderDto);
    Task<GenderResponseDto?> Update(int id, GenderUpdateDto genderDto);
    Task<bool> Delete(int id);
}