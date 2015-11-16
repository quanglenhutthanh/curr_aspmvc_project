using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BabyToys.Models.ViewModel
{
    public class CartItem
    {
        public SanPham SanPham { get; set; }
        public int SoLuong { get; set; }
        //public decimal GiaHienTai { get; set; }
        public int Gia { get; set; }
        public bool VuotSoLuong { get; set; }
        public decimal ThanhTien
        {
            get { return (decimal)SoLuong*Gia;}
        }
    }
}