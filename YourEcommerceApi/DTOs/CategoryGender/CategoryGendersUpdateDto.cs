namespace YourEcommerceApi.DTOs.CategoryGender;

public class CategoryGendersUpdateDto
{
    public string? Name  { get; set; } = string.Empty;
    public IFormFile? CategoryGenderImage { get; set; }
}