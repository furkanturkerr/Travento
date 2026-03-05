using Microsoft.AspNetCore.Mvc;
using Project3Travelin.Services.CommantServices;

namespace Project3Travelin.ViewComponents.TourViewComponents.TourDetailViewComponents;

public class _TourDetailCommentListComponentPartial : ViewComponent
{
    private readonly ICommentService _commentService;

    public _TourDetailCommentListComponentPartial(ICommentService commentService)
    {
        _commentService = commentService;
    }

    public async Task<IViewComponentResult> InvokeAsync(string tourId)
    {
        var value = await _commentService.GetCommentByTourIdAsync(tourId);
        return View(value);
    }
}