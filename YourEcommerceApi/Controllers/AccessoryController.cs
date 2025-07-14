using Microsoft.AspNetCore.Mvc;
using YourEcommerceApi.DTOs.Product;
using YourEcommerceApi.Services.Interfaces;

namespace YourEcommerceApi.Controllers
{
    [Route("api/products/[controller]")]
    [ApiController]
    public class AccessoryController : ControllerBase
    {
        IProductService _productService;

        public AccessoryController(IProductService service)
        {
            _productService = service;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<ProductResponseDto>>> GetAccessories()
        {
            var products = await _productService.GetAll();

            return Ok(products);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<ProductResponseDto>> GetAccesory(int id)
        {
            var product = await _productService.Get(id);

            if (product == null)
                return NotFound("Producto no encontrado");

            return Ok(product);
        }

        [HttpPost]
        public async Task<ActionResult<ProductResponseDto>> CreateAccesory(AccessoryCreateDto accessoryDto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var responseDto = await _productService.SaveAccessory(accessoryDto);

            return CreatedAtAction(nameof(GetAccesory), new { id = responseDto.Id }, responseDto);
        }
    }
}
