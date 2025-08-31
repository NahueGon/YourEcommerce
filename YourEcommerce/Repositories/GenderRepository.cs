using System.Text.Json;
using Microsoft.Extensions.Options;
using YourEcommerce.Config;
using YourEcommerce.DTOs.GenderDtos;
using YourEcommerce.Repositories.Interfaces;

namespace YourEcommerce.Repositories;

public class GenderRepository : IGenderRepository
{
    private readonly HttpClient _httpClient;
    private readonly JsonSerializerOptions _jsonOptions;
    private readonly string _apiBaseUrl;

    public GenderRepository(HttpClient httpClient, JsonSerializerOptions jsonOptions, IOptions<ApiSettings> apiSettings)
    {
        _httpClient = httpClient;
        _jsonOptions = jsonOptions;
        if (string.IsNullOrEmpty(apiSettings.Value.BaseUrl))
            throw new ArgumentException("ApiSettings.BaseUrl no está configurado en appsettings.json");

        _apiBaseUrl = apiSettings.Value.BaseUrl;
    }

    public async Task<List<GenderResponseDto>> GetAll()
    {
        var response = await _httpClient.GetAsync("api/genders");
        if (!response.IsSuccessStatusCode) return new();

        var content = await response.Content.ReadAsStringAsync();
        var genders = JsonSerializer.Deserialize<List<GenderResponseDto>>(content, _jsonOptions) ?? new List<GenderResponseDto>();

        genders.ForEach(s => s.GenderImage = ResolveImageUrl(s.GenderImage));

        return genders;
    }

    public async Task<GenderResponseDto?> GetById(int id)
    {
        var response = await _httpClient.GetAsync($"api/genders/{id}");
        if (!response.IsSuccessStatusCode) return null;

        var content = await response.Content.ReadAsStringAsync();
        var gender = JsonSerializer.Deserialize<GenderResponseDto>(content, _jsonOptions);

        if (gender != null)
            gender.GenderImage = ResolveImageUrl(gender.GenderImage);

        return gender;
    }

    public async Task<GenderResponseDto?> Create(GenderCreateDto genderDto)
    {
        using var formData = new MultipartFormDataContent();

        if (!string.IsNullOrWhiteSpace(genderDto.Name))
            formData.Add(new StringContent(genderDto.Name), "Name");

        if (!string.IsNullOrWhiteSpace(genderDto.Description))
            formData.Add(new StringContent(genderDto.Description), "Description");

        if (genderDto.GenderImage != null)
        {
            AddImageToFormData(formData, genderDto.GenderImage);
        }

        var response = await _httpClient.PostAsync("api/genders", formData);
        if (!response.IsSuccessStatusCode) return null;

        var responseContent = await response.Content.ReadAsStringAsync();
        var created = JsonSerializer.Deserialize<GenderResponseDto>(responseContent, _jsonOptions);

        if (created != null)
            created.GenderImage = ResolveImageUrl(created.GenderImage);

        return created;
    }

    public async Task<GenderResponseDto?> Update(int id, GenderUpdateDto genderDto)
    {
        using var formData = new MultipartFormDataContent();

        if (!string.IsNullOrWhiteSpace(genderDto.Name))
            formData.Add(new StringContent(genderDto.Name), "Name");

        if (!string.IsNullOrWhiteSpace(genderDto.Description))
            formData.Add(new StringContent(genderDto.Description), "Description");

        if (genderDto.GenderImage != null)
        {
            AddImageToFormData(formData, genderDto.GenderImage);
        }

        var request = new HttpRequestMessage(new HttpMethod("PATCH"), $"api/genders/{id}")
        {
            Content = formData
        };

        var response = await _httpClient.SendAsync(request);
        if (!response.IsSuccessStatusCode) return null;

        var responseContent = await response.Content.ReadAsStringAsync();
        return JsonSerializer.Deserialize<GenderResponseDto>(responseContent, _jsonOptions);
    }

    public async Task<bool> Delete(int id)
    {
        var response = await _httpClient.DeleteAsync($"api/genders/{id}");
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
        formData.Add(new StreamContent(fileStream), "GenderImage", image.FileName);
    }
}