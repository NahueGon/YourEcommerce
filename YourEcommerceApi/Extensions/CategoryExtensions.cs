using YourEcommerceApi.DTOs.Category;
using YourEcommerceApi.Models;

namespace YourEcommerceApi.Extensions;

public static class CategoryExtensions
{
    public static CategoryResponseDto ToDto(this Category category)
    {
        return new CategoryResponseDto
        {
            Id = category.Id,
            Name = category.Name,
            Description = category.Description,
            ProductTypes = category.ProductTypes?
                .Select(
                    pt => pt.ToDto()
                ).ToList() ?? new()
        };
    }
}