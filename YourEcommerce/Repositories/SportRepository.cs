using System.Text.Json;
using Microsoft.Extensions.Options;
using YourEcommerce.Config;
using YourEcommerce.DTOs.SportDtos;
using YourEcommerce.Repositories.Interfaces;

namespace YourEcommerce.Repositories;

public class SportRepository : ISportRepository
{
    private readonly HttpClient _httpClient;
    private readonly JsonSerializerOptions _jsonOptions;
    private readonly string _apiBaseUrl;

    public SportRepository(HttpClient httpClient, JsonSerializerOptions jsonOptions, IOptions<ApiSettings> apiSettings)
    {
        _httpClient = httpClient;
        _jsonOptions = jsonOptions;
        if (string.IsNullOrEmpty(apiSettings.Value.BaseUrl))
            throw new ArgumentException("ApiSettings.BaseUrl no está configurado en appsettings.json");

        _apiBaseUrl = apiSettings.Value.BaseUrl;
    }

    public async Task<List<SportResponseDto>> GetAll()
    {
        var response = await _httpClient.GetAsync("api/sports");
        if (!response.IsSuccessStatusCode) return new();

        var content = await response.Content.ReadAsStringAsync();
        var sports = JsonSerializer.Deserialize<List<SportResponseDto>>(content, _jsonOptions) ?? new List<SportResponseDto>();

        sports.ForEach(s => s.SportImage = ResolveImageUrl(s.SportImage));

        return sports;
    }

    public async Task<SportResponseDto?> GetById(int id)
    {
        var response = await _httpClient.GetAsync($"api/sports/{id}");
        if (!response.IsSuccessStatusCode) return null;

        var content = await response.Content.ReadAsStringAsync();
        var sport = JsonSerializer.Deserialize<SportResponseDto>(content, _jsonOptions);

        if (sport != null)
            sport.SportImage = ResolveImageUrl(sport.SportImage);

        return sport;
    }

    public async Task<SportResponseDto?> Create(SportCreateDto sportDto)
    {
        using var formData = new MultipartFormDataContent();

        if (!string.IsNullOrWhiteSpace(sportDto.Name))
            formData.Add(new StringContent(sportDto.Name), "Name");

        if (!string.IsNullOrWhiteSpace(sportDto.Description))
            formData.Add(new StringContent(sportDto.Description), "Description");

        if (sportDto.SportImage != null)
        {
            AddImageToFormData(formData, sportDto.SportImage);
        }

        var response = await _httpClient.PostAsync("api/sports", formData);
        if (!response.IsSuccessStatusCode) return null;

        var responseContent = await response.Content.ReadAsStringAsync();
        var created = JsonSerializer.Deserialize<SportResponseDto>(responseContent, _jsonOptions);

        if (created != null)
            created.SportImage = ResolveImageUrl(created.SportImage);

        return created;
    }

    public async Task<SportResponseDto?> Update(int id, SportUpdateDto sportDto)
    {
        using var formData = new MultipartFormDataContent();

        if (!string.IsNullOrWhiteSpace(sportDto.Name))
            formData.Add(new StringContent(sportDto.Name), "Name");

        if (!string.IsNullOrWhiteSpace(sportDto.Description))
            formData.Add(new StringContent(sportDto.Description), "Description");

        if (sportDto.SportImage != null)
        {
            AddImageToFormData(formData, sportDto.SportImage);
        }

        var request = new HttpRequestMessage(new HttpMethod("PATCH"), $"api/sports/{id}")
        {
            Content = formData
        };

        var response = await _httpClient.SendAsync(request);
        if (!response.IsSuccessStatusCode) return null;

        var responseContent = await response.Content.ReadAsStringAsync();
        return JsonSerializer.Deserialize<SportResponseDto>(responseContent, _jsonOptions);
    }

    public async Task<bool> Delete(int id)
    {
        var response = await _httpClient.DeleteAsync($"api/sports/{id}");
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