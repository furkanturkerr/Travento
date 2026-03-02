using Microsoft.AspNetCore.Mvc;

namespace Project3Travelin.ViewComponents.HomeViewComponents;

public class _HomeAboutComponentPartial : ViewComponent
{
    public IViewComponentResult Invoke() => View();
}