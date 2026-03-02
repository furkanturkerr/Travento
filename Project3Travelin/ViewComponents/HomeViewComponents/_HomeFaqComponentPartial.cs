using Microsoft.AspNetCore.Mvc;

namespace Project3Travelin.ViewComponents.HomeViewComponents;

public class _HomeFaqComponentPartial : ViewComponent
{
    public IViewComponentResult Invoke() => View();
}