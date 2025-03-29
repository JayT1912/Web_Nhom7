using Nhom7_webTourdulich.Models;
using Nhom7_webTourdulich.Repositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.IO;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;

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

[Authorize(Policy = "AdminOrManagePolicy")]
[HttpPost]
public async Task<IActionResult> Update(int id, User user, IFormFile avatar)
{
    if (id != user.Id)
    {
        return NotFound(); // Nếu ID không khớp
    }

    if (ModelState.IsValid) // Kiểm tra dữ liệu hợp lệ
    {
        var existingUser = await _userRepository.GetByIdAsync(id);
        if (existingUser == null)
        {
            return NotFound(); // Nếu người dùng không tồn tại
        }

        // Xử lý ảnh đại diện (nếu có)
        if (avatar != null && avatar.Length > 0)
        {
            var fileName = Path.GetFileName(avatar.FileName);
            var filePath = Path.Combine(_webHostEnvironment.WebRootPath, "images", fileName);
            using (var fileStream = new FileStream(filePath, FileMode.Create))
            {
                await avatar.CopyToAsync(fileStream); // Lưu ảnh
            }
            existingUser.ImageUrl = "/images/" + fileName; // Cập nhật đường dẫn ảnh
        }
        else
        {
            // Nếu không có ảnh mới, giữ nguyên ảnh cũ
            existingUser.ImageUrl = existingUser.ImageUrl ?? "/images/default-avatar.jpg"; // Nếu không có ảnh, sử dụng ảnh mặc định
        }

        // Xử lý mật khẩu (nếu có thay đổi)
        if (!string.IsNullOrEmpty(user.Password))
        {
            existingUser.Password = user.Password; // Cập nhật mật khẩu nếu có thay đổi
        }
        else
        {
            // Nếu không thay đổi mật khẩu, giữ nguyên mật khẩu cũ
            existingUser.Password = existingUser.Password;
        }

        // Cập nhật các thông tin khác của người dùng
        existingUser.FullName = user.FullName;
        existingUser.Email = user.Email;
        existingUser.Username = user.Username;
        existingUser.DateOfBirth = user.DateOfBirth;  // Cập nhật Ngày sinh
        existingUser.CreatedAt = user.CreatedAt;  // Cập nhật Ngày tạo tài khoản
        existingUser.Role = user.Role;

        await _userRepository.UpdateAsync(existingUser); // Lưu thay đổi
        return RedirectToAction(nameof(Index)); // Chuyển hướng về trang danh sách người dùng
    }
    return View(user); // Trả lại view nếu dữ liệu không hợp lệ
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
            return View(user);
        }

        // Post request to delete user
        [Authorize(Policy = "AdminOrManagePolicy")]
        [HttpPost, ActionName("Delete")]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            await _userRepository.DeleteAsync(id);
            return RedirectToAction(nameof(Index));
        }
    }
}
