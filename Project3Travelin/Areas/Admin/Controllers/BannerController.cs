using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Project3Travelin.Dtos.BannerDtos;
using Project3Travelin.Services.BannerService;

namespace Project3Travelin.Areas.Admin.Controllers;
[Area("Admin")]
public class BannerController : Controller
{
    private readonly IBannerService  _BannerService;
    private readonly IMapper _mapper;
    // GET
    public BannerController(IBannerService BannerService, IMapper mapper)
    {
        _BannerService = BannerService;
        _mapper = mapper;
    }

    public async Task<IActionResult> Index()
    {
        var values = await _BannerService.GetAllBannersAsync();
        return View(values);
    }

    public async Task<IActionResult> CreateBanner()
    {
        return View();
    }
    
    [HttpPost]
    public async Task<IActionResult> CreateBanner(CreateBannerDto createBannerDto)
    {
        await _BannerService.CreateBannerAsync(createBannerDto);
        return RedirectToAction("Index");
    }
    
    public async Task<IActionResult> EditBanner(string id)
    {
        var values = await _BannerService.GetBannerByIdAsync(id);
        var updateDto = _mapper.Map<UpdateBannerDto>(values);
        return View(updateDto);
    }
    
    [HttpPost]
    public async Task<IActionResult> EditBanner(UpdateBannerDto updateBannerDto)
    {
        await _BannerService.UpdateBannerAsync(updateBannerDto);
        return RedirectToAction("Index");
    }

    public async Task<IActionResult> DeleteBanner(string id)
    {
        await _BannerService.DeleteBannerAsync(id);
        return RedirectToAction("Index");   
    }
}