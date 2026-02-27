using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Project3Travelin.Dtos.TourDtos;
using Project3Travelin.Services.TourServices;

namespace Project3Travelin.Areas.Admin.Controllers;
[Area("Admin")]
public class TourController : Controller
{
    private readonly ITourService _tourService;
    private readonly IMapper _mapper;

    public TourController(ITourService tourService, IMapper mapper)
    {
        _tourService = tourService;
        _mapper = mapper;
    }

    // GET
    public async Task<IActionResult> TourList()
    {
        var value = await _tourService.GetAllTourAsync();
        return View(value);
    }
    
    public IActionResult CreateTour()
    {
        return View();
    }
    
    [HttpPost]
    public async Task<IActionResult> CreateTour(CreateTourDto createTourDto)
    {
        await _tourService.CreateTourAsync(createTourDto);
        return RedirectToAction("TourList");
    }

    public async Task<IActionResult> Delete(string id)
    {
        await _tourService.DeleteTourAsync(id);
        return RedirectToAction("TourList");
    }

    [HttpGet]
    public async Task<IActionResult> UpdateTour(string id)
    {
        var value = await _tourService.GetTourByIdAsync(id);

        var updateDto = _mapper.Map<UpdateTourDto>(value);

        return View(updateDto);
    }

    [HttpPost]
    public async Task<IActionResult> UpdateTour(UpdateTourDto updateTourDto)
    {
        await _tourService.UpdateTourAsync(updateTourDto);
        return RedirectToAction("TourList");
    }
}