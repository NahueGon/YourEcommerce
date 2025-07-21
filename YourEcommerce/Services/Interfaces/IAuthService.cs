using YourEcommerce.DTOs.AuthDtos.RegisterDtos;
using YourEcommerce.ViewModels;

namespace YourEcommerce.Services.Interfaces;

public interface IAuthService
{
    Task<string?> LoginAsync(LoginViewModel loginModel);
    Task<UserRegisterResponseDto?> RegisterAsync(RegisterViewModel registerModel);
}
