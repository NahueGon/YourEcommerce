using YourEcommerceApi.DTOs.BrandDtos;
using YourEcommerceApi.DTOs.CategoryDtos;
using YourEcommerceApi.DTOs.ProductAttributeDtos;
using YourEcommerceApi.DTOs.ProductColorDtos;
using YourEcommerceApi.DTOs.ProductDtos;
using YourEcommerceApi.DTOs.ProductTagDtos;
using YourEcommerceApi.DTOs.ProductVariantDtos;
using YourEcommerceApi.DTOs.SportDtos;
using YourEcommerceApi.DTOs.TagDtos;
using YourEcommerceApi.Models.Products;

namespace YourEcommerceApi.Extensions;

public static class ProductExtensions
{
    public static ProductResponseDto ToDto(this Product product)
    {
        return new ProductResponseDto
        {
            Id = product.Id,
            Name = product.Name,
            Description = product.Description,
            Price = product.Price,
            TotalStock = product.TotalStock,
            Gender = product.Gender,
            Category = product.Category == null
                ? null
                : new CategoryDto
                {
                    Id = product.Category.Id,
                    Name = product.Category.Name
                },
            Brand = product.Brand == null
                ? null
                : new BrandDto
                {
                    Id = product.Brand.Id,
                    Name = product.Brand.Name
                },
            Sport = product.Sport == null
                ? null
                : new SportDto
                {
                    Id = product.Sport.Id,
                    Name = product.Sport.Name
                },
            ProductAttributes = product.ProductAttributes?
                .Select(pa => new ProductAttributeDto
                {
                    Key = pa.Key,
                    Value = pa.Value
                }).ToList() ?? new List<ProductAttributeDto>(),
            ProductTags = product.ProductTags?
                .Select(pt => new ProductTagDto
                {
                    Tag = new TagDto 
                    {
                        Id = pt.Tag.Id,
                        Name = pt.Tag.Name,
                        Group = pt.Tag.Group
                    }
                }).ToList() ?? new List<ProductTagDto>(),
            ProductVariants = product.ProductVariants?
                .Select(pv => new ProductVariantDto
                {
                    Size = pv.Size,
                    Stock = pv.Stock,
                    Colors = pv.Colors == null
                    ? new List<ProductColorDto>()
                    : pv.Colors.Select(c => new ProductColorDto
                    {
                        Id = c.Id,
                        Name = c.Name
                    }).ToList()
                }).ToList() ?? new List<ProductVariantDto>()
        };
    }
}