using Microsoft.AspNetCore.Mvc;
using YourEcommerceApi.DTOs.SportDtos;
using YourEcommerceApi.Services.Interfaces;

namespace YourEcommerceApi.Controllers
{
    [Route("api/sports")]
    [ApiController]
    [Tags("Sports")]
    public class SportController : ControllerBase
    {
        private readonly ISportService _sportService;

        public SportController(ISportService service)
        {
            _sportService = service;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<SportResponseDto>>> GetSports()
        {
            var sports = await _sportService.GetAll();
            if (sports == null || !sports.Any()) return NotFound("No se encontraron deportes.");

            return Ok(sports);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<SportResponseDto>> GetSport(int id)
        {
            var sport = await _sportService.Get(id);
            if (sport == null) return NotFound("Marca no encontrada");

            return Ok(sport);
        }

        [HttpPost]
        public async Task<ActionResult<SportResponseDto>> CreateSport(SportCreateDto sportDto)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);
            var responseDto = await _sportService.Save(sportDto);

            return CreatedAtAction(nameof(GetSport), new { id = responseDto.Id }, responseDto);
        }

        [HttpPut]
        public async Task<ActionResult> UpdateSport(int id, SportUpdateDto sportDto)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);

            var updated = await _sportService.Get(id);
            if (updated == null) return NotFound("Deporte no encontrado");

            await _sportService.Update(id, sportDto);

            return NoContent();
        }

        [HttpDelete]
        public async Task<ActionResult> DeleteSport(int id)
        {
            var sport = await _sportService.Get(id);
            if (sport == null) return NotFound("Deporte no encontrado");

            await _sportService.Delete(id);

            return NoContent();
        }
    }
}