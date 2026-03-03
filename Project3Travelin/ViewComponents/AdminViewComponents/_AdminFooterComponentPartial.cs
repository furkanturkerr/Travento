using Microsoft.AspNetCore.Mvc;

namespace Project3Travelin.ViewComponents.AdminViewComponents;

public class _AdminFooterComponentPartial : ViewComponent
{
    public IViewComponentResult Invoke() => View();
}