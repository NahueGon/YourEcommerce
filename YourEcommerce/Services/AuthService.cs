using YourEcommerce.DTOs.AuthDtos.RegisterDtos;
using YourEcommerce.Services.Interfaces;
using YourEcommerce.ViewModels;
using YourEcommerceApi.DTOs.AuthDtos.LoginDtos;

namespace YourEcommerce.Services;

public class AuthService : IAuthService
{
    private readonly HttpClient _httpClient;

    public AuthService(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }

    public async Task<string?> LoginAsync(LoginViewModel loginModel)
    {
        var response = await _httpClient.PostAsJsonAsync("api/auth/login", loginModel);

        if (!response.IsSuccessStatusCode) return null;

        var loginResponse = await response.Content.ReadFromJsonAsync<UserLoginResponseDto>();

        return string.IsNullOrEmpty(loginResponse?.Token) ? null : loginResponse.Token;
    }
    
    public async Task<UserRegisterResponseDto?> RegisterAsync(RegisterViewModel registerModel)
    {
        var response = await _httpClient.PostAsJsonAsync("api/auth/register", registerModel);

        return response.IsSuccessStatusCode 
        ? await response.Content.ReadFromJsonAsync<UserRegisterResponseDto>() 
        : null;
    }
}
