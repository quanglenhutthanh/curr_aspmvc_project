using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using BabyToys.Models;
using System.Data;
using BabyToys.Models.ViewModel;
using System.Data.Entity;
namespace BabyToys.DAL
{
    public class RegisterDAL
    {
        DatabaseContext db = new DatabaseContext();
        public void ThemKhachHang(KhachHangViewModel register)
        {
            KhachHang khachhang = new KhachHang();
            khachhang.TenDangNhap = register.TenDangNhap;
            khachhang.TenDayDu = register.TenDayDu;
            khachhang.MatKhau = BabyToys.Utilities.EditString.mahoa_md5(register.MatKhau);
            khachhang.NgayTao = System.DateTime.Now;
            khachhang.Email = register.Email;
            khachhang.DiaChi = register.DiaChi;
            khachhang.SoDienThoai = register.SoDienThoai;
            khachhang.IdTinhThanh = register.IdTinhThanh;
            khachhang.IdLoaiKhachHang = register.IdLoaiKhachHang;
            khachhang.IsActive = true;
            db.KhachHangs.Add(khachhang);
            db.SaveChanges();
        }
        public void SuaKhachHang(KhachHangViewModel kh)
        {
            KhachHang khachhang = new KhachHang();
            khachhang.IdKhachHang = kh.Id;
            khachhang.TenDangNhap = kh.TenDangNhap;
            khachhang.NgayTao = kh.NgayTao;
            khachhang.TenDayDu = kh.TenDayDu;
            khachhang.MatKhau = kh.MatKhau;
            khachhang.Email = kh.Email;
            khachhang.DiaChi = kh.DiaChi;
            khachhang.SoDienThoai = kh.SoDienThoai;
            khachhang.IdLoaiKhachHang = kh.IdLoaiKhachHang;
            khachhang.IdTinhThanh = kh.IdTinhThanh;
            khachhang.IsActive = kh.IsActive;
            db.Entry(khachhang).State = EntityState.Modified;
            db.SaveChanges();
        }
        public void ThemNhanVien(NhanVienViewModel register)
        {
            User user = new User();
            user.TenDangNhap = register.TenDangNhap;
            user.TenDayDu = register.TenDayDu;
            user.MatKhau = BabyToys.Utilities.EditString.mahoa_md5(register.MatKhau);
            user.NgayTao = System.DateTime.Now;
            user.Email = register.Email;
            user.DiaChi = register.DiaChi;
            user.SoDienThoai = register.SoDienThoai;
            user.IdQuyen = register.IdQuyen;
            user.IsActive = true;
            db.Users.Add(user);
            db.SaveChanges();
        }
        public void SuaNhanVien(NhanVienViewModel u)
        {
            User user = new User();
            user.Id = u.Id;
            user.TenDangNhap = u.TenDangNhap;
            user.TenDayDu = u.TenDayDu;
            user.MatKhau = u.MatKhau;
            user.NgayTao = u.NgayTao;
            user.Email = u.Email;
            user.DiaChi = u.DiaChi;
            user.SoDienThoai = u.SoDienThoai;
            user.IdQuyen = u.IdQuyen;
            user.IsActive = u.IsActive;
            db.Entry(user).State = EntityState.Modified;
            db.SaveChanges();
        }
    }
}