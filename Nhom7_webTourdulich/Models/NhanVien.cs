using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Nhom7_webTourdulich.Models;

public partial class NhanVien
{
 [Key]
 [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int MaNhanVien { get; set; } 

    public string Ten { get; set; } = null!;

    public int MaChucVu { get; set; } 
    public string? HinhAnh { get; set; }
    public string? LinkFB { get; set; }
    public string? LinkZL { get; set; }
    public string? LinkIG { get; set; }

    public virtual ChucVu MaChucVuNavigation { get; set; } = null!;
}
