using Microsoft.AspNetCore.Mvc;
using Project3Travelin.Dtos.BookingDtos;
using Project3Travelin.Services.BookingServices;
using Project3Travelin.Services.TourServices;

namespace Project3Travelin.Controllers;

public class BookingController : Controller
{
    private readonly ITourService _tourService;
    private readonly IBookingService _bookingService;

    public BookingController(ITourService tourService, IBookingService bookingService)
    {
        _tourService = tourService;
        _bookingService = bookingService;
    }

    public async Task<IActionResult> Rezervasyon()
    {
        ViewBag.Tours = await _tourService.GetAllTourAsync();
        return View();
    }
    
    [HttpPost]
    public async Task<IActionResult> CreateBooking(CreateBookingDto dto)
    {
        dto.CreatedAt   = DateTime.Now;
        dto.IsConfirmed = false;

        await _bookingService.CreateBookingAsync(dto);

        var tour = await _tourService.GetTourByIdAsync(dto.TourId);

        TempData["FirstName"]   = dto.FirstName;
        TempData["LastName"]    = dto.LastName;
        TempData["Email"]       = dto.Email;
        TempData["TourTitle"]   = tour?.Title;
        TempData["BookingId"]   = dto.BookingId;
        TempData["BookingDate"] = dto.BookingDate.ToString("yyyy-MM-ddTHH:mm:ss");
        TempData["PersonCount"] = dto.PersonCount.ToString();

        return RedirectToAction("BookingSuccess");
    }

    public IActionResult BookingSuccess()
    {
        ViewBag.FirstName   = TempData["FirstName"]   as string;
        ViewBag.LastName    = TempData["LastName"]    as string;
        ViewBag.Email       = TempData["Email"]       as string;
        ViewBag.TourTitle   = TempData["TourTitle"]   as string;
        ViewBag.BookingId   = TempData["BookingId"]   as string;
        ViewBag.PersonCount = TempData["PersonCount"] as string;

        var dateStr = TempData["BookingDate"] as string;
        if (!string.IsNullOrEmpty(dateStr))
            ViewBag.BookingDate = DateTime.Parse(dateStr);

        return View();
    }
}