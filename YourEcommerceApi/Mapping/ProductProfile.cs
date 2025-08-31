using AutoMapper;
using YourEcommerceApi.DTOs.ProductAttributeDtos;
using YourEcommerceApi.DTOs.ProductVariantDtos;
using YourEcommerceApi.Models.Products;
using YourEcommerceApi.DTOs.Product;
using YourEcommerceApi.DTOs.ProductDtos;
using YourEcommerceApi.DTOs.ProductColorDtos;

public class ProductProfile : Profile
{
    public ProductProfile()
    {
        // Lectura Product con total stock
        CreateMap<Product, ProductResponseDto>()
            .ForMember(dest => dest.TotalStock, opt => opt.MapFrom(src => src.ProductVariants != null ? src.ProductVariants.Sum(v => v.Stock) : 0));

        // Creación / actualización Product
        CreateMap<ProductCreateDto, Product>()
            .ForMember(dest => dest.ProductVariants, opt => opt.MapFrom(src => src.ProductVariants))
            .ForMember(dest => dest.ProductAttributes, opt => opt.MapFrom(src => src.ProductAttributes))
            .ForMember(dest => dest.ProductTags, opt => opt.MapFrom(src => src.ProductTags));
        
        CreateMap<ProductUpdateDto, Product>();

        // Variants
        CreateMap<ProductVariant, ProductVariantDto>()
            .ForMember(dest => dest.Images, opt => opt.MapFrom(src => src.Images));
        CreateMap<ProductVariantDto, ProductVariant>()
            .ForMember(dest => dest.Colors, opt => opt.MapFrom(src => src.Colors))
            .ForMember(dest => dest.Images, opt => opt.MapFrom(src => src.Images));
        CreateMap<ProductVariantCreateDto, ProductVariant>()
            .ForMember(dest => dest.Colors, opt => opt.MapFrom(src => src.Colors))
            .ForMember(dest => dest.Images, opt => opt.MapFrom(src => src.Images));

        // Attributes
        CreateMap<ProductAttribute, ProductAttributeDto>();
        CreateMap<ProductAttributeCreateDto, ProductAttribute>();

        // Colors (solo creación)
        CreateMap<ProductColorCreateDto, ProductColor>();
        CreateMap<ProductColor, ProductColorDto>();
    }
}
