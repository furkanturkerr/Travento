using Microsoft.AspNetCore.Mvc;

namespace Project3Travelin.ViewComponents.HomeViewComponents;

public class _HomeBannerComponentPartial : ViewComponent
{
    public IViewComponentResult Invoke() => View();
}