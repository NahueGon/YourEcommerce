namespace YourEcommerceApi.DTOs.ProductAttributeDtos;

public class ProductAttributeCreateDto
{
    public required string Key { get; set; }
    public required string Value { get; set; }

    public int ProductId { get; set; }
}