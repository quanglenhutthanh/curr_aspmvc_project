using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace BabyToys.Models
{
    public class PhieuNhap
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int IdPhieuNhap { get; set; }
        public System.DateTime NgayLap { get; set; }
        public virtual User User { get; set; }
        public virtual List<ChiTietPhieuNhap> ChiTietPhieuNhap { get; set; }
        public int TongGiaTri
        {
            get { return (int)ChiTietPhieuNhap.Select(c => c.ThanhTien).Sum(); }
        }
    }
}