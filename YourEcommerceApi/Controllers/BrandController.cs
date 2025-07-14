using Microsoft.AspNetCore.Mvc;
using YourEcommerceApi.DTOs.Brand;
using YourEcommerceApi.Services.Interfaces;

namespace YourEcommerceApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BrandController : ControllerBase
    {
        IBrandService _brandService;

        public BrandController(IBrandService service)
        {
            _brandService = service;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<BrandResponseDto>>> GetBrands()
        {
            var brands = await _brandService.GetAll();

            return Ok(brands);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<BrandResponseDto>> GetBrand(int id)
        {
            var brand = await _brandService.Get(id);

            if (brand == null)
                return NotFound("Marca no encontrada");

            return Ok(brand);
        }

        [HttpPost]
        public async Task<ActionResult<BrandResponseDto>> CreateBrand(BrandCreateDto brandDto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var responseDto = await _brandService.Save(brandDto);

            return CreatedAtAction(nameof(GetBrand), new { id = responseDto.Id }, responseDto);
        }

        [HttpPut]
        public async Task<ActionResult> UpdateBrand(int id, BrandUpdateDto brandDto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var updated = await _brandService.Get(id);

            if (updated == null)
                return NotFound("Marca no encontrada");

            await _brandService.Update(id, brandDto);

            return NoContent();
        }

        [HttpDelete]
        public async Task<ActionResult> DeleteBrand(int id)
        {
            var brand = await _brandService.Get(id);

            if (brand == null)
                return NotFound("Marca no encontrada");

            await _brandService.Delete(id);
            return NoContent();
        }
    }
}
