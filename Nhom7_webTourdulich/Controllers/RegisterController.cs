using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Nhom7_webTourdulich.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using System.Security.Claims;

namespace Nhom7_webTourdulich.Controllers
{
    public class RegisterController : Controller
    {
        private readonly QuanLyTourContext _quanLyTour; 
        private readonly ILogger<RegisterController> _logger;

        public RegisterController(ILogger<RegisterController> logger, QuanLyTourContext quanLyTour)
        {
            _logger = logger;
            _quanLyTour = quanLyTour;
        }

        // Hiển thị trang đăng ký
        public IActionResult Index()
        {
            return View(); 
        }

// Xử lý đăng ký người dùng
[HttpPost]
[ValidateAntiForgeryToken]
public async Task<IActionResult> Index(Register register)
{
    if (ModelState.IsValid)
    {
        // Kiểm tra xem tên đăng nhập đã tồn tại chưa
        var existingUser = await _quanLyTour.Users
            .FirstOrDefaultAsync(u => u.Username == register.Username);
        if (existingUser != null)
        {
            ModelState.AddModelError(string.Empty, "Tên đăng nhập đã tồn tại.");
            return View(register);
        }

        // Chuyển đổi nullable DateTime thành DateTime
        DateTime dateOfBirth = register.DateOfBirth.HasValue ? register.DateOfBirth.Value : default(DateTime); // Sử dụng default(DateTime) nếu là NULL

        // Tạo đối tượng người dùng mới để lưu vào bảng Users
        var newUser = new User
        {
            FullName = register.FullName,
            Username = register.Username,
            Password = register.Password,  // Gán mật khẩu vào User
            Email = register.Email,
            Address = string.IsNullOrEmpty(register.Address) ? null : register.Address, // Nếu không có địa chỉ, gán NULL
            PhoneNumber = register.PhoneNumber,
            DateOfBirth = dateOfBirth, // Gán giá trị DateOfBirth đã chuyển đổi
            Role = "user" // Gán quyền mặc định là "user"
        };

        // Thêm người dùng vào bảng Users
        _quanLyTour.Users.Add(newUser); 

        // Thêm thông tin đăng ký vào bảng Registers
        var newRegister = new Register
        {
            FullName = register.FullName,
            Username = register.Username,
            Password = register.Password,  // Gán mật khẩu vào Register
            RepeatPassword = register.RepeatPassword, // Gán giá trị RepeatPassword
            Email = register.Email,
            Address = string.IsNullOrEmpty(register.Address) ? null : register.Address, // Nếu không có địa chỉ, gán NULL
            PhoneNumber = register.PhoneNumber,
            DateOfBirth = dateOfBirth, // Gán giá trị DateOfBirth đã chuyển đổi
            Role = "user" // Gán quyền mặc định là "user"
        };

        // Thêm vào bảng Register
        _quanLyTour.Registers.Add(newRegister);  // Thêm vào bảng Register

        // Tạo đối tượng Login và lưu vào bảng Logins
        var newLogin = new Login
        {
            Username = register.Username,
            Password = register.Password,  // Mật khẩu của người dùng
            RememberMe = false // Mặc định RememberMe là false
        };

        // Thêm vào bảng Login
        _quanLyTour.Logins.Add(newLogin);

        // Lưu tất cả thay đổi vào cơ sở dữ liệu
        await _quanLyTour.SaveChangesAsync(); 

        // Tạo một ClaimsIdentity cho người dùng mới
        var claims = new List<Claim>
        {
            new Claim(ClaimTypes.Name, register.Username),
            new Claim(ClaimTypes.Email, register.Email),
            new Claim(ClaimTypes.Role, "user")
        };
        var claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
        var authProperties = new AuthenticationProperties
        {
            IsPersistent = true
        };

        // Đăng nhập người dùng sau khi đăng ký thành công
        await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, new ClaimsPrincipal(claimsIdentity), authProperties);

        // Chuyển hướng tới trang chủ sau khi đăng ký và đăng nhập thành công
        return RedirectToAction("Index", "Home"); // Chuyển hướng đến trang chủ
    }

    // Nếu có lỗi, hiển thị lại form đăng ký
    return View(register);
}


        public IActionResult Privacy()
        {
            return View();
        }

        // Phương thức xử lý lỗi
        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
