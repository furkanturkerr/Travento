using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Project3Travelin.Dtos.SliderDtos;
using Project3Travelin.Services.SliderServices;

namespace Project3Travelin.Areas.Admin.Controllers;
[Area("Admin")]
public class SliderController : Controller
{
    private readonly ISliderService _sliderService;
    private readonly IMapper _mapper;

    public SliderController(ISliderService sliderService, IMapper mapper)
    {
        _sliderService = sliderService;
        _mapper = mapper;
    }

    // GET
    public async Task<IActionResult> Index()
    {
        var value = await _sliderService.GetAllSlidersAsync();
        return View(value);
    }
    
    [HttpGet]
    public async Task<IActionResult> CreateSlider()
    {
        return View();
    }

    [HttpPost]
    public async Task<IActionResult> CreateSlider(CreateSliderDto createSliderDto)
    {
        await _sliderService.CreateSliderAsync(createSliderDto);
        return View();
    }

    [HttpGet]
    public async Task<IActionResult> EditSlider(string id)
    {
        var values = await _sliderService.GetSliderByIdAsync(id);
        var updateDto = _mapper.Map<UpdateSliderDto>(values);
        return View(updateDto);
    }
    
    [HttpPost]
    public async Task<IActionResult> EditSlider(UpdateSliderDto updateSliderDto)
    {
        await _sliderService.UpdateSliderAsync(updateSliderDto);
        return RedirectToAction("Index");
    }
}