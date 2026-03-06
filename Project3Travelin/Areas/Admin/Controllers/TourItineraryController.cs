using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Project3Travelin.Dtos.TourTineraryDtos;
using Project3Travelin.Services.TourServices;
using Project3Travelin.Services.TourTineraryService;

[Area("Admin")]
public class TourItineraryController : Controller
{
    private readonly ITineraryService _TourItineraryService;
    private readonly ITourService _tourService;
    private readonly IMapper _mapper;

    public TourItineraryController(ITineraryService TourItineraryService, ITourService tourService, IMapper mapper)
    {
        _TourItineraryService = TourItineraryService;
        _tourService = tourService;
        _mapper = mapper;
    }

    public async Task<IActionResult> Index(string tourId = null)
    {
        var tours = await _tourService.GetAllTourAsync();
        var allItineraries = new List<ResultTineraryListByTourIdDto>();

        foreach (var tour in tours)
        {
            var itinerary = await _TourItineraryService.GetTineraryByTourIdAsync(tour.TourId);
            if (itinerary != null && itinerary.Any())
                allItineraries.AddRange(itinerary);
        }

        if (!string.IsNullOrEmpty(tourId))
            allItineraries = allItineraries.Where(x => x.TourId == tourId).ToList();

        ViewBag.Tours = tours;
        return View(allItineraries);
    }

    [HttpGet]
    public async Task<IActionResult> CreateTourItinerary()
    {
        var tours = await _tourService.GetAllTourAsync();
        ViewBag.Tours = tours;
        return View();
    }

    [HttpPost]
    public async Task<IActionResult> CreateTourItinerary(CreateTineraryDto createTineraryDto)
    {
        await _TourItineraryService.CreateTineraryAsync(createTineraryDto);
        return RedirectToAction("Index"); 
    }

    [HttpGet]
    public async Task<IActionResult> EditTourItinerary(string id)
    {
        var values = await _TourItineraryService.GetTineraryByIdAsync(id);
        var updateDto = _mapper.Map<UpdateTineraryDto>(values);
        var tours = await _tourService.GetAllTourAsync();
        ViewBag.Tours = tours;
        return View(updateDto);
    }

    [HttpPost]
    public async Task<IActionResult> EditTourItinerary(UpdateTineraryDto updateTourItineraryDto)
    {
        await _TourItineraryService.UpdateTineraryAsync(updateTourItineraryDto);
        return RedirectToAction("Index");
    }

    public async Task<IActionResult> DeleteTourItinerary(string id)
    {
        await _TourItineraryService.DeleteTineraryAsync(id);
        return RedirectToAction("Index");
    }
}