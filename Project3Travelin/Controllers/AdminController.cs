using Microsoft.AspNetCore.Mvc;

namespace Project3Travelin.Controllers;

public class AdminController : Controller
{
    // GET
    public IActionResult Index()
    {
        return View();
    }
}