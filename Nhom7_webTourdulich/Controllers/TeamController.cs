    using System.Diagnostics;
    using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Nhom7_webTourdulich.Models;

    namespace Nhom7_webTourdulich.Controllers;

    public class TeamController : Controller
    {
        private readonly QuanLyTourContext _quanLyTour;
        private readonly ILogger<TeamController> _logger;
        private readonly IWebHostEnvironment _webHostEnvironment;

        public TeamController(ILogger<TeamController> logger, QuanLyTourContext quanLyTour, IWebHostEnvironment webHostEnvironment)
        {
            _logger = logger;
            _quanLyTour = quanLyTour;
            _webHostEnvironment = webHostEnvironment;
        }

public async Task<IActionResult> Index()
{
    // Lấy danh sách nhân viên kèm chức vụ
    var nhanViens = await _quanLyTour.NhanViens
                        .Include(nv => nv.MaChucVuNavigation) // Bao gồm dữ liệu từ ChucVu
                        .ToListAsync();

    // Lấy danh sách chức vụ cho ViewBag
    ViewBag.ChucVus = await _quanLyTour.ChucVus.ToListAsync();

    return View(nhanViens);
}





        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(NhanVien model, IFormFile? file)
        {
            if (ModelState.IsValid)
            {
                if (file != null && file.Length > 0)
                {
                    // Tạo thư mục nếu chưa tồn tại
                    string uploadFolder = Path.Combine(_webHostEnvironment.WebRootPath, "img");
                    if (!Directory.Exists(uploadFolder))
                    {
                        Directory.CreateDirectory(uploadFolder);
                    }

                    // Đặt tên file duy nhất
                    string uniqueFileName = Guid.NewGuid().ToString() + "_" + file.FileName;
                    string filePath = Path.Combine(uploadFolder, uniqueFileName);

                    // Lưu file vào thư mục
                    using (var fileStream = new FileStream(filePath, FileMode.Create))
                    {
                        await file.CopyToAsync(fileStream);
                    }

                    // Lưu đường dẫn file vào database
                    model.HinhAnh = "/img/" + uniqueFileName;
                }

                // Thêm vào database
                _quanLyTour.NhanViens.Add(model);
                await _quanLyTour.SaveChangesAsync();

                return RedirectToAction("Index");
            }

            return View(model);
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
