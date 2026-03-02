using Microsoft.AspNetCore.Mvc;

namespace Project3Travelin.ViewComponents.HomeViewComponents;

public class _HomeWhyChooseComponentPartial : ViewComponent
{
    public IViewComponentResult Invoke() => View();
}