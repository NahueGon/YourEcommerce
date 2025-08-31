using YourEcommerceApi.DTOs.CategoryGender;

namespace YourEcommerceApi.DTOs.CategoryDtos;

public class CategoryFormCreateDto
{
    public required string Name { get; set; }
    public string? Description { get; set; } = string.Empty;
    public IFormFile? CategoryImageFile { get; set; }

    public List<CategoryGenderCreateDto>? CategoryGenders { get; set; } = new();
}