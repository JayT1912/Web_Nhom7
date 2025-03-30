using Nhom7_webTourdulich.Models;
using Nhom7_webTourdulich.Repositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.IO;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using System.Security.Claims;  // Đảm bảo rằng bạn đã thêm namespace này

namespace Nhom7_webTourdulich.Controllers
{
    [Authorize]
    public class UserController : Controller
    {
        private readonly IUserRepository _userRepository;
        private readonly IWebHostEnvironment _webHostEnvironment;

        public UserController(IUserRepository userRepository, IWebHostEnvironment webHostEnvironment)
        {
            _userRepository = userRepository;
            _webHostEnvironment = webHostEnvironment;
        }

        // View to display all users
        public async Task<IActionResult> Index()
        {
            var users = await _userRepository.GetAllAsync();
            return View(users);
        }

        // View to create a new user
        [Authorize(Policy = "AdminOrManagePolicy")]
        public IActionResult Create()
        {
            return View();
        }

        // Post request to create a new user
        [Authorize(Policy = "AdminOrManagePolicy")]
        [HttpPost]
        public async Task<IActionResult> Create(User user, IFormFile avatar)
        {
            if (ModelState.IsValid)
            {
                if (avatar != null && avatar.Length > 0)
                {
                    var fileName = Path.GetFileName(avatar.FileName);
                    var filePath = Path.Combine(_webHostEnvironment.WebRootPath, "images", fileName);

                    using (var fileStream = new FileStream(filePath, FileMode.Create))
                    {
                        await avatar.CopyToAsync(fileStream);
                    }

                    user.ImageUrl = "/images/" + fileName;
                }

                await _userRepository.AddAsync(user);
                return RedirectToAction(nameof(Index));
            }
            return View(user);
        }

        // View to display a single user's details
        [Authorize(Policy = "AdminOrManagePolicy")]
        public async Task<IActionResult> Display(int id)
        {
            var user = await _userRepository.GetByIdAsync(id);
            if (user == null)
            {
                return NotFound();
            }
            return View(user);
        }
// Phương thức GET để hiển thị form update
[Authorize(Policy = "AdminOrManagePolicy")]
public async Task<IActionResult> Update(int id)
{
    var user = await _userRepository.GetByIdAsync(id);
    if (user == null)
    {
        return NotFound(); // Nếu không tìm thấy người dùng
    }
    return View(user); // Trả về view với dữ liệu người dùng
}

[HttpPost]
[ValidateAntiForgeryToken]
public async Task<IActionResult> Update(int id, User user, IFormFile avatar)
{
    if (id != user.Id)
    {
        return NotFound();
    }

    if (ModelState.IsValid)
    {
        var existingUser = await _userRepository.GetByIdAsync(id);
        if (existingUser == null)
        {
            return NotFound();
        }

        // Kiểm tra nếu người dùng đang nâng cấp từ User lên Manager
        if (existingUser.Role == "User" && user.Role == "Manager")
        {
            existingUser.Role = "Manager"; // Cập nhật vai trò lên Manager
        }

        // Nếu người dùng đang nâng cấp từ Manager lên Admin
        if (existingUser.Role == "Manager" && user.Role == "Admin")
        {
            existingUser.Role = "Admin"; // Cập nhật vai trò lên Admin
        }

        // Xử lý ảnh đại diện (nếu có)
        if (avatar != null && avatar.Length > 0)
        {
            var fileName = Path.GetFileName(avatar.FileName);
            var filePath = Path.Combine(_webHostEnvironment.WebRootPath, "images", fileName);
            using (var fileStream = new FileStream(filePath, FileMode.Create))
            {
                await avatar.CopyToAsync(fileStream);
            }
            existingUser.ImageUrl = "/images/" + fileName;
        }

        // Cập nhật các thông tin khác của người dùng
        existingUser.FullName = user.FullName;
        existingUser.Email = user.Email;
        existingUser.Username = user.Username;
        existingUser.DateOfBirth = user.DateOfBirth;  
        existingUser.CreatedAt = user.CreatedAt;  
        existingUser.Role = user.Role;  // Cập nhật vai trò

        await _userRepository.UpdateAsync(existingUser);
        return RedirectToAction(nameof(Index)); // Chuyển hướng về trang danh sách người dùng
    }

    return View(user);
}


      // View to delete user
[Authorize(Policy = "AdminOrManagePolicy")]
public async Task<IActionResult> Delete(int id)
{
    var user = await _userRepository.GetByIdAsync(id);
    if (user == null)
    {
        return NotFound();
    }

    // Kiểm tra nếu người đăng nhập là Admin và người cần xóa cũng là Admin
    var loggedInUserRole = HttpContext.User.FindFirst(ClaimTypes.Role)?.Value;  // Lấy vai trò người đăng nhập
    if (user.Role == "Admin" && loggedInUserRole == "Admin")
    {
        // Nếu người đăng nhập và người cần xóa đều là Admin
        ModelState.AddModelError("", "Không thể xóa tài khoản Admin.");
        return RedirectToAction(nameof(Index)); // Quay lại trang danh sách người dùng
    }

    return View(user);  // Nếu không phải là Admin, cho phép xóa
}

// Post request to delete user
[Authorize(Policy = "AdminOrManagePolicy")]
[HttpPost, ActionName("Delete")]
public async Task<IActionResult> DeleteConfirmed(int id)
{
    var userToDelete = await _userRepository.GetByIdAsync(id);
    var loggedInUserRole = HttpContext.User.FindFirst(ClaimTypes.Role)?.Value;  // Lấy vai trò người đăng nhập

    // Kiểm tra nếu người đăng nhập là Admin và người cần xóa cũng là Admin
    if (userToDelete.Role == "Admin" && loggedInUserRole == "Admin")
    {
        // Nếu người đăng nhập và người cần xóa đều là Admin
        ModelState.AddModelError("", "Không thể xóa tài khoản Admin.");
        return RedirectToAction(nameof(Index));  // Quay lại trang danh sách người dùng
    }

    // Nếu người cần xóa không phải là Admin, thực hiện xóa
    await _userRepository.DeleteAsync(id);
    return RedirectToAction(nameof(Index));  // Quay lại trang danh sách người dùng
        }

    }
}
