namespace YourEcommerce.DTOs.CategoryGendersDtos;

public class CategoryGendersUpdateDto
{
    public int GenderId { get; set; } 
    public string? Name { get; set; } = string.Empty;
    public bool IsSelected { get; set; } = false;
    
    public IFormFile? CategoryGenderImage { get; set; }
    public string? CategoryGenderImageUrl { get; set; }  
}