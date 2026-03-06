using Microsoft.AspNetCore.Mvc;

namespace Project3Travelin.ViewComponents.ContactViewComponent;

public class _ContactInfoComponentPartial : ViewComponent
{
    public IViewComponentResult Invoke() => View();
}