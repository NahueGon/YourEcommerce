using System.Security.Claims;
using System.Text.Json;
using Microsoft.Extensions.Options;
using YourEcommerce.Config;
using YourEcommerce.DTOs.UserDtos;
using YourEcommerce.Repositories.Interfaces;

namespace YourEcommerce.Repositories;

public class UserRepository : IUserRepository
{
    private readonly HttpClient _httpClient;
    private readonly JsonSerializerOptions _jsonOptions;
    private readonly string _apiBaseUrl;

    public UserRepository(HttpClient httpClient, JsonSerializerOptions jsonOptions, IOptions<ApiSettings> apiSettings)
    {
        _httpClient = httpClient;
        _jsonOptions = jsonOptions;
        if (string.IsNullOrEmpty(apiSettings.Value.BaseUrl))
            throw new ArgumentException("ApiSettings.BaseUrl no está configurado en appsettings.json");

        _apiBaseUrl = apiSettings.Value.BaseUrl;
    }

    public async Task<List<UserResponseDto>> GetAll()
    {
        var response = await _httpClient.GetAsync("api/users");
        if (!response.IsSuccessStatusCode) return new();

        var content = await response.Content.ReadAsStringAsync();
        var users = JsonSerializer.Deserialize<List<UserResponseDto>>(content, _jsonOptions) ?? new List<UserResponseDto>();

        users.ForEach(u => u.ProfileImage = ResolveImageUrl(u.ProfileImage));

        return users;
    }

    public async Task<UserResponseDto?> GetById(int id)
    {
        var response = await _httpClient.GetAsync($"api/users/{id}");
        if (!response.IsSuccessStatusCode) return null;

        var content = await response.Content.ReadAsStringAsync();
        var user = JsonSerializer.Deserialize<UserResponseDto>(content, _jsonOptions);

        if (user != null)
            user.ProfileImage = ResolveImageUrl(user.ProfileImage);

        return user;
    }

    public async Task<UserResponseDto?> GetCurrent(ClaimsPrincipal user)
    {
        if (user.Identity == null || !user.Identity.IsAuthenticated) return null;

        var userIdClaim = user.FindFirst(ClaimTypes.NameIdentifier)?.Value;

        if (!int.TryParse(userIdClaim, out int userId)) return null;

        return await GetById(userId);
    }

    public async Task<UserResponseDto?> GetByEmail(string email)
    {
        var response = await _httpClient.GetAsync($"api/users/email?email={email}");
        if (!response.IsSuccessStatusCode) return null;

        var responseContent = await response.Content.ReadAsStringAsync();

        var user = JsonSerializer.Deserialize<UserResponseDto>(responseContent, _jsonOptions);

        return user;
    }

    public async Task<UserResponseDto?> Create(UserCreateDto userDto)
    {
        using var formData = new MultipartFormDataContent();

        if (!string.IsNullOrWhiteSpace(userDto.Name))
            formData.Add(new StringContent(userDto.Name), "Name");

        if (userDto.ProfileImage != null)
        {
            AddImageToFormData(formData, userDto.ProfileImage);
        }

        var response = await _httpClient.PostAsync("api/users", formData);
        if (!response.IsSuccessStatusCode) return null;

        var responseContent = await response.Content.ReadAsStringAsync();
        var created = JsonSerializer.Deserialize<UserResponseDto>(responseContent, _jsonOptions);

        if (created != null)
            created.ProfileImage = ResolveImageUrl(created.ProfileImage);

        return created;
    }

    public async Task<UserResponseDto?> Update(int id, UserUpdateDto userDto)
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

        if (userDto.Role.HasValue)
            formData.Add(new StringContent(((int)userDto.Role.Value).ToString()), "Role");

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

        return JsonSerializer.Deserialize<UserResponseDto>(responseContent, _jsonOptions);
    }

    public async Task<bool> Delete(int id)
    {
        var response = await _httpClient.DeleteAsync($"api/users/{id}");
        return response.IsSuccessStatusCode;
    }

    private string ResolveImageUrl(string? imagePath, string defaultImage = "img/anonymous-profile.png")
    {
        return string.IsNullOrEmpty(imagePath) ? $"{_apiBaseUrl}{defaultImage}" : $"{_apiBaseUrl}{imagePath}";
    }
    
    private void AddImageToFormData(MultipartFormDataContent formData, IFormFile? image)
    {
        if (image == null) return;

        var allowedExtensions = new[] { ".webp", ".jpg", ".png", ".gif" };
        var extension = Path.GetExtension(image.FileName).ToLower();
        if (!allowedExtensions.Contains(extension))
            throw new Exception("Solo se permiten archivos: " + string.Join(", ", allowedExtensions));

        var fileStream = image.OpenReadStream();
        formData.Add(new StreamContent(fileStream), "SportImage", image.FileName);
    }
}