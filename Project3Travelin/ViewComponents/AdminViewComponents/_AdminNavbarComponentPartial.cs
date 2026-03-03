using Microsoft.AspNetCore.Mvc;

namespace Project3Travelin.ViewComponents.AdminViewComponents;

public class _AdminNavbarComponentPartial : ViewComponent
{
    public IViewComponentResult Invoke() => View();
}