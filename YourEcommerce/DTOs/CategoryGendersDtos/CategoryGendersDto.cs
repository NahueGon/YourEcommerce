namespace YourEcommerce.DTOs.CategoryGendersDtos;

public class CategoryGendersDto
{
    public int GenderId { get; set; }
    public string GenderName { get; set; } = string.Empty;
    public string? CategoryGenderImage { get; set; } = string.Empty;
}