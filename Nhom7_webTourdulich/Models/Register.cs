using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Nhom7_webTourdulich.Models
{
    public partial class Register
    {
        [Key]
        public int Id { get; set; }

        [Required(ErrorMessage = "Vui lòng nhập họ và tên")]
        [Display(Name = "Họ và tên")]
        public string FullName { get; set; }

        [Required(ErrorMessage = "Vui lòng nhập email")]
        [EmailAddress(ErrorMessage = "Địa chỉ email không hợp lệ")]
        [Display(Name = "Email")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Vui lòng nhập tên đăng nhập")]
        [Display(Name = "Tên đăng nhập")]
        public string Username { get; set; }

        [Required(ErrorMessage = "Vui lòng nhập mật khẩu")]
        [DataType(DataType.Password)]
        [Display(Name = "Mật khẩu")]
        public string Password { get; set; }

        [Required(ErrorMessage = "Vui lòng nhập lại mật khẩu")]
        [DataType(DataType.Password)]
        [Compare("Password", ErrorMessage = "Mật khẩu nhập lại không khớp")]
        [Display(Name = "Nhập lại mật khẩu")]
        public string RepeatPassword { get; set; }

        [StringLength(200)]
        public string? Address { get; set; } // Địa chỉ cho phép NULL

        [StringLength(15)]
        public string? PhoneNumber { get; set; } // Số điện thoại cho phép NULL

        public DateTime? DateOfBirth { get; set; } // Ngày sinh cho phép NULL

        [Required]
        [StringLength(50)]
        public string Role { get; set; } = "user";

        [Range(typeof(bool), "true", "true", ErrorMessage = "Bạn phải đồng ý với điều khoản sử dụng")]
        [Display(Name = "Tôi đồng ý với điều khoản sử dụng")]
        public bool IsTermsAccepted { get; set; }
    }
}
