using Microsoft.AspNetCore.Mvc;
using Project3Travelin.Services.TourServices;
using Project3Travelin.Services.TourTineraryService;
using Project3Travelin.Services.CommantServices;

namespace Project3Travelin.Controllers;

public class TourController : Controller
{
    private readonly ITourService _tourService;
    private readonly ITineraryService _itineraryService;
    private readonly ICommentService _commentService;

    public TourController(ITourService tourService, ITineraryService itineraryService, ICommentService commentService)
    {
        _tourService = tourService;
        _itineraryService = itineraryService;
        _commentService = commentService;
    }

    [HttpGet]
    public async Task<IActionResult> TourList(string city, decimal? minPrice, decimal? maxPrice, int page = 1)
    {
        var values = await _tourService.GetFilteredToursAsync(city, minPrice, maxPrice);
        int pageSize = 8;
        int totalCount = values.Count;
        int totalPages = (int)Math.Ceiling(totalCount / (double)pageSize);
        var pagedValues = values.Skip((page - 1) * pageSize).Take(pageSize).ToList();
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

    public async Task<IActionResult> TourDetail(string tourId)
    {
        var value = await _tourService.GetTourByIdAsync(tourId);
        var itinerary = await _itineraryService.GetTineraryByTourIdAsync(tourId);
        var avg = await _commentService.GetAverageByTourIdAsync(tourId);
        ViewBag.Itinerary = itinerary;
        ViewBag.ReviewAverage = avg;
        return View(value);
    }
    
    [HttpGet]
    public async Task<IActionResult> Search(
        string? city,
        DateTime? date,
        decimal? minPrice,
        decimal? maxPrice,
        int page = 1)
    {
        var values = await _tourService.GetFilteredToursAsync(city, minPrice, maxPrice);

        int pageSize = 8;
        int totalCount = values.Count;
        int totalPages = (int)Math.Ceiling(totalCount / (double)pageSize);
        var pagedValues = values.Skip((page - 1) * pageSize).Take(pageSize).ToList();

        var cities = await _tourService.GetAllCitiesAsync();
        ViewBag.Cities = cities;

        ViewBag.SelectedCity = city;
        ViewBag.SelectedDate = date?.ToString("yyyy-MM-dd");
        ViewBag.MinPrice = minPrice;
        ViewBag.MaxPrice = maxPrice;
        ViewBag.CurrentPage = page;
        ViewBag.TotalPages = totalPages;
        ViewBag.TotalCount = totalCount;

        return View("TourList", pagedValues);
    }
}