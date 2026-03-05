using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Project3Travelin.Dtos.InstagramDtos;
using Project3Travelin.Services.InstagramServices;

namespace Project3Travelin.Areas.Admin.Controllers;
[Area("Admin")]
public class InstagramController : Controller
{
    private readonly IInstagramService  _InstagramService;
    private readonly IMapper _mapper;
    // GET
    public InstagramController(IInstagramService InstagramService, IMapper mapper)
    {
        _InstagramService = InstagramService;
        _mapper = mapper;
    }

    public async Task<IActionResult> Index()
    {
        var values = await _InstagramService.GetAllInstagramsAsync();
        return View(values);
    }

    public async Task<IActionResult> CreateInstagram()
    {
        return View();
    }
    
    [HttpPost]
    public async Task<IActionResult> CreateInstagram(CreateInstagramDto createInstagramDto)
    {
        await _InstagramService.CreateInstagramAsync(createInstagramDto);
        return RedirectToAction("Index");
    }
    
    public async Task<IActionResult> EditInstagram(string id)
    {
        var values = await _InstagramService.GetInstagramByIdAsync(id);
        var updateDto = _mapper.Map<UpdateInstagramDto>(values);
        return View(updateDto);
    }
    
    [HttpPost]
    public async Task<IActionResult> EditInstagram(UpdateInstagramDto updateInstagramDto)
    {
        await _InstagramService.UpdateInstagramAsync(updateInstagramDto);
        return RedirectToAction("Index");
    }

    public async Task<IActionResult> DeleteInstagram(string id)
    {
        await _InstagramService.DeleteInstagramAsync(id);
        return RedirectToAction("Index");   
    }
}