using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Nhom7_webTourdulich.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using System.Security.Claims;
using System.Threading.Tasks;

namespace Nhom7_webTourdulich.Controllers
{
    public class LoginController : Controller
    {
        private readonly QuanLyTourContext _quanLyTour;
        private readonly ILogger<LoginController> _logger;

        public LoginController(ILogger<LoginController> logger, QuanLyTourContext quanLyTour)
        {
            _logger = logger;
            _quanLyTour = quanLyTour;
        }

        // Phương thức GET cho trang đăng nhập
        public IActionResult Index()
        {
            return View();
        }
[HttpPost]
[ValidateAntiForgeryToken]
public async Task<IActionResult> Index(Login login)
{
    if (ModelState.IsValid)
    {
        // Tìm người dùng từ cơ sở dữ liệu
        var dbLogin = await _quanLyTour.Logins
            .FirstOrDefaultAsync(l => l.Username == login.Username);

        if (dbLogin != null && dbLogin.Password == login.Password)
        {
            HttpContext.Session.SetString("Username", dbLogin.Username);

            // Gán vai trò dựa trên tài khoản đăng nhập
            var role = dbLogin.Username.ToLower() switch
            {
                "admin" => "Admin",         // Nếu tên đăng nhập là admin, gán vai trò Admin
                _ => "User"                 // Mặc định là User nếu không phải Admin hoặc Manager
            };

            // Tạo danh sách Claims
            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.Name, dbLogin.Username),
                new Claim(ClaimTypes.Role, role) // Gán vai trò cho người dùng
            };

            // Tạo ClaimsIdentity và thêm vào context
            var claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
            var authProperties = new AuthenticationProperties
            {
                IsPersistent = true  // Giữ người dùng đăng nhập
            };

            // Đăng nhập người dùng và lưu thông tin trong cookie
            await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, new ClaimsPrincipal(claimsIdentity), authProperties);

            // Chuyển hướng theo quyền
            if (role == "Admin")
            {
                // Nếu là Admin, chuyển tới trang Admin
                return RedirectToAction("Index", "Admin");
            }
            else
            {
                // Nếu là người dùng bình thường, chuyển tới trang Home
                return RedirectToAction("Index", "Home");
            }
        }
        else
        {
            // Nếu không tìm thấy tài khoản hoặc mật khẩu sai
            ModelState.AddModelError("", "Tên đăng nhập hoặc mật khẩu không đúng.");
        }
    }

    // Nếu có lỗi, hiển thị lại trang đăng nhập
    return View(login);
}


        // Phương thức Logout để đăng xuất người dùng
        public async Task<IActionResult> Logout()
        {
            // Đăng xuất người dùng
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);

            // Xóa thông tin người dùng khỏi Session
            HttpContext.Session.Clear();

            // Chuyển hướng về trang chủ
            return RedirectToAction("Index", "Home");
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
}
