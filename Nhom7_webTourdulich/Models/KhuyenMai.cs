using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace Nhom7_webTourdulich.Models;

public partial class KhuyenMai
{
     [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int MaKhuyenMai { get; set; } 

    public string TenKhuyenMai { get; set; } = null!;

    public int MaGiaTour { get; set; }

    public double? PhanTramGiam { get; set; }

    public string? GiaGiam { get; set; }

    public DateOnly NgayBatDau { get; set; }

    public DateOnly NgayKetThuc { get; set; }

    public virtual GiaTour? MaGiaTourNavigation { get; set; } = null!;
}
