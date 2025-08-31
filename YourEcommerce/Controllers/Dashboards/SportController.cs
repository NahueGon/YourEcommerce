using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using YourEcommerce.Controllers.Dashboard;
using YourEcommerce.DTOs.SportDtos;
using YourEcommerce.Services.Interfaces;
using YourEcommerce.ViewModels;

namespace YourEcommerce.Controllers.Dashboards;

[Authorize(Roles = "Admin")]
[Route("admin/dashboard/")]
public class SportController : DashboardControllerBase
{
    private readonly ILogger<SportController> _logger;
    private readonly IMapper _mapper;
    private readonly ISportService _sportService;

    public SportController(
        ILogger<SportController> logger,
        IMapper mapper,
        ISportService sportService,
        INotificationService notificationService
    ) : base(notificationService)
    {
        _logger = logger;
        _mapper = mapper;
        _sportService = sportService;
    }

    [HttpGet("sports")]
    public async Task<IActionResult> Index()
    {
        var sports = await _sportService.GetAllForTable();

        var model = new TableDashboardViewModel<SportDto>
        {
            Title = "sports",
            Items = sports,
            TotalItems = sports.Count()
        };

        return DashboardView("Index", model);
    }

    [HttpGet("sports/create", Name = "CreateSport")]
    public IActionResult CreateSport()
    {
        var model = new SportCreateDto();
        return DashboardView("CreateSport", model);
    }

    [HttpPost("sports/create", Name = "StoreSport")]
    public async Task<IActionResult> StoreSport(SportCreateDto sportDto)
    {
        if (!ModelState.IsValid) return DashboardView("CreateSport", sportDto);

        var createdSport = await _sportService.Create(sportDto);
        Notify(
            createdSport != null,
            "Deporte creado correctamente",
            "No se pudo crear el deporte"
        );

        return RedirectToAction(nameof(Index));
    }

    [HttpGet("sports/{id}/edit", Name = "EditSport")]
    public async Task<IActionResult> EditSport(int id)
    {
        var sport = await _sportService.GetForEdit(id);
        if (sport == null) return NotFound();

        var model = _mapper.Map<SportUpdateDto>(sport);
        return DashboardView("EditSport", model);
    }

    [HttpPost("sports/{id}/edit", Name = "UpdateSport")]
    public async Task<IActionResult> UpdateSport(int id, SportUpdateDto sportDto)
    {
        if (!ModelState.IsValid) return DashboardView("EditSport", sportDto);

        var updatedSport = await _sportService.Update(id, sportDto);
        Notify(
            updatedSport != null,
            "Deporte actualizado correctamente",
            "No se pudo actualizar el deporte"
        );

        return RedirectToAction("EditSport", new { id = sportDto.Id });
    }

    [HttpPost("sports/{id}/delete", Name = "DeleteSport")]
    public async Task<IActionResult> DeleteSport(int id)
    {
        var success = await _sportService.Delete(id);
        Notify(
            success,
            "Deporte eliminado correctamente",
            "No se pudo borrar el deporte"
        );

        return RedirectToAction(nameof(Index));
    }
}