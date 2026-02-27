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

    // GET
    public IActionResult CreateComment()
    {
        return View();
    }

    [HttpPost]
    public async Task<IActionResult> CreateComment(CreateCommentDto createCommentDto)
    {
        await _commentService.CreateCommantAsync(createCommentDto);
        return RedirectToAction("CommentList");
    }

    public async Task<IActionResult> CommentListByTourId(string tourId)
    {
        var values = await _commentService.GetCommentByTourIdAsync(tourId);
        return View(values);
    }
}