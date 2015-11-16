using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace BabyToys.Models
{
    public class TaiKhoan
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public string TenDangNhap { get; set; }
        public string TenDayDu { get; set; }
        public string SoDienThoai { get; set; }
        public string Email { get; set; }
        public string DiaChi { get; set; }

        public virtual TinhThanh TinhThanh { get; set; }
        public virtual Quyen Quyen { get; set; }
        public virtual List<HoaDon> HoaDons { get; set; }

    }
}