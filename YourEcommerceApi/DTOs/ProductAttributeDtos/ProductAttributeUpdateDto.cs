namespace YourEcommerceApi.DTOs.ProductAttributeDtos;

public class ProductAttributeUpdateDto
{
    public required string Key { get; set; }
    public required string Value { get; set; }

    public int ProductId { get; set; }
}