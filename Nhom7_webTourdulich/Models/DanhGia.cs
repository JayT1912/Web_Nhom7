using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Nhom7_webTourdulich.Models
{
    public class DanhGia
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int MaDanhGia { get; set; }  // Khóa chính

        [Required]
        public string NoiDung { get; set; } = null!;  // Nội dung đánh giá

        [Required]
        [Range(1, 5, ErrorMessage = "Bạn chỉ có thể chọn từ 1 đến 5 sao.")]
        public int SoSao { get; set; }  // Số sao đánh giá

        public DateTime? NgayDanhGia { get; set; } = DateTime.Now;  // Ngày đánh giá, có thể để null


        [ForeignKey("Tour")]
        public int MaTour { get; set; }  // Mã Tour (bắt buộc)
        public virtual Tour Tour { get; set; } = null!;  // Liên kết với bảng Tour

        [Required]
        public string Username { get; set; } = null!;  // Username từ bảng User (bắt buộc)
    }
}
