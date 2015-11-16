using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BabyToys.Models
{
    public class HinhAnh
    {
        public int ID { get; set; }
        public string AnhGoc { get; set; }
        public string Thumbnail { get; set; }
        
        public virtual SanPham SanPham { get; set; }
    }
}