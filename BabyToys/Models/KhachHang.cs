using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace BabyToys.Models
{
    public class KhachHang
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int IdKhachHang { get; set; }
        public string TenDangNhap { get; set; }
        public string TenDayDu { get; set; }
        public string MatKhau { get; set; }
        public DateTime NgayTao { get; set; }
        public string SoDienThoai { get; set; }
        public string Email { get; set; }
        public string DiaChi { get; set; }
        public string GhiChu { get; set; }
        public bool IsActive { get; set; }
         [Required(ErrorMessage = "Vui lòng chọn tỉnh thành nơi bạn ở.")]
        public int IdTinhThanh { get; set; }
        public int IdLoaiKhachHang { get; set; }
        public virtual List<HoaDon> ListDonHang { get; set; }
        public virtual TinhThanh TinhThanh { get; set; }
        public virtual LoaiKhachHang LoaiKhachHang { get; set; }
        public virtual List<DiemTichLuy> ListDiemTichLuy { get; set; }
        public override string ToString()
        {
            return TenDangNhap;
        }

        public int TongDiemTichLuy()
        {
            return ListDiemTichLuy!=null?(ListDiemTichLuy.ToList().Select(d => d.Diem)).Sum():0;
        }
    }
}