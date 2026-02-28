using Microsoft.AspNetCore.Mvc;

namespace Project3Travelin.ViewComponents.TourViewComponents;

public class _TourScriptComponentPartial : ViewComponent
{
    public IViewComponentResult Invoke() => View();
}