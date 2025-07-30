using Microsoft.AspNetCore.Mvc;
using YourEcommerceApi.DTOs.Product;
using YourEcommerceApi.DTOs.ProductDtos;
using YourEcommerceApi.Services.Interfaces;

namespace YourEcommerceApi.Controllers
{
    [Route("api/products")]
    [ApiController]
    [Tags("Products")]
    public class ProductController : ControllerBase
    {
        private readonly IProductService _productService;

        public ProductController(IProductService service)
        {
            _productService = service;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<ProductResponseDto>>> GetProducts()
        {
            var products = await _productService.GetAll();
            if (products == null || !products.Any()) return NotFound("No se encontraron productos.");

            return Ok(products);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<ProductResponseDto>> GetProduct(int id)
        {
            var product = await _productService.Get(id);
            if (product == null) return NotFound("Producto no encontrado");

            return Ok(product);
        }

        [HttpPost]
        public async Task<IActionResult> CreateProduct([FromBody] ProductCreateDto productDto)
        {
            var created = await _productService.Save(productDto);
            if (created == null) return BadRequest("Categoria, Marca o deporte no encontrados.");

            return CreatedAtAction(nameof(GetProduct), new { id = created.Id }, created);
        }

        [HttpPatch("{id}")]
        public async Task<ActionResult<ProductResponseDto>> UpdateProduct(int id, [FromBody] ProductUpdateDto productDto)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);

            var existingProduct = await _productService.Get(id);
            if (existingProduct == null) return NotFound("Producto no encontrado");

            var updatedProduct = await _productService.Update(id, productDto);
            if (updatedProduct == null) return NotFound("Producto no encontrado");

            return Ok(updatedProduct);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteProduct(int id)
        {
            var deleted = await _productService.Get(id);
            if (deleted == null) return NotFound("Producto no encontrada");

            await _productService.Delete(id);

            return NoContent();
        }
    }
}