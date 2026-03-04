using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Project3Travelin.Dtos.FaqDtos;
using Project3Travelin.Services.FaqServices;

namespace Project3Travelin.Areas.Admin.Controllers;
[Area("Admin")]
public class FaqController : Controller
{
    private readonly IFaqService  _FaqService;
    private readonly IMapper _mapper;
    // GET
    public FaqController(IFaqService FaqService, IMapper mapper)
    {
        _FaqService = FaqService;
        _mapper = mapper;
    }

    public async Task<IActionResult> Index()
    {
        var values = await _FaqService.GetAllFaqsAsync();
        return View(values);
    }

    public async Task<IActionResult> CreateFaq()
    {
        return View();
    }
    
    [HttpPost]
    public async Task<IActionResult> CreateFaq(CreateFaqDto createFaqDto)
    {
        await _FaqService.CreateFaqAsync(createFaqDto);
        return RedirectToAction("Index");
    }
    
    public async Task<IActionResult> EditFaq(string id)
    {
        var values = await _FaqService.GetFaqByIdAsync(id);
        var updateDto = _mapper.Map<UpdateFaqDto>(values);
        return View(updateDto);
    }
    
    [HttpPost]
    public async Task<IActionResult> EditFaq(UpdateFaqDto updateFaqDto)
    {
        await _FaqService.UpdateFaqAsync(updateFaqDto);
        return RedirectToAction("Index");
    }

    public async Task<IActionResult> DeleteFaq(string id)
    {
        await _FaqService.DeleteFaqAsync(id);
        return RedirectToAction("Index");   
    }
}