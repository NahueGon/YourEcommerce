using System.Security.Claims;
using YourEcommerce.DTOs.UserDtos;

namespace YourEcommerce.Services.Interfaces;

public interface IUserService
{
    Task<UserResponseDto?> Get(int id);
    Task<UserResponseDto?> GetByEmail(string email);
    Task<List<UserDto>> GetAllForTable();
    Task<UserUpdateDto?> GetForEdit(int id);
    Task<UserResponseDto?> GetCurrent(ClaimsPrincipal claimsPrincipal);
    Task<UserDto?> Create(UserCreateDto userDto);
    Task<UserDto?> Update(int id, UserUpdateDto userDto);
    Task<bool> Delete(int id);
}