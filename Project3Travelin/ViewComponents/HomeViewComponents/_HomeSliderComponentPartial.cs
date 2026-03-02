using Microsoft.AspNetCore.Mvc;

namespace Project3Travelin.ViewComponents.HomeViewComponents;

public class _HomeSliderComponentPartial : ViewComponent
{
    public IViewComponentResult Invoke() => View();
}