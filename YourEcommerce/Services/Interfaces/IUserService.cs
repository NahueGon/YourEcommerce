using YourEcommerce.DTOs.UserDtos;

namespace YourEcommerce.Services.Interfaces;

public interface IUserService
{
    Task<UserViewModel?> GetByEmail(string email);
}
