namespace YourEcommerceApi.DTOs.CategoryGender;

public class CategoryGenderCreateDto
{
    public required int GenderId { get; set; }
    public required int CategoryId { get; set; }
    public required string Name { get; set; }
    public IFormFile? CategoryGenderImage { get; set; }
}