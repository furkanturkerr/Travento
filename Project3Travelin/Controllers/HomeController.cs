using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Project3Travelin.Models;

namespace Project3Travelin.Controllers;

public class HomeController : Controller
{
    public IActionResult Anasayfa() => View();
}