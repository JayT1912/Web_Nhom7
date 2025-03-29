using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace Nhom7_webTourdulich.Models;


public partial class DiemDen
{
     [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int MaDiemDen { get; set; } 

    public string Ten { get; set; } = null!;

    public string ThanhPho { get; set; } = null!;

    public virtual ICollection<Tour> Tours { get; set; } = new List<Tour>();
}
