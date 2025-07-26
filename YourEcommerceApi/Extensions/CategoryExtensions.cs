using YourEcommerceApi.DTOs.Category;
using YourEcommerceApi.DTOs.ProductDtos;
using YourEcommerceApi.Models.Products;

namespace YourEcommerceApi.Extensions;

public static class CategoryExtensions
{
    public static CategoryResponseDto ToDto(this Category category)
    {
        return new CategoryResponseDto
        {
            Id = category.Id,
            Name = category.Name,
            Description = category.Description ?? string.Empty,
            Products = category.Products?
                .Select(p => new ProductDto
                {
                    Id = p.Id,
                    Name = p.Name
                }).ToList() ?? []
        };
    }
}