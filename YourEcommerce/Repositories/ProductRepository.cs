using System.Text.Json;
using Microsoft.Extensions.Options;
using YourEcommerce.Config;
using YourEcommerce.DTOs.ProductDtos;
using YourEcommerce.Repositories.Interfaces;

namespace YourEcommerce.Repositories;

public class ProductRepository : IProductRepository
{
    private readonly HttpClient _httpClient;
    private readonly JsonSerializerOptions _jsonOptions;
    private readonly string _apiBaseUrl;

    public ProductRepository(HttpClient httpClient, JsonSerializerOptions jsonOptions, IOptions<ApiSettings> apiSettings)
    {
        _httpClient = httpClient;
        _jsonOptions = jsonOptions;
        if (string.IsNullOrEmpty(apiSettings.Value.BaseUrl))
            throw new ArgumentException("ApiSettings.BaseUrl no está configurado en appsettings.json");

        _apiBaseUrl = apiSettings.Value.BaseUrl;
    }

    public async Task<List<ProductResponseDto>> GetAll()
    {
        var response = await _httpClient.GetAsync("api/products");
        if (!response.IsSuccessStatusCode) return new();

        var content = await response.Content.ReadAsStringAsync();
        var products = JsonSerializer.Deserialize<List<ProductResponseDto>>(content, _jsonOptions) ?? new List<ProductResponseDto>();

        // products.ForEach(s => s.ProductImage = ResolveImageUrl(s.ProductImage));

        return products;
    }

    public async Task<ProductResponseDto?> GetById(int id)
    {
        var response = await _httpClient.GetAsync($"api/products/{id}");
        if (!response.IsSuccessStatusCode) return null;

        var content = await response.Content.ReadAsStringAsync();
        var product = JsonSerializer.Deserialize<ProductResponseDto>(content, _jsonOptions);

        // if (Product != null)
        //     Product.ProductImage = ResolveImageUrl(Product.ProductImage);

        return product;
    }

    public async Task<ProductResponseDto?> Create(ProductCreateDto productDto)
    {
        using var formData = new MultipartFormDataContent();

        if (!string.IsNullOrWhiteSpace(productDto.Name))
            formData.Add(new StringContent(productDto.Name), "Name");

        if (!string.IsNullOrWhiteSpace(productDto.Description))
            formData.Add(new StringContent(productDto.Description), "Description");

        // if (productDto.ProductImage != null)
        // {
        //     AddImageToFormData(formData, productDto.ProductImage);
        // }

        var response = await _httpClient.PostAsync("api/products", formData);
        if (!response.IsSuccessStatusCode) return null;

        var responseContent = await response.Content.ReadAsStringAsync();
        var created = JsonSerializer.Deserialize<ProductResponseDto>(responseContent, _jsonOptions);

        // if (created != null)
        //     created.ProductImage = ResolveImageUrl(created.ProductImage);

        return created;
    }

    public async Task<ProductResponseDto?> Update(int id, ProductUpdateDto productDto)
    {
        using var formData = new MultipartFormDataContent();

        if (!string.IsNullOrWhiteSpace(productDto.Name))
            formData.Add(new StringContent(productDto.Name), "Name");

        if (!string.IsNullOrWhiteSpace(productDto.Description))
            formData.Add(new StringContent(productDto.Description), "Description");

        // if (productDto.ProductImage != null)
        // {
        //     AddImageToFormData(formData, productDto.ProductImage);
        // }

        var request = new HttpRequestMessage(new HttpMethod("PATCH"), $"api/products/{id}")
        {
            Content = formData
        };

        var response = await _httpClient.SendAsync(request);
        if (!response.IsSuccessStatusCode) return null;

        var responseContent = await response.Content.ReadAsStringAsync();
        return JsonSerializer.Deserialize<ProductResponseDto>(responseContent, _jsonOptions);
    }

    public async Task<bool> Delete(int id)
    {
        var response = await _httpClient.DeleteAsync($"api/products/{id}");
        return response.IsSuccessStatusCode;
    }

    // private string ResolveImageUrl(string? imagePath, string defaultImage = "img/anonymous-profile.png")
    // {
    //     return string.IsNullOrEmpty(imagePath) ? $"{_apiBaseUrl}{defaultImage}" : $"{_apiBaseUrl}{imagePath}";
    // }
    
    // private void AddImageToFormData(MultipartFormDataContent formData, IFormFile? image)
    // {
    //     if (image == null) return;

    //     var allowedExtensions = new[] { ".webp", ".jpg", ".png", ".gif" };
    //     var extension = Path.GetExtension(image.FileName).ToLower();
    //     if (!allowedExtensions.Contains(extension))
    //         throw new Exception("Solo se permiten archivos: " + string.Join(", ", allowedExtensions));

    //     var fileStream = image.OpenReadStream();
    //     formData.Add(new StreamContent(fileStream), "ProductImage", image.FileName);
    // }
}