using Microsoft.AspNetCore.Mvc;
using YourEcommerceApi.DTOs.Product;
using YourEcommerceApi.Services.Interfaces;

namespace YourEcommerceApi.Controllers
{
    [Route("api/products/[controller]")]
    [ApiController]
    public class ClothController : ControllerBase
    {
        IProductService _productService;

        public ClothController(IProductService service)
        {
            _productService = service;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<ProductResponseDto>>> GetClothes()
        {
            var products = await _productService.GetAll();

            return Ok(products);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<ProductResponseDto>> GetCloth(int id)
        {
            var product = await _productService.Get(id);

            if (product == null)
                return NotFound("Producto no encontrado");

            return Ok(product);
        }

        [HttpPost]
        public async Task<ActionResult<ProductResponseDto>> CreateCloth(ClothCreateDto clothDto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var responseDto = await _productService.SaveCloth(clothDto);

            return CreatedAtAction(nameof(GetCloth), new { id = responseDto.Id }, responseDto);
        }
    }
}
