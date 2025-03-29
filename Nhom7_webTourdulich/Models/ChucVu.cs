using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace Nhom7_webTourdulich.Models;


public partial class ChucVu
{
     [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int MaChucVu { get; set; } 

    public string TenChucVu { get; set; } = null!;

    public virtual ICollection<NhanVien>? NhanViens{ get; set; } = new List<NhanVien>();
}
