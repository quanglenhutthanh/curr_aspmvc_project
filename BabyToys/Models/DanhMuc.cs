using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace BabyToys.Models
{
    public class DanhMuc
    {
        [Key,DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int IdDanhMuc { get; set; }
        public string TenDanhMuc { get; set; }
        public string MoTa { get; set; }
        public string GhiChu { get; set; }
        public bool IsActive { get; set; }
        
        
        public virtual ICollection<DanhMuc> DanhMucCons { get; set; }
        public virtual DanhMuc DanhMucCha { get; set; }
        public virtual ICollection<SanPham> SanPhams { get; set; }
    }
}