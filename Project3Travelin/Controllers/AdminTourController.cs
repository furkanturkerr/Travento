using Microsoft.AspNetCore.Mvc;

namespace Project3Travelin.Controllers;

public class AdminTourController : Controller
{
    // GET
    public IActionResult Index()
    {
        return View();
    }
}