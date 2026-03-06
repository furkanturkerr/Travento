using Microsoft.AspNetCore.Mvc;
using Project3Travelin.Services.SliderServices;

namespace Project3Travelin.ViewComponents.HomeViewComponents;

public class _HomeSliderComponentPartial : ViewComponent
{
    private readonly ISliderService _sliderService;

    public _HomeSliderComponentPartial(ISliderService sliderService)
    {
        _sliderService = sliderService;
    }

    public async Task<IViewComponentResult > InvokeAsync()
    {
        var values = await _sliderService.GetAllSlidersAsync();
        return View(values.FirstOrDefault());
    }
}