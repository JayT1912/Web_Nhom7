using Nhom7_webTourdulich.Models;
using Nhom7_webTourdulich.Repositories;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;

namespace Nhom7_webTourdulich.Controllers
{
    public class AccountController : Controller
    {
        private readonly IUserRepository _userRepository;

        // Constructor để lấy IUserRepository từ DI container
        public AccountController(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        // Phương thức GET để hiển thị form đăng nhập
        public IActionResult Login()
        {
            return View();
        }

        // Phương thức POST để xử lý đăng nhập
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(string username, string password)
        {
            // Kiểm tra thông tin đăng nhập
            var user = await _userRepository.GetByUsernameAndPasswordAsync(username, password);

            if (user != null)
            {
                // Tạo danh sách các claim cho người dùng
                var claims = new List<Claim>
                {
                    new Claim(ClaimTypes.Name, user.Username),   // Gán tên người dùng
                    new Claim(ClaimTypes.Email, user.Email),     // Gán email người dùng
                    new Claim(ClaimTypes.Role, user.Role ?? "User")  // Gán vai trò (role) của người dùng
                };

                var claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);

                var authProperties = new AuthenticationProperties
                {
                    IsPersistent = true  // Đảm bảo cookie tồn tại lâu dài
                };

                // Đăng nhập người dùng và lưu thông tin trong cookie
                await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, new ClaimsPrincipal(claimsIdentity), authProperties);

                // Kiểm tra vai trò và chuyển hướng đến trang phù hợp
                if (user.Role == "Admin")
                {
                    return RedirectToAction("Index", "Admin");  // Chuyển tới trang Admin nếu là Admin
                }
                else if (user.Role == "Manager")
                {
                    return RedirectToAction("Index", "Manager");  // Chuyển tới trang Manager nếu là Manager
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
            return RedirectToAction("Login", "Account");
        }
    }
}
