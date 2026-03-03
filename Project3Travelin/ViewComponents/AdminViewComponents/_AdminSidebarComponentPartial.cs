using Microsoft.AspNetCore.Mvc;

namespace Project3Travelin.ViewComponents.AdminViewComponents;

public class _AdminSidebarComponentPartial : ViewComponent
{
    public IViewComponentResult Invoke() => View();
}