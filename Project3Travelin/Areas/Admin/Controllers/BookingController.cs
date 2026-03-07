using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Project3Travelin.Dtos.BookingDtos;
using Project3Travelin.Services.BookingServices;
using Project3Travelin.Services.TourServices;

namespace Project3Travelin.Areas.Admin.Controllers;
[Area("Admin")]
public class BookingController : Controller
{
    private readonly IBookingService _bookingService;
    private readonly ITourService _tourService;
    private readonly IMapper _mapper;

    public BookingController(IBookingService bookingService, IMapper mapper, ITourService tourService)
    {
        _bookingService = bookingService;
        _mapper = mapper;
        _tourService = tourService;
    }

    // GET
    public async Task<IActionResult> Index()
    {
        var values = await _bookingService.GetAllBookingAsync();
        var valuestour = await _tourService.GetAllTourAsync();
        return View(values);
    }
    
    [HttpGet]
    public async Task<IActionResult> Details(string id)
    {
        var value = await _bookingService.GetBookingByIdAsync(id);
        var details = _mapper.Map<UpdateBookingDto>(value);
        return View(details);
    }
    
    [HttpPost]
    public async Task<IActionResult> Details(UpdateBookingDto updateBookingDto)
    {
        await _bookingService.UpdateBookingAsync(updateBookingDto);
        return RedirectToAction("Index");
    }

    public async Task<IActionResult> Delete(string id)
    {
        await _bookingService.DeleteBookingAsync(id);
        return RedirectToAction("Index");  
    }

    public async Task<IActionResult> Approve(string id)
    {
        await _bookingService.Approve(id);
        return RedirectToAction("Index"); 
    }
}