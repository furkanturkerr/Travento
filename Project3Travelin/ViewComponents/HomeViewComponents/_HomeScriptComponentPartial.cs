using Microsoft.AspNetCore.Mvc;

namespace Project3Travelin.ViewComponents.HomeViewComponents;

public class _HomeScriptComponentPartial : ViewComponent
{
    public IViewComponentResult Invoke() => View();
}