using Microsoft.AspNetCore.Mvc;

namespace Project3Travelin.ViewComponents.AdminViewComponents;

public class _AdminScriptComponentPartial : ViewComponent
{
    public IViewComponentResult Invoke() => View();
}