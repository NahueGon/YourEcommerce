namespace YourEcommerceApi.DTOs.SubCategory;

public class SubcategoryUpdateDto
{
    public required string Name { get; set; }
    public string? Description { get; set; } 
    public int ProductTypeId { get; set; }
}
