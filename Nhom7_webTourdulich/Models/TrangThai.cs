using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace Nhom7_webTourdulich.Models;


public partial class TrangThai
{

     [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]   
         public int MaTrangThai { get; set; } 

    public string TenTrangThai { get; set; } = null!;

    public virtual ICollection<NhomTour> NhomTours { get; set; } = new List<NhomTour>();
}
