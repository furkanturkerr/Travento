using Microsoft.AspNetCore.Mvc;
using Project3Travelin.Services.AboutService;

namespace Project3Travelin.ViewComponents.HomeViewComponents;

public class _HomeAboutComponentPartial : ViewComponent
{
    private readonly IAboutService _aboutService;

    public _HomeAboutComponentPartial(IAboutService aboutService)
    {
        _aboutService = aboutService;
    }

    public async Task<IViewComponentResult> InvokeAsync()
    {
        var value = await _aboutService.GetAllAboutAsync();
        return View(value.FirstOrDefault());
    }
}