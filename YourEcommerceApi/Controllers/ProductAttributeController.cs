// using Microsoft.AspNetCore.Mvc;
// using YourEcommerceApi.DTOs.ProductAttributeDtos;
// using YourEcommerceApi.Services.Interfaces;

// namespace YourEcommerceApi.Controllers
// {
//     [Route("api/product-attributes")]
//     [ApiController]
//     [Tags("ProductAttributes")]
//     public class ProductAttributeController : ControllerBase
//     {
//         private readonly IProductAttributeService _productAttributeService;

//         public ProductAttributeController(IProductAttributeService service)
//         {
//             _productAttributeService = service;
//         }

//         [HttpGet]
//         public async Task<ActionResult<IEnumerable<ProductAttributeResponseDto>>> GetProductAttributes()
//         {
//             var productAttributes = await _productAttributeService.GetAll();
//             if (productAttributes == null || !productAttributes.Any()) return NotFound("No se encontraron atributos.");

//             return Ok(productAttributes);
//         }

//         [HttpGet("{id}")]
//         public async Task<ActionResult<ProductAttributeResponseDto>> GetProductAttribute(int id)
//         {
//             var productAttribute = await _productAttributeService.Get(id);
//             if (productAttribute == null) return NotFound("Atributo no encontrado");

//             return Ok(productAttribute);
//         }

//         [HttpPost]
//         public async Task<IActionResult> CreateProductAttribute([FromBody] ProductAttributeCreateDto productAttributeDto)
//         {
//             var created = await _productAttributeService.Save(productAttributeDto);

//             return CreatedAtAction(nameof(GetProductAttribute), new { id = created.Id }, created);
//         }

//         [HttpDelete("{id}")]
//         public async Task<ActionResult> DeleteProduct(int id)
//         {
//             var deleted = await _productAttributeService.Get(id);
//             if (deleted == null) return NotFound("Atributo no encontrada");

//             await _productAttributeService.Delete(id);

//             return NoContent();
//         }
//     }
// }