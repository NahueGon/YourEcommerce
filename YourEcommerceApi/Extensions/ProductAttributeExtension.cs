using YourEcommerceApi.DTOs.ProductAttributeDtos;
using YourEcommerceApi.Models.Products;

namespace YourEcommerceApi.Extensions;

public static class ProductAttributeExtension
{
    public static ProductAttributeResponseDto ToDto(this ProductAttribute productAttribute)
    {
        return new ProductAttributeResponseDto
        {
            Id = productAttribute.Id,
            Key = productAttribute.Key,
            Value = productAttribute.Value,
            ProductId = productAttribute.ProductId
        };
    }
}