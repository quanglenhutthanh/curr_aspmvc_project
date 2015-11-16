using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace BabyToys.Models
{
    public class DanhGia
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "Vui lòng nhập họ tên")]
        public string HoTen { get; set; }
        [Required(ErrorMessage="Vui lòng nhập Email")]
        [RegularExpression(@"^[\w-\.]+@([\w-]+\.)+[\w-]{2,4}$", ErrorMessage = "Email không đúng !")]
        public string Email { get; set; }
        public string BinhLuan { get; set; }
        [Required(ErrorMessage = "Vui lòng để lại đánh giá")]
        public int DiemDanhGia { get; set; }
        public DateTime ThoiGian { get; set; }
        public Nullable<int> IdSanPham { get; set; }
        public virtual SanPham SanPham { get; set; }
    }
}