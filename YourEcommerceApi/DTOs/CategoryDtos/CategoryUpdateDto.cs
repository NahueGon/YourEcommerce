namespace YourEcommerceApi.DTOs.CategoryDtos;

public class CategoryUpdateDto
{
    public string? Name { get; set; }
    public string? Description { get; set; }
    public IFormFile? CategoryImage { get; set; }
}