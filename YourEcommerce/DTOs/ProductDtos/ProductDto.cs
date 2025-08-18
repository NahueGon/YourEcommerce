using System.ComponentModel.DataAnnotations;

namespace YourEcommerce.DTOs.ProductDtos;

public class ProductDto
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

    [Display(Name = "Categoria")]
    public string? Category { get; set; } = "-";

    [Display(Name = "Deporte")]
    public string? Sport { get; set; } = "-";

    [Display(Name = "Marca")]
    public string? Brand { get; set; } = "-";

    [Display(Name = "Genero")]
    public string? Gender { get; set; } = "-";

    [Display(Name = "Etiquetas")]
    public string? ProductTags { get; set; } = string.Empty;
}