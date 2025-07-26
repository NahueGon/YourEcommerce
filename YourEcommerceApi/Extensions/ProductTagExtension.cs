using YourEcommerceApi.DTOs.ProductDtos;
using YourEcommerceApi.DTOs.ProductTagDtos;
using YourEcommerceApi.DTOs.TagDtos;
using YourEcommerceApi.Models.Products;

namespace YourEcommerceApi.Extensions;

public static class ProductTagExtensions
{
    public static ProductTagResponseDto ToDto(this ProductTag productTag)
    {
        if (productTag.Product == null)
            throw new InvalidOperationException("El producto asociado no puede ser null.");

        if (productTag.Tag == null)
            throw new InvalidOperationException("El tag asociado no puede ser null.");

        return new ProductTagResponseDto
        {
            Product = new ProductDto
            {
                Id = productTag.Product.Id,
                Name = productTag.Product.Name
            },
            Tag = new TagDto
            {
                Id = productTag.Tag.Id,
                Name = productTag.Tag.Name
            }
        };
    }
}