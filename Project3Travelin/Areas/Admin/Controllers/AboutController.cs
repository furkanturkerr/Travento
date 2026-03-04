using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Project3Travelin.Dtos.AboutDtos;
using Project3Travelin.Services.AboutService;

namespace Project3Travelin.Areas.Admin.Controllers;
[Area("Admin")]
public class AboutController : Controller
{
    private readonly IAboutService  _aboutService;
    private readonly IMapper _mapper;
    // GET
    public AboutController(IAboutService aboutService, IMapper mapper)
    {
        _aboutService = aboutService;
        _mapper = mapper;
    }

    public async Task<IActionResult> Index()
    {
        var values = await _aboutService.GetAllAboutAsync();
        return View(values);
    }

    public async Task<IActionResult> CreateAbout()
    {
        return View();
    }
    
    [HttpPost]
    public async Task<IActionResult> CreateAbout(CreateAboutDto createAboutDto)
    {
        await _aboutService.CreateAboutAsync(createAboutDto);
        return RedirectToAction("Index");
    }
    
    public async Task<IActionResult> EditAbout(string id)
    {
        var values = await _aboutService.GetAboutByIdAsync(id);
        var updateDto = _mapper.Map<UpdateAboutDto>(values);
        return View(updateDto);
    }
    
    [HttpPost]
    public async Task<IActionResult> EditAbout(UpdateAboutDto updateAboutDto)
    {
        await _aboutService.UpdateAboutAsync(updateAboutDto);
        return RedirectToAction("Index");
    }

    public async Task<IActionResult> DeleteAbout(string id)
    {
        await _aboutService.DeleteAboutAsync(id);
        return RedirectToAction("Index");   
    }
    
}