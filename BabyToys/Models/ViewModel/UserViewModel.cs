using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;


namespace BabyToys.Models
{
    public class UserViewModel
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "Tên đăng nhập không được để trống !")]
        [StringLength(20, ErrorMessage = "Tên đăng nhập có ít nhất {2} kí tự, và không vượt quá {1} kí tự", MinimumLength = 3)]
        public string TenDangNhap { get; set; }
        public string TenDayDu { get; set; }
        [RegularExpression(@"^(\d{10,11})$", ErrorMessage = "Số điện thoại không đúng !")]
        public string SoDienThoai { get; set; }
        [RegularExpression(@"^[\w-\.]+@([\w-]+\.)+[\w-]{2,4}$", ErrorMessage = "Email không hợp lệ !")]
        public string Email { get; set; }
        public DateTime NgayTao { get; set; }
        public string DiaChi { get; set; }
        public string GhiChu { get; set; }
        public int IdTinhThanh { get; set; }
        [Required(ErrorMessage = "Mật khẩu không được để trống")]
        [StringLength(100, ErrorMessage = "Mật khẩu có ít nhất {2} kí tự", MinimumLength = 6)]
        public string MatKhau { get; set; }
        [Compare("MatKhau", ErrorMessage = "Mật khẩu và mật khẩu xác nhận không đúng.")]
        public string XacNhanMatKhau { get; set; }


        public bool IsActive { get; set; }
    }
}