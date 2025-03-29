using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Nhom7_webTourdulich.Models
{
    public partial class KhachHang
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int MaKhachHang { get; set; }  // Sử dụng int để dễ dàng tăng giá trị
        public string Ten { get; set; } = null!;
        public string Sdt { get; set; } = null!;
        public string DiaChi { get; set; } = null!;
        public string Email { get; set; } = null!;
        public string GioiTinh { get; set; } = null!;
        public string QuocTich { get; set; } = null!;
        public string? HinhAnh { get; set; }

        [ForeignKey("Tour")]
        public int? MaTour { get; set; }
        public virtual Tour? Tour { get; set; }

        public virtual ICollection<ChiTietHoaDon> ChiTietHoaDons { get; set; } = new List<ChiTietHoaDon>();
        public virtual ICollection<HoaDon> HoaDons { get; set; } = new List<HoaDon>();
    }
}