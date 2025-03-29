using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace Nhom7_webTourdulich.Models;

public partial class GiaTour
{
     [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int MaGiaTour { get; set; } 

    public decimal Gia { get; set; }

    public DateOnly NgayBatDau { get; set; }

    public DateOnly NgayKetThuc { get; set; }

    public virtual ICollection<KhuyenMai> KhuyenMais { get; set; } = new List<KhuyenMai>();

    public virtual ICollection<Tour> Tours { get; set; } = new List<Tour>();
}
