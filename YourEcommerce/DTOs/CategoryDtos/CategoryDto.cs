using System.ComponentModel.DataAnnotations;

namespace YourEcommerce.DTOs.CategoryDtos;

public class CategoryDto
{
    public int Id { get; set; }

    [Display(Name = "Nombre")]
    public string Name { get; set; } = string.Empty;

    [Display(Name = "Descripción")]
    public string Description { get; set; } = string.Empty;

    [Display(Name = "Portada")]
    public string? CategoryImage { get; set; } = "/img/anonymous-profile.png";
}