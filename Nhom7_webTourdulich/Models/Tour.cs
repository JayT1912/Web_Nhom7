using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Nhom7_webTourdulich.Models
{
    public partial class Tour
    {
         [Key]
         [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
         public int MaTour { get; set; } 

         public string Ten { get; set; } = null!;

         public int MaLoaiTour { get; set; } 

         public int MaGiaTour { get; set; } 
         public int MaDiemDen { get; set; }

         public string SoNgay { get; set; }
         public string? SoLuongNguoi { get; set; }
         public string? MoTa { get; set; }

         // Quan hệ với DiemDen
         public virtual DiemDen MaDiemDenNavigation { get; set; } = null!;

         // Quan hệ với GiaTour
         public virtual GiaTour MaGiaTourNavigation { get; set; } = null!;

         // Quan hệ với LoaiTour
         public virtual LoaiTour MaLoaiTourNavigation { get; set; } = null!;

         public ICollection<KhachHang>? KhachHangs { get; set; }

         // Quan hệ với TourImage
         public virtual ICollection<TourImage> TourImages { get; set; } = new List<TourImage>();

         // Quan hệ với NhomTour
         public virtual ICollection<NhomTour> NhomTours { get; set; } = new List<NhomTour>();

         // THÊM MÔI LIÊN HỆ VỚI BẢNG DANHGIA
         public virtual ICollection<DanhGia> DanhGias { get; set; } = new List<DanhGia>();
    }
}
