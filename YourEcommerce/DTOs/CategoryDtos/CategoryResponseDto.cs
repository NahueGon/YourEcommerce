using System.ComponentModel.DataAnnotations;
using YourEcommerce.DTOs.CategoryGendersDtos;

namespace YourEcommerce.DTOs.CategoryDtos;

public class CategoryResponseDto
{
    public int Id { get; set; }

    [Display(Name = "Nombre")]
    public string Name { get; set; } = string.Empty;

    [Display(Name = "Descripción")]
    public string Description { get; set; } = string.Empty;

    [Display(Name = "Portada")]
    public string? CategoryImage { get; set; } = "/img/anonymous-profile.png";

    [Display(Name = "Géneros")]
    public List<CategoryGendersDto> CategoryGenders { get; set; } = new();
}