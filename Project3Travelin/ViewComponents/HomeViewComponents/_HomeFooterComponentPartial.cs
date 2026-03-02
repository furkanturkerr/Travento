using Microsoft.AspNetCore.Mvc;

namespace Project3Travelin.ViewComponents.HomeViewComponents;

public class _HomeFooterComponentPartial : ViewComponent
{
    public IViewComponentResult Invoke() => View();
}