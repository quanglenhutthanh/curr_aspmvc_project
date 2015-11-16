using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace BabyToys.Models
{
    public class LoaiKhachHang
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int IdLoaiKhachHang { get; set; }
        [Required(ErrorMessage = "Nhập tên loại!")]
        public string TenLoai { get; set; }
        [Required(ErrorMessage = "Nhập điểm tích lũy cần có!")]
        [RegularExpression("\\d+", ErrorMessage = "Điểm phải là một số nguyên dương")]
        public int Diem { get; set; }
        [Required(ErrorMessage = "Nhập giá giảm!")]
        [Range(0, 100, ErrorMessage="Giảm giá không được nhỏ hơn 1% và vượt quá 100%")]
        public int GiamGia { get; set; }
        public string GhiChu { get; set; }
    }
}