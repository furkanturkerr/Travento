using Microsoft.AspNetCore.Mvc;

namespace Project3Travelin.Areas.Admin.Controllers;
[Area("Admin")]
public class SliderController : Controller
{
    // GET
    public IActionResult Index()
    {
        return View();
    }
}