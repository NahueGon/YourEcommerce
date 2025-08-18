using System.Security.Claims;
using YourEcommerce.DTOs.UserDtos;

namespace YourEcommerce.Services.Interfaces;

public interface IUserService
{
    Task<IEnumerable<UserViewModel>> GetAll();
    Task<UserViewModel?> Get(int id);
    Task<UserViewModel?> GetCurrent(ClaimsPrincipal user);
    Task<UserViewModel?> GetByEmail(string email);
    Task<UserViewModel?> Update(int id, UserUpdateViewModel userDto);
    Task<bool> Delete(int id);
}
