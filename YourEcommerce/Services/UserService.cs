using System.Text.Json;
using YourEcommerce.DTOs.UserDtos;
using YourEcommerce.Services.Interfaces;

namespace YourEcommerce.Services;

public class UserService : IUserService
{
    private readonly HttpClient _httpClient;

    public UserService(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }

    public async Task<UserViewModel?> GetByEmail(string  email)
    {
        var response = await _httpClient.GetAsync($"api/user/email?email={email}");

        if (!response.IsSuccessStatusCode) return null;
   
        var responseContent = await response.Content.ReadAsStringAsync();

        var user = JsonSerializer.Deserialize<UserViewModel>(responseContent, new JsonSerializerOptions
        {
            PropertyNameCaseInsensitive = true
        });

        return user;
    }
}
