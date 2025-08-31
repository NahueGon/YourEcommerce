using System.Security.Claims;
using YourEcommerce.DTOs.UserDtos;

namespace YourEcommerce.Repositories.Interfaces;

public interface IUserRepository
{
    Task<List<UserResponseDto>> GetAll();
    Task<UserResponseDto?> GetById(int id);
    Task<UserResponseDto?> GetCurrent(ClaimsPrincipal user);
    Task<UserResponseDto?> GetByEmail(string email);
    Task<UserResponseDto?> Create(UserCreateDto userDto);
    Task<UserResponseDto?> Update(int id, UserUpdateDto userDto);
    Task<bool> Delete(int id);
}