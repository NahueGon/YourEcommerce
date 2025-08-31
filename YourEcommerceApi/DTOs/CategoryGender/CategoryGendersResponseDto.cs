using YourEcommerceApi.DTOs.CategoryDtos;
using YourEcommerceApi.DTOs.GenderDtos;

namespace YourEcommerceApi.DTOs.CategoryGender;

public class CategoryGendersResponseDto
{
    public int Id { get; set; }
    public string? Name { get; set; } = string.Empty;
    public string? CategoryGenderImage { get; set; }

    public CategoryDto Category { get; set; } = new CategoryDto();
    public GenderDto Gender { get; set; } = new GenderDto();
}