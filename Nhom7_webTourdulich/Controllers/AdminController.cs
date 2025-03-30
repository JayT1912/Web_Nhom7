using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Nhom7_webTourdulich.Models;
using Nhom7_webTourdulich.Repositories;
using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;

namespace Nhom7_webTourdulich.Controllers
{
    [Authorize(Roles = "Admin")]  // Đảm bảo chỉ cho phép truy cập nếu người dùng có role là "Admin"
    public class AdminController : Controller
    {
        private readonly IUserRepository _userRepository;

        public AdminController(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        // Hiển thị trang Admin (hiển thị tất cả người dùng)
        public async Task<IActionResult> Index()
        {
            var users = await _userRepository.GetAllAsync();  // Lấy tất cả người dùng từ cơ sở dữ liệu
            return View(users);  // Trả về view với danh sách người dùng
        }

[HttpPost]
[ValidateAntiForgeryToken]
public async Task<IActionResult> Login(string username, string password)
{
    var user = await _userRepository.GetByUsernameAndPasswordAsync(username, password);

    if (user != null)
    {
        // Tạo danh sách các claim cho người dùng
        var claims = new List<Claim>
        {
            new Claim(ClaimTypes.Name, user.Username),   // Gán tên người dùng
            new Claim(ClaimTypes.Email, user.Email),     // Gán email người dùng (nếu có)
            new Claim(ClaimTypes.Role, user.Role ?? "User")  // Gán vai trò (role) của người dùng (nếu không có thì gán "User")
        };

        var claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);

        var authProperties = new AuthenticationProperties
        {
            IsPersistent = true  // Đảm bảo cookie tồn tại lâu dài
        };

        // Đăng nhập người dùng và lưu thông tin trong cookie
        await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, new ClaimsPrincipal(claimsIdentity), authProperties);

        // Chuyển hướng người dùng dựa trên quyền của họ
        if (user.Role == "Admin")
        {
            return RedirectToAction("Index", "Admin");  // Chuyển tới trang Admin nếu là Admin
        }


        if (user.Role == "Manage")
        {
            return RedirectToAction("Index", "Admin");  // Chuyển tới trang Admin nếu là Admin
        }

        else
        {
            return RedirectToAction("Index", "Home");  // Chuyển tới trang Home nếu là User
        }
    }
    else
    {
        // Nếu không tìm thấy người dùng hoặc mật khẩu sai
        ModelState.AddModelError("", "Tên đăng nhập hoặc mật khẩu không đúng.");
    }

    return View();
}


        // Phương thức xử lý đăng xuất
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Logout()
        {
            // Đăng xuất và xóa cookie
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return RedirectToAction("Login", "Admin");
        }
    }
}
