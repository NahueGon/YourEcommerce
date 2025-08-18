using Microsoft.AspNetCore.Mvc;
using YourEcommerceApi.DTOs.GenderDtos;
using YourEcommerceApi.Services.Interfaces;

namespace YourEcommerceApi.Controllers
{
    [Route("api/genders")]
    [ApiController]
    [Tags("Genders")]
    public class GenderController : ControllerBase
    {
        private readonly IGenderService _genderService;

        public GenderController(IGenderService service)
        {
            _genderService = service;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<GenderResponseDto>>> GetGenders()
        {
            var genders = await _genderService.GetAll();
            if (genders == null || !genders.Any()) return NotFound("No se encontraron generos.");

            return Ok(genders);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<GenderResponseDto>> GetGender(int id)
        {
            var gender = await _genderService.Get(id);
            if (gender == null) return NotFound("Genero no encontrada");

            return Ok(gender);
        }

        [HttpPost]
        public async Task<ActionResult<GenderResponseDto>> CreateGender(GenderCreateDto genderDto)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);
            var responseDto = await _genderService.Save(genderDto);

            return CreatedAtAction(nameof(GetGender), new { id = responseDto.Id }, responseDto);
        }

        [HttpPatch("{id}")]
        [Consumes("multipart/form-data")]
        public async Task<ActionResult<GenderResponseDto>> UpdateGender(int id, [FromForm] GenderUpdateDto genderDto)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);

            var updated = await _genderService.Get(id);
            if (updated == null) return NotFound("Genero no encontrada");

            var updatedGender = await _genderService.Update(id, genderDto);
            if (updatedGender == null) return NotFound("Genero no encontrada");

            return Ok(updatedGender);
        }

        [HttpDelete]
        public async Task<ActionResult> DeleteGender(int id)
        {
            var gender = await _genderService.Get(id);
            if (gender == null) return NotFound("Genero no encontrada");

            await _genderService.Delete(id);

            return NoContent();
        }
    }
}