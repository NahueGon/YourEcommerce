// using Microsoft.AspNetCore.Mvc;
// using YourEcommerceApi.DTOs.ProductVariantDtos;
// using YourEcommerceApi.Services.Interfaces;

// namespace YourEcommerceApi.Controllers
// {
//     [Route("api/product-variants")]
//     [ApiController]
//     [Tags("ProductVariants")]
//     public class ProductVariantController : ControllerBase
//     {
//         private readonly IProductVariantService _productVariantService;

//         public ProductVariantController(IProductVariantService service)
//         {
//             _productVariantService = service;
//         }

//         [HttpGet]
//         public async Task<ActionResult<IEnumerable<ProductVariantResponseDto>>> GetProductVariants()
//         {
//             var productVariants = await _productVariantService.GetAll();
//             if (productVariants == null || !productVariants.Any()) return NotFound("No se encontraron variantes de productos.");

//             return Ok(productVariants);
//         }

//         [HttpGet("{id}")]
//         public async Task<ActionResult<ProductVariantResponseDto>> GetProductVariant(int id)
//         {
//             var productVariant = await _productVariantService.Get(id);
//             if (productVariant == null) return NotFound("Variante de producto no encontrado");

//             return Ok(productVariant);
//         }

//         [HttpPost]
//         public async Task<IActionResult> CreateProductVariant([FromBody] ProductVariantCreateDto productVariantDto)
//         {
//             var created = await _productVariantService.Save(productVariantDto);
//             if (created == null) return BadRequest("Marca o deporte no encontrados.");

//             return CreatedAtAction(nameof(GetProductVariant), new { id = created.Id }, created);
//         }

//         [HttpDelete("{id}")]
//         public async Task<ActionResult> DeleteProductVariant(int id)
//         {
//             var deleted = await _productVariantService.Get(id);
//             if (deleted == null) return NotFound("Producto no encontrada");

//             await _productVariantService.Delete(id);

//             return NoContent();
//         }
//     }
// }
