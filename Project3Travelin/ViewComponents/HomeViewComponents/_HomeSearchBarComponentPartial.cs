// ViewComponents/HomeViewComponents/_HomeSearchBarComponentPartial.cs
using Microsoft.AspNetCore.Mvc;
using Project3Travelin.Services.TourServices;

namespace Project3Travelin.ViewComponents.HomeViewComponents;

public class _HomeSearchBarComponentPartial : ViewComponent
{
    private readonly ITourService _tourService;

    public _HomeSearchBarComponentPartial(ITourService tourService)
    {
        _tourService = tourService;
    }

    public async Task<IViewComponentResult> InvokeAsync()
    {
        var cities = await _tourService.GetAllCitiesAsync();
        return View(cities);
    }
}