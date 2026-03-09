using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Project3Travelin.Models;
using Project3Travelin.Services.TourServices;

namespace Project3Travelin.Controllers;

public class HomeController : Controller
{
    private readonly ITourService _tourService;

    public HomeController(ITourService tourService)
    {
        _tourService = tourService;
    }

    public async Task<IActionResult> Anasayfa()
    {
        var cities = await _tourService.GetAllCitiesAsync();
        ViewBag.Cities = cities;
        return View();
    }
}