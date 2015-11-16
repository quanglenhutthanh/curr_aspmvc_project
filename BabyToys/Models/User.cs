using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace BabyToys.Models
{
    public class User
    {
        public static User CurrentUser = null;
        public int Id { get; set; }
        public string TenDangNhap { get; set; }
        public string MatKhau { get; set; }
        public DateTime NgayTao { get; set; }
        public string TenDayDu { get; set; }
        public string SoDienThoai { get; set; }
        public string Email { get; set; }
        public string DiaChi { get; set; }

        public int IdQuyen { get; set; }
        public bool IsActive { get; set; }
        public Nullable<int> IdTinhThanh { get; set; }
        public virtual TinhThanh TinhThanh { get; set; }
        public Quyen Quyen { get; set; }
    }
}