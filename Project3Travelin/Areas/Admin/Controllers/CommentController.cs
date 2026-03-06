using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using MongoDB.Bson.Serialization.Attributes;
using Project3Travelin.Dtos.CommantDtos;
using Project3Travelin.Services.CommantServices;

namespace Project3Travelin.Areas.Admin.Controllers;
[Area("Admin")]
[BsonIgnoreExtraElements] 
public class CommentController : Controller
{
    private readonly ICommentService  _CommentService;
    private readonly IMapper _mapper;
    // GET
    public CommentController(ICommentService CommentService, IMapper mapper)
    {
        _CommentService = CommentService;
        _mapper = mapper;
    }

    public async Task<IActionResult> CommentList()
    {
        var values = await _CommentService.GetAllCommantsAsync();
        return View(values);
    }

    public async Task<IActionResult> CreateComment()
    {
        return View();
    }
    
    [HttpPost]
    public async Task<IActionResult> CreateComment(CreateCommentDto createCommentDto)
    {
        await _CommentService.CreateCommantAsync(createCommentDto);
        return RedirectToAction("CommentList");
    }
    
    public async Task<IActionResult> EditComment(string id)
    {
        var values = await _CommentService.GetCommentByIdAsync(id);
        var updateDto = _mapper.Map<UpdateCommentDto>(values);
        return View(updateDto);
    }
    
    [HttpPost]
    public async Task<IActionResult> EditComment(UpdateCommentDto updateCommentDto)
    {
        await _CommentService.UpdateCommantAsync(updateCommentDto);
        return RedirectToAction("CommentList");
    }

    public async Task<IActionResult> DeleteComment(string id)
    {
        await _CommentService.DeleteCommantAsync(id);
        return RedirectToAction("CommentList");   
    }

    public async Task<IActionResult> ApproveComment(string id)
    {
        await _CommentService.ApproveCommentAsync(id);
        return RedirectToAction("CommentList");  
    }
}