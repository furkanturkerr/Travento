using Microsoft.AspNetCore.Mvc;

namespace Project3Travelin.ViewComponents.ContactViewComponent;

public class _ContactScriptComponentPartial : ViewComponent
{
    public IViewComponentResult Invoke() => View();
}