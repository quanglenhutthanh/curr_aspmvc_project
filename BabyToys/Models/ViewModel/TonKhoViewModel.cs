using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BabyToys.Models.ViewModel
{
    public class TonKhoViewModel
    {
        public int Id { get; set; }
        public SanPham SanPham { get; set; }
        public string ThoiGian { get; set; }
        public int TonDau { get; set; }
        public int TonCuoi { get; set; }
        public int Nhap { get; set; }
        public int Xuat { get; set; }
    }
}