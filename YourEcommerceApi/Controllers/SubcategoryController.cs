using Microsoft.AspNetCore.Mvc;
using YourEcommerceApi.DTOs.SubCategory;
using YourEcommerceApi.Services.Interfaces;

namespace YourEcommerceApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SubcategoryController : ControllerBase
    {
        ISubcategoryService _subcategoryService;

        public SubcategoryController(ISubcategoryService service)
        {
            _subcategoryService = service;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<SubcategoryResponseDto>>> GetSubcategories()
        {
            var subcategories = await _subcategoryService.GetAll();

            return Ok(subcategories);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<SubcategoryResponseDto>> GetSubcategory(int id)
        {
            var subcategory = await _subcategoryService.Get(id);

            if (subcategory == null) return NotFound("Subcategoría no encontrada");

            return Ok(subcategory);
        }

        [HttpPost]
        public async Task<ActionResult<SubcategoryResponseDto>> CreateSubcategory(SubcategoryCreateDto subcategoryDto)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);

            var responseDto = await _subcategoryService.Save(subcategoryDto);

            return CreatedAtAction(nameof(GetSubcategory), new { id = responseDto.Id }, responseDto);
        }

        [HttpPatch("{id}")]
        public async Task<ActionResult> UpdateSubcategory(int id, SubcategoryUpdateDto subcategoryDto)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);

            var updated = await _subcategoryService.Get(id);

            if (updated == null)
                return NotFound("Subcategoría no encontrada");

            await _subcategoryService.Update(id, subcategoryDto);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteSubcategory(int id)
        {
            var deleted = await _subcategoryService.Get(id);

            if (deleted == null) return NotFound("Subcategoría no encontrada");

            await _subcategoryService.Delete(id);
            return NoContent();
        }
    }
}
