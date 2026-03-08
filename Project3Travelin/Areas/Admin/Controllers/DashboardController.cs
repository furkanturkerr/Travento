using Microsoft.AspNetCore.Mvc;
using Project3Travelin.Services.TourServices;
using Project3Travelin.Services.BookingServices;
using Project3Travelin.Services.CommantServices;

namespace Project3Travelin.Areas.Admin.Controllers;

[Area("Admin")]
public class DashboardController : Controller
{
    private readonly ITourService    _tourService;
    private readonly IBookingService _bookingService;
    private readonly ICommentService _commentService;

    public DashboardController(ITourService tourService, IBookingService bookingService, ICommentService commentService)
    {
        _tourService    = tourService;
        _bookingService = bookingService;
        _commentService = commentService;
    }

    public async Task<IActionResult> Index()
    {
        var tours    = await _tourService.GetAllTourAsync();
        var bookings = await _bookingService.GetAllBookingAsync();
        var comments = await _commentService.GetAllCommantsAsync();

        ViewBag.TotalTour      = tours.Count;
        ViewBag.TotalBooking   = bookings.Count;
        ViewBag.PendingBooking = bookings.Count(b => !b.IsConfirmed);

        ViewBag.RecentBookings = bookings.OrderByDescending(b => b.CreatedAt).Take(8).ToList();
        ViewBag.RecentComments = comments.OrderByDescending(c => c.CommentDate).Take(7).ToList();

        var currentYear = DateTime.Now.Year;
        ViewBag.MonthlyBookings = Enumerable.Range(1, 12)
            .Select(month => bookings.Count(b => b.CreatedAt.Year == currentYear && b.CreatedAt.Month == month))
            .ToList();

        return View();
    }
}