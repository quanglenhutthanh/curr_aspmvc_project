using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace BabyToys.Models
{
    public class HoaDon
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int IdHoaDon { get; set; }
        public System.DateTime NgayLapHD { get; set; }
        public decimal TienHang { get; set; }
        public decimal PhiVanChuyen { get; set; }
        public decimal TienKhuyenMai { get; set; }

        public string DiaChiGiaoHang { get; set; }
        public string TenNguoiNhan { get; set; }
        public string Email { get; set; }
        public string SoDienThoai { get; set; }
        public Nullable<int> IdTinhThanh { get; set; }

        //public decimal TongTien { get; set; }
        public int TrangThai { get; set; }
        public Nullable<int> IdKhachHang { get; set; }

        public virtual List<ChiTietHoaDon> ChiTietHoaDons { get; set; }
        public virtual KhachHang KhachHang{ get; set; }
        public virtual TinhThanh TinhThanh { get; set; }

        public decimal TongGiaTri
        {
            get { return (TienHang + PhiVanChuyen - TienKhuyenMai); }
        }
    }
}