using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;

namespace Nhom7_webTourdulich.Models
{
    public class User
    {
        [Key]
        public int Id { get; set; }

        public string FullName { get; set; }

        public string Email { get; set; }
        [Required]
        public string Username {get; set;}  
        [Required, StringLength(100)]
        public string Password { get; set; }

        public string Role { get; set; }
        public DateTime DateOfBirth { get; set; } // Ngày sinh
        public string Address { get; set; } // Địa chỉ
        public string PhoneNumber { get; set; } // Số điện thoại
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow; // Ngày tạo tài khoản
        public string? ImageUrl { get; set; }
        public List<UserImage>? UserImages { get; set; }
    }

}
