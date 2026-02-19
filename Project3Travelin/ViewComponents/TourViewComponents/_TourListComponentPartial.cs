using Microsoft.AspNetCore.Mvc;

namespace Project3Travelin.ViewComponents.TourViewComponents;

public class _TourListComponentPartial : ViewComponent
{
    public async Task<IViewComponentResult> InvokeAsync() => View();
}