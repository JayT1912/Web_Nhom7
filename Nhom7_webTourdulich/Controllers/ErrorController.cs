using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Nhom7_webTourdulich.Models;

namespace Nhom7_webTourdulich.Controllers;

public class ErrorController : Controller
{
    private readonly QuanLyTourContext _quanLyTour;
    private readonly ILogger<ErrorController> _logger;

    public ErrorController(ILogger<ErrorController> logger, QuanLyTourContext quanLyTour)
    {
        _logger = logger;
        _quanLyTour = quanLyTour;
    }
    public IActionResult Index()
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
