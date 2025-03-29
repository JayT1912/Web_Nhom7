using System;
using System.Collections.Generic;

namespace Nhom7_webTourdulich.Models;

public partial class ChiTietHoaDon
{
    
    public int MaHoaDon { get; set; }

    public int MaKhachHang { get; set; } 

    public decimal GiaTour { get; set; }

    public int SoLuong { get; set; }

    public decimal? ThanhTien { get; set; }

    public virtual HoaDon? MaHoaDonNavigation { get; set; } = null!;

    public virtual KhachHang? MaKhachHangNavigation { get; set; } = null!;
}
