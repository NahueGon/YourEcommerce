using Microsoft.AspNetCore.Mvc;
using YourEcommerceApi.DTOs.Product;
using YourEcommerceApi.Services.Interfaces;

namespace YourEcommerceApi.Controllers
{
    [Route("api/products/[controller]")]
    [ApiController]
    public class ShoeController : ControllerBase
    {
        IProductService _productService;

        public ShoeController(IProductService service)
        {
            _productService = service;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<ProductResponseDto>>> GetShoes()
        {
            var products = await _productService.GetAll();

            return Ok(products);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<ProductResponseDto>> GetShoe(int id)
        {
            var product = await _productService.Get(id);

            if (product == null)
                return NotFound("Producto no encontrado");

            return Ok(product);
        }

        [HttpPost]
        public async Task<ActionResult<ProductResponseDto>> CreateShoe(ShoeCreateDto shoeDto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var responseDto = await _productService.SaveShoe(shoeDto);

            return CreatedAtAction(nameof(GetShoe), new { id = responseDto.Id }, responseDto);
        }
    }
}
