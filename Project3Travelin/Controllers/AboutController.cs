using Microsoft.AspNetCore.Mvc;

namespace Project3Travelin.Controllers;

public class AboutController : Controller
{
    // GET
    public IActionResult Hakkımızda()
    {
        return View();
    }
}