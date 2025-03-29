using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Nhom7_webTourdulich.Models
{
   public class TourImage
{
   [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int Id { get; set; }
    public int MaTour { get; set; } // Khóa ngoại tới bảng Tour
    public string HinhAnh { get; set; } = null!; // Đường dẫn hình ảnh

    public virtual Tour? Tour { get; set; } = null!;
}

}