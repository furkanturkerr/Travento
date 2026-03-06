using Microsoft.AspNetCore.Mvc;
using Project3Travelin.Services.PopulerService;

namespace Project3Travelin.ViewComponents.HomeViewComponents;

public class _HomePoulerTourComponentPartial : ViewComponent
{
    private readonly IPopulerService _populerService;

    public _HomePoulerTourComponentPartial(IPopulerService populerService)
    {
        _populerService = populerService;
    }

    public async Task<IViewComponentResult> InvokeAsync()
    {
        var values = await _populerService.GetAllPopulersAsync();
        return View(values);
    }
}