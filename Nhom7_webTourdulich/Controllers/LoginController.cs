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

        // Phương thức POST xử lý đăng nhập
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Index(Login login)
        {
            if (ModelState.IsValid)
            {
                // Kiểm tra tên đăng nhập có tồn tại không
                var dbLogin = await _quanLyTour.Logins
                    .FirstOrDefaultAsync(l => l.Username == login.Username);

                if (dbLogin != null && dbLogin.Password == login.Password)
                {
                    // Lưu thông tin người dùng vào Session
                    HttpContext.Session.SetString("Username", dbLogin.Username);

                    // Tạo Claims cho người dùng và thực hiện đăng nhập
                    var claims = new List<Claim>
                    {
                        new Claim(ClaimTypes.Name, dbLogin.Username),
                        new Claim(ClaimTypes.Role, "user") // Bạn có thể thay đổi vai trò theo yêu cầu
                    };

                    var claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
                    var authProperties = new AuthenticationProperties
                    {
                        IsPersistent = true
                    };

                    // Đăng nhập người dùng
                    await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, new ClaimsPrincipal(claimsIdentity), authProperties);

                    // Chuyển hướng tới trang chủ sau khi đăng nhập thành công
                    return RedirectToAction("Index", "Home");
                }
                else
                {
                    // Thêm lỗi nếu tên đăng nhập hoặc mật khẩu không đúng
                    ModelState.AddModelError("", "Tên đăng nhập hoặc mật khẩu không đúng.");
                }
            }

            // Nếu có lỗi, hiển thị lại form đăng nhập
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
