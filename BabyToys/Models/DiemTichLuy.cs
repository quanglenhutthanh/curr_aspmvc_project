using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace BabyToys.Models
{
    public class DiemTichLuy
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public int IdKhachHang { get; set; }
        public DateTime ThoiGian { get; set; }
        public int Diem { get; set; }
        public int IdHoaDon { get; set; }

        public virtual HoaDon hoadon { get; set; }
        public virtual KhachHang khachhang { get; set; }
    }
}