using Microsoft.AspNetCore.Mvc;

namespace Project3Travelin.ViewComponents.TourViewComponents;

public class _DefaultFooterComponentPartial : ViewComponent
{
    public IViewComponentResult Invoke() => View();
}