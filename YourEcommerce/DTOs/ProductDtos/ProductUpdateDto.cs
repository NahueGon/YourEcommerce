using System.ComponentModel.DataAnnotations;
using YourEcommerce.DTOs.BrandDtos;
using YourEcommerce.DTOs.CategoryDtos;
using YourEcommerce.DTOs.GenderDtos;
using YourEcommerce.DTOs.SportDtos;
using YourEcommerce.DTOs.TagDtos;

namespace YourEcommerce.DTOs.ProductDtos;

public class ProductUpdateDto
{
    [Display(Name = "Producto")]
    public string Name { get; set; } = string.Empty;

    [Display(Name = "Descripcion")]
    public string? Description { get; set; } = string.Empty;

    [Display(Name = "Precio")]
    public decimal Price { get; set; }

    [Display(Name = "Total")]
    public int TotalStock { get; set; }

    [Display(Name = "Activo")]
    public bool IsActive { get; set; } = true;

    [Display(Name = "Categoria")]
    public int CategoryId { get; set; }
    public IEnumerable<CategoryDto> Categories { get; set; } = new List<CategoryDto>();

    [Display(Name = "Deporte")]
    public int SportId { get; set; }
    public IEnumerable<SportDto> Sports { get; set; } = new List<SportDto>();

    [Display(Name = "Marca")]
    public int BrandId { get; set; }
    public IEnumerable<BrandDto> Brands { get; set; } = new List<BrandDto>();

    [Display(Name = "Genero")]
    public int GenderId { get; set; }
    public IEnumerable<GenderDto> Genders { get; set; } = new List<GenderDto>();

    [Display(Name = "TagsId")]
    public List<int> ProductTagIds { get; set; } = new();

    [Display(Name = "Etiquetas")]
    public IEnumerable<TagDto> ProductTags { get; set; } = new List<TagDto>();
}
