using Microsoft.AspNetCore.Mvc;
using Project3Travelin.Dtos.TourDtos;
using Project3Travelin.Services.TourServices;

namespace Project3Travelin.Controllers;

public class TourController : Controller
{
    private readonly ITourService _tourService;

    public TourController(ITourService tourService)
    {
        _tourService = tourService;
    }

    // GET
    public IActionResult CreateTour()
    {
        return View();
    }
    
    [HttpPost]
    public async Task<IActionResult> CreateTour(CreateTourDto createTourDto)
    {
        await _tourService.CreateTourAsync(createTourDto);
        return RedirectToAction("TourList");
    }
    
    [HttpGet]
    public async Task<IActionResult> TourList(string city, decimal? minPrice, decimal? maxPrice, int page = 1)
    {
        var values = await _tourService.GetFilteredToursAsync(city, minPrice, maxPrice);

        // Sayfalama
        int pageSize = 8;
        int totalCount = values.Count;
        int totalPages = (int)Math.Ceiling(totalCount / (double)pageSize);

        // Sadece o sayfanın turlarını al
        var pagedValues = values
            .Skip((page - 1) * pageSize)
            .Take(pageSize)
            .ToList();

        // Dropdown için şehirler
        var allTours = await _tourService.GetAllTourAsync();
        var cities = allTours.Select(x => x.City).Distinct().OrderBy(x => x).ToList();

        ViewBag.Cities = cities;
        ViewBag.City = city;
        ViewBag.MinPrice = minPrice;
        ViewBag.MaxPrice = maxPrice;
        ViewBag.CurrentPage = page;
        ViewBag.TotalPages = totalPages;
        ViewBag.TotalCount = totalCount;

        return View(pagedValues);
    }
}