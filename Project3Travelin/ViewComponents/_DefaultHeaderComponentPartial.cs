using Microsoft.AspNetCore.Mvc;

namespace Project3Travelin.ViewComponents;

public class _DefaultHeaderComponentPartial : ViewComponent
{
    public IViewComponentResult Invoke() => View();
    
}