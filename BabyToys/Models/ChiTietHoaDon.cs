using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BabyToys.Models
{
    public class ChiTietHoaDon
    {
        public int Id { get; set; }
        public int SoLuong { get; set; }
        public decimal ThanhTien { get; set; }
        public int IdHoaDon { get; set; }
        public int IdSanPham { get; set; }

        public virtual HoaDon HoaDon { get; set; }
        public virtual SanPham SanPham { get; set; }
    }
}