using Microsoft.AspNetCore.Mvc;
using Project3Travelin.Dtos.CategoryDtos;
using Project3Travelin.Services.CategoryServices;

namespace Project3Travelin.Controllers;

public class CategoryController : Controller
{
    private readonly ICategoryService _categoryService;

    public CategoryController(ICategoryService categoryService)
    {
        _categoryService = categoryService;
    }

    // GET
    public IActionResult CreateCategory()
    {
        return View();
    }

    [HttpPost]
    public async Task<IActionResult> CreateCategory(CreateCategoryDto createCategoryDto)
    {
        createCategoryDto.IsStatus = true;
        await _categoryService.CreateCategoryAsync(createCategoryDto);
        return RedirectToAction("CategoryList");
    }
}