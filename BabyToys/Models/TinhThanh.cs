using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace BabyToys.Models
{
    public class TinhThanh
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int IdTinhThanh { get; set; }
        [Required(ErrorMessage = "Nhập tên tỉnh thành")]
        public string TenTinh { get; set; }

        [Required(ErrorMessage = "Nhập phí vận chuyển")]
        [RegularExpression(@"^(\d+)$", ErrorMessage = "Cần nhập phí vận chuyển kiểu số")]
        public int PhiVanChuyen { get; set; }

        public virtual ICollection<HoaDon> HoaDons { get; set; }
        public virtual ICollection<KhachHang> TaiKhoans { get; set; }
    }
}