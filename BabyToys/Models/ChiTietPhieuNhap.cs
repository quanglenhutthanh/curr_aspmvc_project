using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BabyToys.Models
{
    public class ChiTietPhieuNhap
    {
        public int Id { get; set; }
        public int SoLuong { get; set; }
        public decimal ThanhTien { get; set; }
        public int IdPhieuNhap { get; set; }
        public int IdSanPham { get; set; }
        public int? GiaNhap { get; set; }
        public virtual PhieuNhap PhieuNhap { get; set; }
        public virtual SanPham SanPham { get; set; }
    }
}