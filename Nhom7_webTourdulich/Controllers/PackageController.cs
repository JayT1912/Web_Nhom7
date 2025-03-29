using System;
using System.Diagnostics;
using System.Linq;
using Microsoft.AspNetCore.Http; // Dùng cho Session
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Nhom7_webTourdulich.Models;
using Nhom7_webTourdulich.ViewModels; // Nếu cần mapping sang ViewModel

namespace Nhom7_webTourdulich.Controllers
{
    public class PackageController : Controller
    {
        private readonly QuanLyTourContext _quanLyTour;
        private readonly ILogger<PackageController> _logger;

        // Constructor
        public PackageController(ILogger<PackageController> logger, QuanLyTourContext quanLyTour)
        {
            _logger = logger;
            _quanLyTour = quanLyTour;
        }
        // GET: Hiển thị tất cả các đánh giá
        public IActionResult AllReviews()
        {
            // Lấy danh sách toàn bộ đánh giá từ cơ sở dữ liệu
            var danhGias = _quanLyTour.DanhGias
                .Include(d => d.Tour) // Bao gồm thông tin Tour liên quan
                .ToList();

            return View(danhGias); // Truyền danh sách đánh giá xuống View
        }

        // Action Index: Hiển thị danh sách các tour
     public IActionResult Index(string query, int page = 1, int pageSize = 12)
{
    // Truy vấn dữ liệu từ cơ sở dữ liệu
    var tours = _quanLyTour.Tours
        .Include(t => t.TourImages)
        .Include(t => t.MaDiemDenNavigation)
        .Include(t => t.MaGiaTourNavigation)
        .Include(t => t.DanhGias)
        .AsQueryable();

    // Nếu có từ khóa tìm kiếm
    if (!string.IsNullOrEmpty(query))
    {
        tours = tours.Where(t => EF.Functions.Like(t.Ten, $"%{query}%") ||
                                 EF.Functions.Like(t.MaDiemDenNavigation.Ten, $"%{query}%"));
    }

    // Tổng số tour
    var totalTours = tours.Count();

    // Lấy dữ liệu của trang hiện tại
    var paginatedTours = tours
        .Skip((page - 1) * pageSize)
        .Take(pageSize)
        .ToList();

    // Tạo thông tin phân trang
    ViewBag.CurrentPage = page;
    ViewBag.TotalPages = (int)Math.Ceiling((double)totalTours / pageSize);
    ViewBag.Query = query;

    return View(paginatedTours);
}



        [HttpGet]
        public IActionResult Details(int id)
        {
            var tour = _quanLyTour.Tours
                .Include(t => t.TourImages)
                .Include(t => t.MaDiemDenNavigation)
                .Include(t => t.MaGiaTourNavigation)
                .Include(t => t.MaLoaiTourNavigation)
                .Include(t => t.DanhGias) // Bao gồm đánh giá liên quan
                .FirstOrDefault(t => t.MaTour == id);

            if (tour == null)
            {
                return NotFound(); // Trả về lỗi nếu không tìm thấy Tour
            }

            ViewBag.MoTaDaXuLy = tour.MoTa
                ?.Replace("\n", "<br>")
                ?.Replace("[IMPORTANT]", "<strong style='color: red;'>")
                ?.Replace("[/IMPORTANT]", "</strong>");

            return View(tour); // Truyền đối tượng Tour trực tiếp đến View
        }



        // GET: Hiển thị form đánh giá
        // GET: Hiển thị form đánh giá
        [HttpGet]
        public IActionResult Review(int id)
        {
            // Lấy Username từ session
            var username = HttpContext.Session.GetString("Username");
            if (string.IsNullOrEmpty(username))
            {
                // Nếu chưa đăng nhập, chuyển hướng về trang Login
                return RedirectToAction("Index", "Login");
            }

            // Lấy thông tin Tour theo MaTour
            var tour = _quanLyTour.Tours.FirstOrDefault(t => t.MaTour == id);
            if (tour == null)
            {
                return NotFound(); // Nếu không tìm thấy Tour, trả về lỗi 404
            }

            // Gán dữ liệu vào ViewBag
            ViewBag.MaTour = id;
            ViewBag.TourName = tour.Ten;
            ViewBag.Username = username;

            return View(); // Trả về form đánh giá
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Review(int MaTour, string NoiDung, int SoSao)
        {
            // Lấy Username từ session để đảm bảo không giả mạo
            var username = HttpContext.Session.GetString("Username");
            if (string.IsNullOrEmpty(username))
            {
                return RedirectToAction("Index", "Login"); // Nếu chưa đăng nhập, chuyển về Login
            }

            // Kiểm tra tính hợp lệ của Tour
            var tour = _quanLyTour.Tours.FirstOrDefault(t => t.MaTour == MaTour);
            if (tour == null)
            {
                return NotFound(); // Nếu không tìm thấy Tour, trả về lỗi 404
            }

            // Tạo đối tượng DanhGia và gán giá trị
            var review = new DanhGia
            {
                MaTour = MaTour,
                Tour = tour,
                NoiDung = NoiDung,
                SoSao = SoSao,
                Username = username,
                NgayDanhGia = DateTime.Now
            };

            // Lưu dữ liệu vào cơ sở dữ liệu
            try
            {
                _quanLyTour.DanhGias.Add(review);
                _quanLyTour.SaveChanges();

                // Hiển thị thông báo thành công
                TempData["SuccessMessage"] = "Đánh giá của bạn đã được lưu thành công!";
                return RedirectToAction("Details", "Package", new { id = MaTour });
            }
            catch (Exception ex)
            {
                // Log lỗi và trả về View với thông báo lỗi
                _logger.LogError(ex, "Lỗi khi lưu đánh giá.");
                ModelState.AddModelError("", "Đã xảy ra lỗi trong quá trình lưu đánh giá. Vui lòng thử lại.");
                return View();
            }
        }

        public IActionResult Privacy()
        {
            return View();
        }

        // Action Error: Xử lý lỗi hệ thống
        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
