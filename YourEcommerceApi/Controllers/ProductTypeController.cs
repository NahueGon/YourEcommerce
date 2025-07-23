using Microsoft.AspNetCore.Mvc;
using YourEcommerceApi.DTOs.ProductType;
using YourEcommerceApi.Services.Interfaces;

namespace YourEcommerceApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductTypeController : ControllerBase
    {
        IProductTypeService _productTypeService;

        public ProductTypeController(IProductTypeService service)
        {
            _productTypeService = service;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<ProductTypeResponseDto>>> GetProductTypes()
        {
            var productTypes = await _productTypeService.GetAll();

            return Ok(productTypes);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<ProductTypeResponseDto>> GetProductType(int id)
        {
            var productType = await _productTypeService.Get(id);

            if (productType == null) return NotFound("Tipo no encontrado");

            return Ok(productType);
        }

        [HttpPost]
        public async Task<ActionResult<ProductTypeResponseDto>> CreateProductType(ProductTypeCreateDto productTypeDto)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);

            var responseDto = await _productTypeService.Save(productTypeDto);

            return CreatedAtAction(nameof(GetProductType), new { id = responseDto.Id }, responseDto);
        }

        [HttpPatch("{id}")]
        public async Task<ActionResult> UpdateProductType(int id, ProductTypeUpdateDto productTypeDto)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);

            var updated = await _productTypeService.Get(id);

            if (updated == null) return NotFound("Tipo no encontrado");

            await _productTypeService.Update(id, productTypeDto);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteProductType(int id)
        {
            var deleted = await _productTypeService.Get(id);

            if (deleted == null) return NotFound("Tipo no encontrado");

            await _productTypeService.Delete(id);
            return NoContent();
        }
    }
}
