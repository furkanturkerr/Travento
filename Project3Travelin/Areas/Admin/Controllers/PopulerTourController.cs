using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Project3Travelin.Dtos.PopulerDtos;
using Project3Travelin.Services.PopulerService;

namespace Project3Travelin.Areas.Admin.Controllers;
[Area("Admin")]
public class PopulerTourController : Controller
{
    private readonly IPopulerService _populerService;
    private readonly IMapper _mapper;
    // GET
    public PopulerTourController(IPopulerService populerService, IMapper mapper)
    {
        _populerService = populerService;
        _mapper = mapper;
    }
    
    public async Task<IActionResult> PopulerTourList()
    {
        var values = await _populerService.GetAllPopulersAsync();
        return View(values);
    }

    public async Task<IActionResult> CreatePopulerTour()
    {
        return View(); 
    }
    
    [HttpPost]
    public async Task<IActionResult> CreatePopulerTour(CreatePopulerDto createPopulerDto)
    {
        await _populerService.CreatePopulerAsync(createPopulerDto);
        return RedirectToAction("PopulerTourList");
    }

    public async Task<IActionResult>  EditPopulerTour(string id)
    {
        var values = await _populerService.GetPopulerByIdAsync(id);
        var updateDto = _mapper.Map<UpdatePopulerDto>(values);
        return View(updateDto);
    }
    
    [HttpPost]
    public async Task<IActionResult> EditPopulerTour(UpdatePopulerDto updatePopulerDto)
    {
        await _populerService.UpdatePopulerAsync(updatePopulerDto);
        return RedirectToAction("PopulerTourList");
    }

    public async Task<IActionResult> DeletePopulerTour(string id)
    {
        await _populerService.DeletePopulerAsync(id);
        return RedirectToAction("PopulerTourList");
    }
}