using Microsoft.AspNetCore.Mvc;

namespace Project3Travelin.Controllers;

public class DestinationController : Controller
{
    // GET
    public IActionResult Index()
    {
        return View();
    }
}