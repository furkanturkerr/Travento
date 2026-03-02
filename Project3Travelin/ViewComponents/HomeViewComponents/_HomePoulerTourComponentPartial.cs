using Microsoft.AspNetCore.Mvc;

namespace Project3Travelin.ViewComponents.HomeViewComponents;

public class _HomePoulerTourComponentPartial : ViewComponent
{
    public IViewComponentResult Invoke() => View();
}