using System.Text.Json;
using Microsoft.Extensions.Options;
using YourEcommerce.Config;
using YourEcommerce.DTOs.BrandDtos;
using YourEcommerce.Repositories.Interfaces;

namespace YourEcommerce.Repositories;

public class BrandRepository : IBrandRepository
{
    private readonly HttpClient _httpClient;
    private readonly JsonSerializerOptions _jsonOptions;
    private readonly string _apiBaseUrl;

    public BrandRepository(HttpClient httpClient, JsonSerializerOptions jsonOptions, IOptions<ApiSettings> apiSettings)
    {
        _httpClient = httpClient;
        _jsonOptions = jsonOptions;
        if (string.IsNullOrEmpty(apiSettings.Value.BaseUrl))
            throw new ArgumentException("ApiSettings.BaseUrl no está configurado en appsettings.json");

        _apiBaseUrl = apiSettings.Value.BaseUrl;
    }

    public async Task<List<BrandResponseDto>> GetAll()
    {
        var response = await _httpClient.GetAsync("api/brands");
        if (!response.IsSuccessStatusCode) return new();

        var content = await response.Content.ReadAsStringAsync();
        var Brands = JsonSerializer.Deserialize<List<BrandResponseDto>>(content, _jsonOptions) ?? new List<BrandResponseDto>();

        Brands.ForEach(s => s.BrandImage = ResolveImageUrl(s.BrandImage));

        return Brands;
    }

    public async Task<BrandResponseDto?> GetById(int id)
    {
        var response = await _httpClient.GetAsync($"api/brands/{id}");
        if (!response.IsSuccessStatusCode) return null;

        var content = await response.Content.ReadAsStringAsync();
        var brand = JsonSerializer.Deserialize<BrandResponseDto>(content, _jsonOptions);

        if (brand != null)
            brand.BrandImage = ResolveImageUrl(brand.BrandImage);

        return brand;
    }

    public async Task<BrandResponseDto?> Create(BrandCreateDto brandDto)
    {
        using var formData = new MultipartFormDataContent();

        if (!string.IsNullOrWhiteSpace(brandDto.Name))
            formData.Add(new StringContent(brandDto.Name), "Name");

        if (!string.IsNullOrWhiteSpace(brandDto.Description))
            formData.Add(new StringContent(brandDto.Description), "Description");

        if (brandDto.BrandImage != null)
        {
            AddImageToFormData(formData, brandDto.BrandImage);
        }

        var response = await _httpClient.PostAsync("api/brands", formData);
        if (!response.IsSuccessStatusCode) return null;

        var responseContent = await response.Content.ReadAsStringAsync();
        var created = JsonSerializer.Deserialize<BrandResponseDto>(responseContent, _jsonOptions);

        if (created != null)
            created.BrandImage = ResolveImageUrl(created.BrandImage);

        return created;
    }

    public async Task<BrandResponseDto?> Update(int id, BrandUpdateDto brandDto)
    {
        using var formData = new MultipartFormDataContent();

        if (!string.IsNullOrWhiteSpace(brandDto.Name))
            formData.Add(new StringContent(brandDto.Name), "Name");

        if (!string.IsNullOrWhiteSpace(brandDto.Description))
            formData.Add(new StringContent(brandDto.Description), "Description");

        if (brandDto.BrandImage != null)
        {
            AddImageToFormData(formData, brandDto.BrandImage);
        }

        var request = new HttpRequestMessage(new HttpMethod("PATCH"), $"api/brands/{id}")
        {
            Content = formData
        };

        var response = await _httpClient.SendAsync(request);
        if (!response.IsSuccessStatusCode) return null;

        var responseContent = await response.Content.ReadAsStringAsync();
        return JsonSerializer.Deserialize<BrandResponseDto>(responseContent, _jsonOptions);
    }

    public async Task<bool> Delete(int id)
    {
        var response = await _httpClient.DeleteAsync($"api/brands/{id}");
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
        formData.Add(new StreamContent(fileStream), "BrandImage", image.FileName);
    }
}