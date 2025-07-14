using System;
using YourEcommerceApi.DTOs.Product;

namespace YourEcommerceApi.DTOs.SubCategory;

public class SubcategoryDto
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public string? Description { get; set; }

    public List<ProductResponseDto> Products { get; set; } = new();
}
