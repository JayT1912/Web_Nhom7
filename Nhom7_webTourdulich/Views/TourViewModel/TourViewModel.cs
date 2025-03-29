using System.Collections.Generic;
using Nhom7_webTourdulich.Models; // Namespace chứa các model chính

namespace Nhom7_webTourdulich.ViewModels
{
    public class TourViewModel
    {
        // Thông tin cơ bản của Tour
        public int MaTour { get; set; }
        public string Ten { get; set; } = null!;
        public string MoTa { get; set; } = null!;
        public string SoNgay { get; set; } = null!;
        public string SoLuongNguoi { get; set; } = null!;

        // Quan hệ với các bảng liên quan
        public DiemDen? MaDiemDenNavigation { get; set; }
        public GiaTour? MaGiaTourNavigation { get; set; }
        public LoaiTour? MaLoaiTourNavigation { get; set; }

        // Hình ảnh liên quan đến Tour
        public ICollection<TourImage>? TourImages { get; set; }

        // Danh sách đánh giá
        public ICollection<DanhGia>? DanhGias { get; set; }

        // Tính toán đánh giá tổng thể
        public double AverageRating { get; set; } = 0;
        public int ReviewCount { get; set; } = 0;
    }
}
