using Microsoft.AspNetCore.Mvc;
using Nhom7_webTourdulich.Models;
using Nhom7_webTourdulich.Repositories;
using Microsoft.AspNetCore.Hosting;
using System.IO;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace Nhom7_webTourdulich.Controllers
{
    [Authorize]
    public class TourController : Controller
    {
        private readonly ITourRepository _tourRepository;
        private readonly IWebHostEnvironment _webHostEnvironment;

        // Tiêm ITourRepository và IWebHostEnvironment vào constructor
        public TourController(ITourRepository tourRepository, IWebHostEnvironment webHostEnvironment)
        {
            _tourRepository = tourRepository;
            _webHostEnvironment = webHostEnvironment;
        }

        // Phương thức GET cho Index (hiển thị danh sách các tour)
        [HttpGet]
        public async Task<IActionResult> Index()
        {
            // Lấy tất cả các tour cùng thông tin chi tiết
            var tours = await _tourRepository.GetAllToursWithDetails();
            return View(tours); // Trả về view với danh sách các tour
        }

        // Phương thức GET cho Create (hiển thị form tạo mới tour)
        [HttpGet]
        public async Task<IActionResult> Create()
        {
            // Lấy dữ liệu từ các bảng LoaiTour, GiaTour và DiemDen thông qua repository
            var loaiTours = await _tourRepository.GetLoaiTours();   // Gọi repository để lấy LoaiTour
            var giaTours = await _tourRepository.GetGiaTours();     // Gọi repository để lấy GiaTour
            var diemDens = await _tourRepository.GetDiemDens();     // Gọi repository để lấy DiemDen

            // Gán dữ liệu vào ViewBag để sử dụng trong view
            ViewBag.MaLoaiTour = new SelectList(loaiTours, "MaLoaiTour", "TenLoaiTour");
            ViewBag.MaGiaTour = new SelectList(giaTours, "MaGiaTour", "Gia");
            ViewBag.MaDiemDen = new SelectList(diemDens, "MaDiemDen", "Ten");

            return View();
        }
[HttpPost]
[ValidateAntiForgeryToken]
public async Task<IActionResult> Create(Tour model, IFormFile avatar)
{
    if (ModelState.IsValid)
    {
        // Check if an image was uploaded
        if (avatar != null && avatar.Length > 0)
        {
            // Get the file name and path
            var fileName = Path.GetFileName(avatar.FileName);
            var filePath = Path.Combine(_webHostEnvironment.WebRootPath, "images", fileName);

            // Save the image to the images folder
            using (var fileStream = new FileStream(filePath, FileMode.Create))
            {
                await avatar.CopyToAsync(fileStream);
            }

            // Assign the image URL to the model
            model.ImageUrl = "/images/" + fileName;
        }

        // Save the tour model to the database
        await _tourRepository.AddAsync(model);

        // Redirect to the Index page after the creation
        return RedirectToAction("Index");
    }

    // Return the model if validation fails
    return View(model);
}

// Phương thức GET cho Edit (hiển thị form chỉnh sửa tour)
[HttpGet]
public async Task<IActionResult> Edit(int? id)
{
    if (id == null)
    {
        return NotFound();
    }

    // Get the existing tour based on the ID
    var tour = await _tourRepository.GetByIdAsync(id.Value);
    if (tour == null)
    {
        return NotFound();
    }

    // Get LoaiTours, GiaTours, DiemDens for dropdown lists
    var loaiTours = await _tourRepository.GetLoaiTours();
    var giaTours = await _tourRepository.GetGiaTours();
    var diemDens = await _tourRepository.GetDiemDens();

    // Populate the ViewBag with the necessary data for the form
    ViewBag.MaLoaiTour = new SelectList(loaiTours, "MaLoaiTour", "TenLoaiTour", tour.MaLoaiTour);
    ViewBag.MaGiaTour = new SelectList(giaTours, "MaGiaTour", "Gia", tour.MaGiaTour);
    ViewBag.MaDiemDen = new SelectList(diemDens, "MaDiemDen", "Ten", tour.MaDiemDen);

    return View(tour);
}

[HttpPost]
[ValidateAntiForgeryToken]
public async Task<IActionResult> Edit(int id, Tour model, IFormFile avatar)
{
    if (id != model.MaTour)
    {
        return NotFound();
    }

    if (ModelState.IsValid)
    {
        try
        {
            var existingTour = await _tourRepository.GetByIdAsync(id);
            if (existingTour == null)
            {
                return NotFound();
            }

            // Check if the avatar image is uploaded and handle it
            if (avatar != null && avatar.Length > 0)
            {
                var fileName = Path.GetFileName(avatar.FileName);
                var filePath = Path.Combine(_webHostEnvironment.WebRootPath, "images", fileName);
                using (var fileStream = new FileStream(filePath, FileMode.Create))
                {
                    await avatar.CopyToAsync(fileStream);
                }
                existingTour.ImageUrl = "/images/" + fileName;
            }

            // Update the tour's details
            existingTour.Ten = model.Ten;
            existingTour.MaLoaiTour = model.MaLoaiTour;
            existingTour.MaGiaTour = model.MaGiaTour;
            existingTour.MaDiemDen = model.MaDiemDen;
            existingTour.SoNgay = model.SoNgay;
            existingTour.SoLuongNguoi = model.SoLuongNguoi;
            existingTour.MoTa = model.MoTa;

            // Save the updated tour
            await _tourRepository.UpdateAsync(existingTour);
        }
        catch (DbUpdateConcurrencyException)
        {
            if (!await _tourRepository.ExistsAsync(model.MaTour))
            {
                return NotFound();
            }
            else
            {
                throw;
            }
        }
        return RedirectToAction(nameof(Index));
    }

    return View(model);
}


        // GET: Tour/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var tour = await _tourRepository.GetByIdAsync(id.Value);
            if (tour == null)
            {
                return NotFound();
            }

            return View(tour);
        }

        // POST: Tour/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            await _tourRepository.DeleteAsync(id);
            return RedirectToAction(nameof(Index));
        }

        private bool TourExists(int id)
        {
            return _tourRepository.ExistsAsync(id).Result;  // Kiểm tra xem tour có tồn tại không
        }
    }
}
