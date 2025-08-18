using System.Security.Claims;
using System.Text;
using System.Text.Json;
using YourEcommerce.DTOs.UserDtos;
using YourEcommerce.Services.Interfaces;

namespace YourEcommerce.Services;

public class UserService : IUserService
{
    private readonly HttpClient _httpClient;
    private readonly string _apiBaseUrl = "http://192.168.100.11:5076/";

    public UserService(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }

    public async Task<IEnumerable<UserViewModel>> GetAll()
    {
        var response = await _httpClient.GetAsync("api/users");
        if (!response.IsSuccessStatusCode) return Enumerable.Empty<UserViewModel>();

        var responseContent = await response.Content.ReadAsStringAsync();
        var users = JsonSerializer.Deserialize<IEnumerable<UserViewModel>>(responseContent, new JsonSerializerOptions
        {
            PropertyNameCaseInsensitive = true
        }) ?? Enumerable.Empty<UserViewModel>();

        foreach (var user in users)
        {
            if (!string.IsNullOrEmpty(user.ProfileImage))
            {
                user.ProfileImage = $"{_apiBaseUrl}{user.ProfileImage}";
            }
            else
            {
                user.ProfileImage = $"{_apiBaseUrl}/img/anonymous-profile.png";
            }
        }

        return users;
    }

    public async Task<UserViewModel?> Get(int id)
    {
        var response = await _httpClient.GetAsync($"api/users/{id}");

        if (!response.IsSuccessStatusCode) return null;

        var responseContent = await response.Content.ReadAsStringAsync();

        var user = JsonSerializer.Deserialize<UserViewModel>(responseContent, new JsonSerializerOptions
        {
            PropertyNameCaseInsensitive = true
        });

        return user;
    }

    public async Task<UserViewModel?> GetCurrent(ClaimsPrincipal user)
    {
        if (user.Identity == null || !user.Identity.IsAuthenticated) return null;

        var userIdClaim = user.FindFirst(ClaimTypes.NameIdentifier)?.Value;

        if (!int.TryParse(userIdClaim, out int userId)) return null;

        return await Get(userId);
    }

    public async Task<UserViewModel?> GetByEmail(string email)
    {
        var response = await _httpClient.GetAsync($"api/users/email?email={email}");

        if (!response.IsSuccessStatusCode) return null;

        var responseContent = await response.Content.ReadAsStringAsync();

        var user = JsonSerializer.Deserialize<UserViewModel>(responseContent, new JsonSerializerOptions
        {
            PropertyNameCaseInsensitive = true
        });

        return user;
    }

    public async Task<UserViewModel?> Update(int id, UserUpdateViewModel userDto)
    {
        using var formData = new MultipartFormDataContent();

        if (!string.IsNullOrWhiteSpace(userDto.Name))
            formData.Add(new StringContent(userDto.Name), "Name");

        if (!string.IsNullOrWhiteSpace(userDto.Lastname))
            formData.Add(new StringContent(userDto.Lastname), "Lastname");

        if (!string.IsNullOrWhiteSpace(userDto.Email))
            formData.Add(new StringContent(userDto.Email), "Email");

        if (!string.IsNullOrWhiteSpace(userDto.PhoneNumber))
            formData.Add(new StringContent(userDto.PhoneNumber), "PhoneNumber");

        if (!string.IsNullOrWhiteSpace(userDto.Address))
            formData.Add(new StringContent(userDto.Address), "Address");

        if (!string.IsNullOrWhiteSpace(userDto.CurrentPassword))
            formData.Add(new StringContent(userDto.CurrentPassword), "CurrentPassword");

        if (!string.IsNullOrWhiteSpace(userDto.NewPassword))
            formData.Add(new StringContent(userDto.NewPassword), "NewPassword");

        if (userDto.ProfileImage != null)
        {
            var fileStream = userDto.ProfileImage.OpenReadStream();
            formData.Add(new StreamContent(fileStream), "ProfileImage", userDto.ProfileImage.FileName);
        }

        var request = new HttpRequestMessage(new HttpMethod("PATCH"), $"api/users/{id}")
        {
            Content = formData
        };

        var response = await _httpClient.SendAsync(request);

        if (!response.IsSuccessStatusCode) return null;

        var responseContent = await response.Content.ReadAsStringAsync();

        return JsonSerializer.Deserialize<UserViewModel>(responseContent, new JsonSerializerOptions
        {
            PropertyNameCaseInsensitive = true
        });
    }
    
    public async Task<bool> Delete(int id)
    {
        var response = await _httpClient.DeleteAsync($"api/users/{id}");

        return response.IsSuccessStatusCode;
    }
}
