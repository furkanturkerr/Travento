using Microsoft.AspNetCore.Mvc;
using Project3Travelin.Dtos.CommantDtos;
using Project3Travelin.Services.CommantServices;

namespace Project3Travelin.Controllers;

public class CommentController : Controller
{
    private readonly ICommentService _commentService;

    public CommentController(ICommentService commentService)
    {
        _commentService = commentService;
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> CreateComment(CreateCommentDto createCommentDto)
    {
        createCommentDto.CommentDate = DateTime.Now;
        createCommentDto.IsStatus = true;
        await _commentService.CreateCommantAsync(createCommentDto);
        
        return RedirectToAction("TourDetail", "Tour", new { tourId = createCommentDto.TourId });
    }

    public async Task<IActionResult> CommentListByTourId(string tourId)
    {
        var avg = await _commentService.GetAverageByTourIdAsync(tourId);
        ViewBag.ReviewAverage = avg;

        var comments = await _commentService.GetCommentByTourIdAsync(tourId);
        ViewBag.Comments = comments;

        return View(comments);
    }
}