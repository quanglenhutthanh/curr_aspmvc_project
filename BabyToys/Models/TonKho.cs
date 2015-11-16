using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BabyToys.Models
{
    public class TonKho
    {
        public int Id { get; set; }
        public int IdSanPham { get; set; }
        public int Xuat { get;set;}
        public int Nhap { get; set; }
        public DateTime ThoiGian { get; set; }
        public int SoLuong { get; set; }

        public string Date()
        {
            return String.Format("{0:dd/MM/yyyy}", ThoiGian); 
        }
        public virtual SanPham SanPham { get; set; }
    }
}