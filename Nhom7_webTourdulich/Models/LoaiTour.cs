using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace Nhom7_webTourdulich.Models;

public partial class LoaiTour
{
     [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int MaLoaiTour { get; set; }

    public string TenLoaiTour { get; set; } = null!;

    public virtual ICollection<Tour>? Tours { get; set; } = new List<Tour>();
}
