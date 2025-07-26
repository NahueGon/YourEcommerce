using YourEcommerceApi.DTOs.ProductColorDtos;
using YourEcommerceApi.DTOs.ProductDtos;
using YourEcommerceApi.DTOs.ProductVariantDtos;
using YourEcommerceApi.Models.Products;

namespace YourEcommerceApi.Extensions;

public static class ProductVariantExtensions
{
    public static ProductVariantResponseDto ToDto(this ProductVariant productVariant)
    {
        return new ProductVariantResponseDto
        {
            Id = productVariant.Id,
            Size = productVariant.Size,
            Stock = productVariant.Stock,
            Product = productVariant.Product == null
                ? null
                : new ProductDto
                {
                    Id = productVariant.Product.Id,
                    Name = productVariant.Product.Name
                },
            Colors = productVariant.Colors == null
                ? new List<ProductColorDto>()
                : productVariant.Colors.Select(c => new ProductColorDto
                {
                    Id = c.Id,
                    Name = c.Name
                }).ToList()
        };
    }
}