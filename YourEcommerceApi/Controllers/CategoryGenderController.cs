using Microsoft.AspNetCore.Mvc;
using YourEcommerceApi.DTOs.CategoryGender;
using YourEcommerceApi.Services.Interfaces;

namespace YourEcommerceApi.Controllers
{
    [Route("api/category-genders")]
    [ApiController]
    [Tags("CategoryGenders")]
    public class CategoryGenderController : ControllerBase
    {
        private readonly ICategoryGendersService _categoryGenderService;

        public CategoryGenderController(ICategoryGendersService categoryGenderService)
        {
            _categoryGenderService = categoryGenderService;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<CategoryGendersResponseDto>>> GetCategories()
        {
            var categories = await _categoryGenderService.GetAll();
            if (categories == null || !categories.Any()) return NotFound("No se pudo encontrar relación.");

            return Ok(categories);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<CategoryGendersResponseDto>> GetCategory(int id)
        {
            var category = await _categoryGenderService.Get(id);
            if (category == null) return NotFound("Relación no encontrada");

            return Ok(category);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromForm] CategoryGenderCreateDto categoryGenderDto)
        {
            var result = await _categoryGenderService.Save(categoryGenderDto);
            if (result == null) return BadRequest("No se pudo crear la relación.");
            return Ok(result);
        }

        [HttpPatch("{id}")]
        public async Task<IActionResult> Update(int id, [FromForm] CategoryGendersUpdateDto dto)
        {
            if (dto == null)
                return BadRequest("No se recibieron datos para actualizar.");

            var result = await _categoryGenderService.Update(id, dto);

            if (result == null)
                return NotFound("Relación no encontrada.");

            return Ok(result);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var success = await _categoryGenderService.Delete(id);
            if (!success) return NotFound("Relación no encontrada.");
            return NoContent();
        }
    }
}