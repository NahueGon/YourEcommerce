using YourEcommerceApi.DTOs.AuthDtos.LoginDtos;
using YourEcommerceApi.DTOs.AuthDtos.RegisterDtos;

namespace YourEcommerceApi.Services;

public interface IAuthService
{
    Task<UserLoginResponseDto?> Authenticate(UserLoginDto loginDto);
    Task<UserRegisterResponseDto?> Register(UserRegisterCreateDto registerDto);
}
