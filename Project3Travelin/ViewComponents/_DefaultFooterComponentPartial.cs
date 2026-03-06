using Microsoft.AspNetCore.Mvc;
using Project3Travelin.Services.InstagramServices;

namespace Project3Travelin.ViewComponents.TourViewComponents;

public class _DefaultFooterComponentPartial : ViewComponent
{
    private readonly IInstagramService _ınstagramService;

    public _DefaultFooterComponentPartial(IInstagramService ınstagramService)
    {
        _ınstagramService = ınstagramService;
    }

    public async Task<IViewComponentResult> InvokeAsync()
    {
        var values = await _ınstagramService.GetAllInstagramsAsync();
        return View(values.FirstOrDefault());
    }
}