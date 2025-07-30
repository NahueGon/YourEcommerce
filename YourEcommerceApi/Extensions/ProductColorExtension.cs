using YourEcommerceApi.DTOs.ProductColorDtos;
using YourEcommerceApi.DTOs.ProductVariantDtos;
using YourEcommerceApi.Models.Products;

namespace YourEcommerceApi.Extensions;

public static class ProductColorExtensions
{
    public static ProductColorResponseDto ToDto(this ProductColor productColor)
    {
        return new ProductColorResponseDto
        {
            Id = productColor.Id,
            Name = productColor.Name,
            ProductVariants = productColor.ProductVariant == null
            ? new List<ProductVariantDto>()
            : new List<ProductVariantDto>
            {
                new ProductVariantDto
                {
                    Size = productColor.ProductVariant.Size,
                    Stock = productColor.ProductVariant.Stock
                }
            }
        };
    }
}