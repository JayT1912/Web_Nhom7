using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace Nhom7_webTourdulich.Models;


public partial class NhomTour
{
    
     [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int MaNhomTour { get; set; }

    public int MaTour { get; set; }

    public DateOnly NgayKhoiHanh { get; set; }

    public DateOnly NgayKetThuc { get; set; }

    public string DiemXuatPhat { get; set; } = null!;

    public int MaTrangThai { get; set; }

    public string? SoLuongNguoi { get; set; }
 
    public string? NoiDung { get; set; }

    public virtual ICollection<HoaDon>? HoaDons { get; set; } = new List<HoaDon>();

    public virtual Tour? MaTourNavigation { get; set; } = null!;

    public virtual TrangThai? MaTrangThaiNavigation { get; set; } = null!;
}
