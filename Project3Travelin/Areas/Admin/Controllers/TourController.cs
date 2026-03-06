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
    public async Task<IActionResult> TourList(int page = 1)
    {
        var value = await _tourService.GetAllTourAsync();
        
        int pageSize = 9;
        int totalCount = value.Count;
        int totalPages = (int)Math.Ceiling(totalCount / (double)pageSize);
        var paged = value.Skip((page - 1) * pageSize).Take(pageSize).ToList();
        
        ViewBag.CurrentPage = page;
        ViewBag.TotalPages = totalPages;
        ViewBag.TotalCount = totalCount;
        
        return View(paged);
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

    public async Task<IActionResult> DeleteTour(string id)
    {
        await _tourService.DeleteTourAsync(id);
        return RedirectToAction("TourList");
    }

    [HttpGet]
    public async Task<IActionResult> EditTour(string id)
    {
        var value = await _tourService.GetTourByIdAsync(id);

        var updateDto = _mapper.Map<UpdateTourDto>(value);

        return View(updateDto);
    }

    [HttpPost]
    public async Task<IActionResult> EditTour(UpdateTourDto updateTourDto)
    {
        await _tourService.UpdateTourAsync(updateTourDto);
        return RedirectToAction("TourList");
    }
}