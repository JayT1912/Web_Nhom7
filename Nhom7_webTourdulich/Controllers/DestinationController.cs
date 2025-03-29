using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;
using Nhom7_webTourdulich.Models;

namespace Nhom7_webTourdulich.Controllers
{
    public class DestinationController : Controller
    {
        private readonly QuanLyTourContext _quanLyTour;
        private readonly ILogger<DestinationController> _logger;

        public DestinationController(ILogger<DestinationController> logger, QuanLyTourContext quanLyTour)
        {
            _logger = logger;
            _quanLyTour = quanLyTour;
        }

        // Action: Display a list of destinations
        public IActionResult Index()
        {
            var destinations = _quanLyTour.DiemDens
                .OrderBy(d => d.MaDiemDen)
                .Take(4) // Adjust the number of destinations to display
                .Select(d => new
                {
                    d.Ten,
                    d.ThanhPho,
                    FirstTourImage = d.Tours
                        .SelectMany(t => t.TourImages)
                        .Select(ti => ti.HinhAnh)
                        .FirstOrDefault() ?? "/images/default-destination.jpg"
                })
                .ToList();

            return View(destinations);
        }
public IActionResult ToursByDestination(string destinationCity, int page = 1, int pageSize = 8)
{
    // Query tours based on the selected city
    var tours = _quanLyTour.Tours
        .Include(t => t.TourImages)
        .Include(t => t.MaDiemDenNavigation)
        .Include(t => t.MaGiaTourNavigation)
        .Where(t => EF.Functions.Like(t.MaDiemDenNavigation.ThanhPho, $"%{destinationCity}%"))
        .AsQueryable();

    // Pagination logic
    var totalTours = tours.Count();
    var paginatedTours = tours
        .Skip((page - 1) * pageSize)
        .Take(pageSize)
        .ToList();

    // Set data for the view
    ViewBag.CurrentPage = page;
    ViewBag.TotalPages = (int)Math.Ceiling((double)totalTours / pageSize);
    ViewBag.DestinationCity = destinationCity;

    return View("TourList", paginatedTours); // Use the TourList view
}


        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
