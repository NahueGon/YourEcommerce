using Microsoft.AspNetCore.Mvc;
using YourEcommerceApi.DTOs.ProductColorDtos;
using YourEcommerceApi.Services.Interfaces;

namespace YourEcommerceApi.Controllers
{
    [Route("api/product-colors")]
    [ApiController]
    [Tags("ProductColors")]
    public class ProductColorController : ControllerBase
    {
        private readonly IProductColorService _productColorService;

        public ProductColorController(IProductColorService service)
        {
            _productColorService = service;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<ProductColorResponseDto>>> GetProductColors()
        {
            var productColors = await _productColorService.GetAll();
            if (productColors == null || !productColors.Any()) return NotFound("No se encontraron colores de productos.");

            return Ok(productColors);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<ProductColorResponseDto>> GetProductColor(int id)
        {
            var productColor = await _productColorService.Get(id);
            if (productColor == null) return NotFound("Color de producto no encontrado");

            return Ok(productColor);
        }

        [HttpPost]
        public async Task<IActionResult> CreateProductColor([FromBody] ProductColorCreateDto productColorDto)
        {
            var created = await _productColorService.Save(productColorDto);
            if (created == null) return BadRequest("Marca o deporte no encontrados.");

            return CreatedAtAction(nameof(GetProductColor), new { id = created.Id }, created);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteProductColor(int id)
        {
            var deleted = await _productColorService.Get(id);
            if (deleted == null) return NotFound("Producto no encontrada");

            await _productColorService.Delete(id);

            return NoContent();
        }
    }
}