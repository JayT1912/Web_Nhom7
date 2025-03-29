using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Nhom7_webTourdulich.Models;

namespace Nhom7_webTourdulich.Controllers;

public class AboutController : Controller
{
    private readonly QuanLyTourContext _quanLyTour;

    private readonly ILogger<AboutController> _logger;

    public AboutController(ILogger<AboutController> logger, QuanLyTourContext quanLyTour)
    {
        _logger = logger;
        _quanLyTour = quanLyTour;
    }

  
    public IActionResult Index()
    {
        return View();
    }
      public IActionResult cc()
    {
        return View();
    }
    public IActionResult Privacy()
    {
        return View();
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}
