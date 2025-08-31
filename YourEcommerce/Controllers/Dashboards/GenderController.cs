using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using YourEcommerce.Controllers.Dashboard;
using YourEcommerce.DTOs.GenderDtos;
using YourEcommerce.Services.Interfaces;
using YourEcommerce.ViewModels;

namespace YourEcommerce.Controllers.Dashboards;

[Authorize(Roles = "Admin")]
[Route("admin/dashboard/")]
public class GenderController : DashboardControllerBase
{
    private readonly ILogger<GenderController> _logger;
    private readonly IMapper _mapper;
    private readonly IGenderService _genderService;

    public GenderController(
        ILogger<GenderController> logger,
        IMapper mapper,
        IGenderService genderService,
        INotificationService notificationService
    ) : base(notificationService)
    {
        _logger = logger;
        _mapper = mapper;
        _genderService = genderService;
    }

    [HttpGet("genders")]
    public async Task<ActionResult> Index()
    {
        var genders = await _genderService.GetAllForTable();

        var model = new TableDashboardViewModel<GenderDto>
        {
            Title = "genders",
            Items = genders,
            TotalItems = genders.Count()
        };

        return DashboardView("Index", model);
    }

    [HttpGet("genders/create", Name = "CreateGender")]
    public IActionResult CreateGender()
    {
        var model = new GenderCreateDto();
        return DashboardView("CreateGender", model);
    }

    [HttpPost("Genders/create", Name = "StoreGender")]
    public async Task<IActionResult> StoreGender(GenderCreateDto genderDto)
    {
        if (!ModelState.IsValid) return DashboardView("CreateGender", genderDto);

        var createdGender = await _genderService.Create(genderDto);
        Notify(
            createdGender != null,
            "Genero creado correctamente",
            "No se pudo crear el Genero"
        );

        return RedirectToAction(nameof(Index));
    }

    [HttpGet("genders/{id}/edit", Name = "EditGender")]
    public async Task<IActionResult> EditGender(int id)
    {
        var gender = await _genderService.GetForEdit(id);
        if (gender == null) return NotFound();

        var model = _mapper.Map<GenderUpdateDto>(gender);
        return DashboardView("EditGender", model);
    }

    [HttpPost("genders/{id}/edit", Name = "UpdateGender")]
    public async Task<IActionResult> UpdateGender(int id, GenderUpdateDto genderDto)
    {
        if (!ModelState.IsValid) return DashboardView("EditGender", genderDto);

        var updatedGender = await _genderService.Update(id, genderDto);
        Notify(
            updatedGender != null,
            "Genero actualizado correctamente",
            "No se pudo actualizar el Genero"
        );

        return RedirectToAction("EditGender", new { id = genderDto.Id });
    }

    [HttpPost("genders/{id}/delete", Name = "DeleteGender")]
    public async Task<IActionResult> DeleteGender(int id)
    {
        var success = await _genderService.Delete(id);
        Notify(
            success,
            "Genero eliminado correctamente",
            "No se pudo borrar el Genero"
        );

        return RedirectToAction(nameof(Index));
    }
}