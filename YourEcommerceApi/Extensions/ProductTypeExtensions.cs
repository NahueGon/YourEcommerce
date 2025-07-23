using YourEcommerceApi.DTOs.ProductType;
using YourEcommerceApi.Models;

namespace YourEcommerceApi.Extensions;

public static class ProductTypeExtensions
{
    public static ProductTypeResponseDto ToDto(this ProductType productType)
    {
        return new ProductTypeResponseDto
        {
            Id = productType.Id,
            Name = productType.Name,
            SubCategories = productType.Subcategories?
                .Select(
                    sc => sc.ToDto()
                ).ToList() ?? new()
        };
    }
}