using YourEcommerceApi.DTOs.SubCategory;
using YourEcommerceApi.Models;

namespace YourEcommerceApi.Extensions;

public static class SubCategoryExtensions
{
    public static SubcategoryResponseDto ToDto(this SubCategory subcategory)
    {
        return new SubcategoryResponseDto
        {
            Id = subcategory.Id,
            Name = subcategory.Name,
            Products = subcategory.Products?
                .Select(
                    sc => sc.ToDto()
                ).ToList() ?? new()
        };
    }
}