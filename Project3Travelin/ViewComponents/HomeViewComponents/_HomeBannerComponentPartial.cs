using Microsoft.AspNetCore.Mvc;
using Project3Travelin.Services.BannerService;

namespace Project3Travelin.ViewComponents.HomeViewComponents;

public class _HomeBannerComponentPartial : ViewComponent
{
    private readonly IBannerService _bannerService;

    public _HomeBannerComponentPartial(IBannerService bannerService)
    {
        _bannerService = bannerService;
    }

    public async Task<IViewComponentResult> InvokeAsync()
    {
        var values = await _bannerService.GetAllBannersAsync();
        return View(values.FirstOrDefault());
    }
}