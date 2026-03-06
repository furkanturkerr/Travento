using Microsoft.AspNetCore.Mvc;
using Project3Travelin.Services.FaqServices;

namespace Project3Travelin.ViewComponents.HomeViewComponents;

public class _HomeFaqComponentPartial : ViewComponent
{
    private readonly IFaqService _faqService;

    public _HomeFaqComponentPartial(IFaqService faqService)
    {
        _faqService = faqService;
    }

    public async Task<IViewComponentResult> InvokeAsync()
    {
        var value = await _faqService.GetAllFaqsAsync();
        return View(value);
    }
}