using Microsoft.AspNetCore.Mvc;

namespace Project3Travelin.ViewComponents.TourViewComponents;

public class _TourBreadcrumbComponentPartial : ViewComponent
{
    public IViewComponentResult Invoke() => View();
}