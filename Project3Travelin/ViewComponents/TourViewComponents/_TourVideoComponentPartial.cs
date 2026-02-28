using Microsoft.AspNetCore.Mvc;

namespace Project3Travelin.ViewComponents.TourViewComponents;

public class _TourVideoComponentPartial : ViewComponent
{
    public IViewComponentResult Invoke() => View();
}