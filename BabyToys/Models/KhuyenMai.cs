using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace BabyToys.Models
{
    public class KhuyenMai
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int IdKhuyenMai { get; set; }
        public DateTime TuNgay { get; set; }
        public DateTime DenNgay { get; set; }
        public int PhanTramGiam { get; set; }
        public int IdSanPham { get; set; }

        public virtual SanPham SanPham { get; set; }
    }
}