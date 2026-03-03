using Microsoft.AspNetCore.Mvc;

namespace Project3Travelin.ViewComponents.AdminViewComponents;

public class _AdminHeadComponetPartial : ViewComponent
{
    public IViewComponentResult Invoke() => View();
}