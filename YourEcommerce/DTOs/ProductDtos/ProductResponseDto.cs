using System.ComponentModel.DataAnnotations;
using YourEcommerce.DTOs.BrandDtos;
using YourEcommerce.DTOs.CategoryDtos;
using YourEcommerce.DTOs.GenderDtos;
using YourEcommerce.DTOs.ProductTagDtos;
using YourEcommerce.DTOs.SportDtos;

namespace YourEcommerce.DTOs.ProductDtos;

public class ProductResponseDto
{
    public int Id { get; set; }

    [Display(Name = "Nombre")]
    public string Name { get; set; } = string.Empty;

    [Display(Name = "Descripción")]
    public string? Description { get; set; } = string.Empty;

    [Display(Name = "Precio")]
    public decimal Price { get; set; }

    [Display(Name = "Stock")]
    public int TotalStock { get; set; }

    [Display(Name = "Activo")]
    public bool IsActive { get; set; }

    [Display(Name = "Categoria")]
    public CategoryDto Category { get; set; } = new();

    [Display(Name = "Deporte")]
    public SportDto Sport { get; set; } = new();

    [Display(Name = "Marca")]
    public BrandDto Brand { get; set; } = new();

    [Display(Name = "Genero")]
    public GenderDto Gender { get; set; } = new();
    public ICollection<ProductTagResponseDto> ProductTags { get; set; } = new List<ProductTagResponseDto>();
}