using Microsoft.AspNetCore.Mvc;
using Project3Travelin.Dtos.TourDtos;
using Project3Travelin.Services.TourServices;
using Project3Travelin.Services.TourTineraryService;

namespace Project3Travelin.Controllers;

public class TourController : Controller
{
    private readonly ITourService _tourService;
    private readonly ITineraryService _itineraryService;

    public TourController(ITourService tourService, ITineraryService itineraryService)
    {
        _tourService = tourService;
        _itineraryService = itineraryService;
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

    public async Task<IActionResult> TourDetail(string tourId)
    {
        var value = await _tourService.GetTourByIdAsync(tourId);
        var itinerary = await _itineraryService.GetTineraryByTourIdAsync(tourId);
        ViewBag.Itinerary = itinerary;
        return View(value);
    }
}