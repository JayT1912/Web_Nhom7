using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Nhom7_webTourdulich.Models;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Nhom7_webTourdulich.Controllers
{
    public class KhachHangController : Controller
    {
        private readonly QuanLyTourContext _context;

        public KhachHangController(QuanLyTourContext context)
        {
            _context = context;
        }

        // GET: KhachHang/Create
        public async Task<IActionResult> Create()
        {
            var tours = await _context.Tours.ToListAsync();

            // Debug log để kiểm tra dữ liệu
            System.Diagnostics.Debug.WriteLine("Tours count: " + tours.Count);
            foreach (var t in tours)
            {
                System.Diagnostics.Debug.WriteLine("Tour Name: " + t.Ten);
            }

            // Truyền danh sách tour vào ViewBag
            ViewBag.Tours = tours;

            return View(new KhachHang());
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(KhachHang khachHang)
        {
            if (ModelState.IsValid)
            {
                // Gán giá trị tệp ảnh nếu người dùng đã chọn
                if (Request.Form.Files.Count > 0)
                {
                    var file = Request.Form.Files[0];
                    if (file != null && file.Length > 0)
                    {
                        // Xử lý tệp ảnh (ví dụ: lưu tên tệp hoặc đường dẫn)
                        // Để đơn giản, giả sử lưu tên tệp
                        khachHang.HinhAnh = file.FileName;
                    }
                }

                _context.Add(khachHang);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Create));
            }

            // Nếu có lỗi, load lại danh sách tour
            ViewBag.Tours = await _context.Tours.ToListAsync();
            return View(khachHang);
        }
    }
}
