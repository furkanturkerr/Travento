using Microsoft.AspNetCore.Mvc;
using Project3Travelin.Services.TourServices;

namespace Project3Travelin.ViewComponents.HomeViewComponents;

public class _HomeNewTourComponentPartial : ViewComponent
{
    private readonly ITourService _tourService;

    public _HomeNewTourComponentPartial(ITourService tourService)
    {
        _tourService = tourService;
    }

    public async Task<IViewComponentResult> InvokeAsync()
    {
        var values = await _tourService.GetAllTourAsync();
        return View(values);
    }
}