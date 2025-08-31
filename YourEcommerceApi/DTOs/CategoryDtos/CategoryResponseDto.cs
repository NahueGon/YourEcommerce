using YourEcommerceApi.DTOs.CategoryGender;

namespace YourEcommerceApi.DTOs.CategoryDtos;

public class CategoryResponseDto
{
    public int Id { get; set; }
    public string? Name { get; set; }
    public string? Description { get; set; } = string.Empty;
    public string? CategoryImage { get; set; }

    public List<CategoryGendersDto> CategoryGenders { get; set; } = new();
}