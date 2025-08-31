using System.Text.Json;
using Microsoft.Extensions.Options;
using YourEcommerce.Config;
using YourEcommerce.DTOs.CategoryDtos;
using YourEcommerce.Repositories.Interfaces;

namespace YourEcommerce.Repositories;

public class CategoryRepository : ICategoryRepository
{
    private readonly HttpClient _httpClient;
    private readonly JsonSerializerOptions _jsonOptions;
    private readonly string _apiBaseUrl;

    public CategoryRepository(HttpClient httpClient, JsonSerializerOptions jsonOptions, IOptions<ApiSettings> apiSettings)
    {
        _httpClient = httpClient;
        _jsonOptions = jsonOptions;
        if (string.IsNullOrEmpty(apiSettings.Value.BaseUrl))
            throw new ArgumentException("ApiSettings.BaseUrl no está configurado en appsettings.json");

        _apiBaseUrl = apiSettings.Value.BaseUrl;
    }

    public async Task<List<CategoryResponseDto>> GetAll()
    {
        var response = await _httpClient.GetAsync("api/categories");
        if (!response.IsSuccessStatusCode) return new();

        var content = await response.Content.ReadAsStringAsync();
        var categories = JsonSerializer.Deserialize<List<CategoryResponseDto>>(content, _jsonOptions) ?? new List<CategoryResponseDto>();

        categories.ForEach(s => s.CategoryImage = ResolveImageUrl(s.CategoryImage));

        return categories;
    }

    public async Task<CategoryResponseDto?> GetById(int id)
    {
        var response = await _httpClient.GetAsync($"api/categories/{id}");
        if (!response.IsSuccessStatusCode) return null;

        var content = await response.Content.ReadAsStringAsync();
        var category = JsonSerializer.Deserialize<CategoryResponseDto>(content, _jsonOptions);

        if (category != null)
            category.CategoryImage = ResolveImageUrl(category.CategoryImage);

        return category;
    }

    public async Task<CategoryResponseDto?> Create(CategoryCreateDto categoryDto)
    {
        using var formData = new MultipartFormDataContent();

        if (!string.IsNullOrWhiteSpace(categoryDto.Name))
            formData.Add(new StringContent(categoryDto.Name), "Name");

        if (!string.IsNullOrWhiteSpace(categoryDto.Description))
            formData.Add(new StringContent(categoryDto.Description), "Description");

        if (categoryDto.CategoryImage != null)
        {
            AddImageToFormData(formData, categoryDto.CategoryImage);
        }

        var response = await _httpClient.PostAsync("api/categories", formData);
        if (!response.IsSuccessStatusCode) return null;

        var responseContent = await response.Content.ReadAsStringAsync();
        var created = JsonSerializer.Deserialize<CategoryResponseDto>(responseContent, _jsonOptions);

        if (created != null)
            created.CategoryImage = ResolveImageUrl(created.CategoryImage);

        return created;
    }

    public async Task<CategoryResponseDto?> Update(int id, CategoryUpdateDto categoryDto)
    {
        using var formData = new MultipartFormDataContent();

        if (!string.IsNullOrWhiteSpace(categoryDto.Name))
            formData.Add(new StringContent(categoryDto.Name), "Name");

        if (!string.IsNullOrWhiteSpace(categoryDto.Description))
            formData.Add(new StringContent(categoryDto.Description), "Description");

        if (categoryDto.CategoryImage != null)
        {
            AddImageToFormData(formData, categoryDto.CategoryImage);
        }

        var request = new HttpRequestMessage(new HttpMethod("PATCH"), $"api/categories/{id}")
        {
            Content = formData
        };

        var response = await _httpClient.SendAsync(request);
        if (!response.IsSuccessStatusCode) return null;

        var responseContent = await response.Content.ReadAsStringAsync();
        return JsonSerializer.Deserialize<CategoryResponseDto>(responseContent, _jsonOptions);
    }

    public async Task<bool> Delete(int id)
    {
        var response = await _httpClient.DeleteAsync($"api/categories/{id}");
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
        formData.Add(new StreamContent(fileStream), "CategoryImage", image.FileName);
    }
}