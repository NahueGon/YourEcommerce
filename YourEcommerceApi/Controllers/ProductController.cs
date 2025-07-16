using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Filters;
using YourEcommerceApi.DTOs.BrandDtos;
using YourEcommerceApi.DTOs.Product;
using YourEcommerceApi.DTOs.Product.Examples;
using YourEcommerceApi.DTOs.ProductDtos.ShoeDtos;
using YourEcommerceApi.DTOs.SportDtos;
using YourEcommerceApi.DTOs.SubCategory;
using YourEcommerceApi.Models;
using YourEcommerceApi.Services.Interfaces;

namespace YourEcommerceApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        IProductService _productService;

        public ProductController(IProductService service)
        {
            _productService = service;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<ProductResponseDto>>> GetProducts()
        {
            var products = await _productService.GetAll();

            return Ok(products);
        }

        [HttpGet("{id:int}")]
        public async Task<ActionResult<ProductResponseDto>> GetProduct(int id)
        {
            var product = await _productService.Get(id);

            if (product == null)
                return NotFound("Producto no encontrado");

            return Ok(product);
        }

        [HttpGet("type/{type:alpha}")]
        public async Task<ActionResult<IEnumerable<ProductResponseDto>>> GetByType(string type)
        {
            var validTypes = new[] { "cloth", "shoe", "accessory" };

            if (!validTypes.Contains(type.ToLower()))
                return BadRequest("Tipo de producto no válido.");

            var products = await _productService.GetAllByType(type);

            if (!products.Any())
                return NotFound($"No se encontraron productos del tipo '{type}'.");

            return Ok(products);
        }

        [HttpPost]
        [ApiExplorerSettings(IgnoreApi = true)]
        public IActionResult CreateProduct()
        {
            return BadRequest("Use /shoe, /accessory o /cloth para crear productos específicos.");
        }

        [HttpPost("shoe")]
        public async Task<IActionResult> CreateShoe([FromBody] ShoeCreateDto shoeDto)
        {
            var created = await _productService.SaveShoe(shoeDto);
            return CreatedAtAction(nameof(GetProduct), new { id = created.Id }, created);
        }

        [HttpPost("accessory")]
        public async Task<IActionResult> CreateAccessory([FromBody] AccessoryCreateDto accessoryDto)
        {
            var created = await _productService.SaveAccessory(accessoryDto);
            return CreatedAtAction(nameof(GetProduct), new { id = created.Id }, created);
        }

        [HttpPost("cloth")]
        public async Task<IActionResult> CreateCloth([FromBody] ClothCreateDto clothDto)
        {
            var created = await _productService.SaveCloth(clothDto);
            return CreatedAtAction(nameof(GetProduct), new { id = created.Id }, created);
        }

        [HttpDelete("{id:int}")]
        public async Task<ActionResult> DeleteProduct(int id)
        {
            var deleted = await _productService.Get(id);

            if (deleted == null)
                return NotFound("Producto no encontrada");

            await _productService.Delete(id);
            return NoContent();
        }
    }
}
