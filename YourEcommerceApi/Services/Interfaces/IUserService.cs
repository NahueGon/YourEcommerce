using YourEcommerceApi.DTOs.UserDtos;

namespace YourEcommerceApi.Services.Interfaces;

public interface IUserService
{
    Task<IEnumerable<UserResponseDto>> GetAll();
    Task<UserResponseDto?> Get(int id);
    Task<UserResponseDto?> GetByEmail(string email);
    Task<UserResponseDto> Save(UserCreateDto userDto);
    Task<UserResponseDto?> Update(int id, UserUpdateDto? userDto);
    Task<bool> Delete(int id);
}
