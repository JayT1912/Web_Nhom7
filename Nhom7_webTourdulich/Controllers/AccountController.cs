using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Nhom7_webTourdulich.Repositories;
using Microsoft.AspNetCore.Authentication.Cookies;  // Cần thiết cho CookieAuthenticationDefaults
using Microsoft.AspNetCore.Authentication;  // Cần thiết cho SignOutAsync
using System.Security.Claims;  // Cần thiết cho ClaimTypes

namespace Nhom7_webTourdulich.Controllers
{
    public class AccountController : Controller
    {
        private readonly IUserRepository _userRepository;

        public AccountController(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        // Phương thức hiển thị trang "Access Denied"
        public IActionResult AccessDenied()
        {
            return View();
        }

        // Phương thức GET cho trang đăng nhập
        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }

        // Phương thức POST xử lý đăng nhập
        [HttpPost]
        public async Task<IActionResult> Login(string email, string password)
        {
            var user = await _userRepository.GetByEmailAndPasswordAsync(email, password);

            if (user != null)
            {
                // Tạo danh sách các claim cho người dùng
                var claims = new List<Claim>
                {
                    new Claim(ClaimTypes.Name, user.Username),  // Đảm bảo sử dụng ClaimTypes.Name thay cho ClaimTypes.FullName
                    new Claim(ClaimTypes.Email, user.Email),
                    new Claim(ClaimTypes.Role, user.Role)
                };

                // Tạo ClaimsIdentity và gán authentication scheme là CookieAuthenticationDefaults.AuthenticationScheme
                var claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
                
                // Tạo các thuộc tính cho authentication
                var authProperties = new AuthenticationProperties
                {
                    IsPersistent = true  // Đảm bảo cookie tồn tại lâu dài
                };

                // Đăng nhập người dùng và lưu thông tin trong cookie
                await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, new ClaimsPrincipal(claimsIdentity), authProperties);

                // Kiểm tra vai trò của người dùng và chuyển hướng phù hợp
                if (user.Role == "Admin" || user.Role == "Manage")
                {
                    return RedirectToAction("Index", "Home");
                }
                else
                {
                    return RedirectToAction("Index", "Product");
                }
            }

            // Nếu không tìm thấy người dùng, thêm lỗi vào ModelState và trả về view đăng nhập
            ModelState.AddModelError(string.Empty, "Invalid login attempt.");
            return View();
        }

        // Phương thức xử lý đăng xuất
        [HttpPost]
        public async Task<IActionResult> Logout()
        {
            // Đăng xuất và xóa cookie
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return RedirectToAction("Login", "Account");
        }
    }
}
