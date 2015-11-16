using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace BabyToys.Models
{
    public class ThuongHieu
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int IdThuongHieu { get; set; }
        public string TenThuongHieu { get; set; }
        public string QuocGia { get; set; }
        public string Logo { get; set; }
        public string ThongTin { get; set; }
        public string GhiChu { get; set; }
        public bool IsActive { get; set; }
        public virtual ICollection<SanPham> SanPhams { get; set; }
    }
}