using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using BabyToys.Models;
using BabyToys.Models.ViewModel;
using PagedList;
using PagedList.Mvc;
using System.Data;
using System.Data.Entity;
namespace BabyToys.Controllers
{
    public class AdminKhachHangController : admin_baseController
    {
        //
        // GET: /AdminKhachHang/
        DatabaseContext db = new DatabaseContext();
        public ActionResult Index(int? page, string currentFilter, string searchString)
        {
            if (!Models.User.CurrentUser.Quyen.CoChucNang("xem_khach_hang"))
            {
                return RedirectToAction("Error", "Admin");
            }
            if (Models.User.CurrentUser.Quyen.CoChucNang("sua_khach_hang"))
            {
                ViewBag.Xoa = true;
            }
            var khachhangs = db.KhachHangs.ToList();
            var pageNumber = page ?? 1;
            if (searchString != null)
            {
                page = 1;
            }
            else
            {
                searchString = currentFilter;
            }

            ViewBag.CurrentFilter = searchString;
            if (!String.IsNullOrEmpty(searchString))
            {
                khachhangs = khachhangs.Where(t => t.TenDangNhap.ToLower().Contains(searchString.ToLower())).ToList();
            }
            ViewBag.onePageOfKhachHang = khachhangs.ToPagedList(pageNumber, 10);
            return View();
        }

        public ActionResult ThemKhachHang()
        {
            if (!Models.User.CurrentUser.Quyen.CoChucNang("them_khach_hang"))
            {
                return RedirectToAction("Error", "Admin");
            }
            ViewBag.IdLoaiKhachHang = new SelectList(db.LoaiKhachHangs.ToList(), "IdLoaiKhachHang", "TenLoai");
            ViewBag.IdtinhThanh = new SelectList(db.TinhThanhs.ToList(), "IdTinhThanh", "TenTinh");
            return View();
        }
        [HttpPost]
        public ActionResult ThemKhachHang(KhachHangViewModel khachhang)
        {
            if (ModelState.IsValid)
            {
                BabyToys.DAL.RegisterDAL reg = new DAL.RegisterDAL();
                reg.ThemKhachHang(khachhang);
                return RedirectToAction("Index");
            }
            ViewBag.IdtinhThanh = new SelectList(db.TinhThanhs.ToList(), "IdTinhThanh", "TenTinh");
            ViewBag.IdLoaiKhachHang = new SelectList(db.LoaiKhachHangs.ToList(), "IdLoaiKhachHang", "TenLoai", khachhang.IdLoaiKhachHang);
            return View();
        }

        public ActionResult SuaKhachHang(int id)
        {
            if (!Models.User.CurrentUser.Quyen.CoChucNang("xem_khach_hang"))
            {
                return RedirectToAction("Error", "Admin");
            }
            
            var kh = db.KhachHangs.SingleOrDefault(u => u.IdKhachHang == id);
            KhachHangViewModel khachhang=new KhachHangViewModel
            //userModel = user;
            {
                Id = kh.IdKhachHang,
                IdLoaiKhachHang=kh.IdLoaiKhachHang,
                TenDangNhap = kh.TenDangNhap,
                TenDayDu = kh.TenDayDu,
                MatKhau = kh.MatKhau,
                XacNhanMatKhau = kh.MatKhau,
                Email = kh.Email,
                DiaChi = kh.DiaChi,
                SoDienThoai = kh.SoDienThoai,
                IsActive = kh.IsActive,
                NgayTao=kh.NgayTao,
                IdTinhThanh=kh.IdTinhThanh
            };
            //userModel = user as RegisterModel;
            ViewBag.IdLoaiKhachHang = new SelectList(db.LoaiKhachHangs.ToList(), "IdLoaiKhachHang", "TenLoai", kh.IdLoaiKhachHang);
            ViewBag.IdtinhThanh = new SelectList(db.TinhThanhs.ToList(), "IdTinhThanh", "TenTinh",kh.IdTinhThanh);
            return View(khachhang);
        }

        [HttpPost]
        public ActionResult SuaKhachHang(KhachHangViewModel khachhang)
        {
            if (!Models.User.CurrentUser.Quyen.CoChucNang("sua_khach_hang"))
            {
                return RedirectToAction("Error", "Admin");
            }
            if (ModelState.IsValid)
            {
                var kh = db.KhachHangs.SingleOrDefault(u => u.IdKhachHang == khachhang.Id);
                if (kh.MatKhau != khachhang.MatKhau)
                {
                    khachhang.MatKhau = Utilities.EditString.mahoa_md5(khachhang.MatKhau);
                }
                BabyToys.DAL.RegisterDAL reg = new DAL.RegisterDAL();
                reg.SuaKhachHang(khachhang);
                return RedirectToAction("Index");
            }
            ViewBag.IdtinhThanh = new SelectList(db.TinhThanhs.ToList(), "IdTinhThanh", "TenTinh", khachhang.IdTinhThanh);
            ViewBag.IdLoaiKhachHang = new SelectList(db.LoaiKhachHangs.ToList(), "IdLoaiKhachHang", "TenLoai", khachhang.IdLoaiKhachHang);
            return View(khachhang);
        }
        [HttpPost]
        public ActionResult CapNhatTrangThai(int id, bool trangthai)
        {
            var khachhang = db.KhachHangs.SingleOrDefault(u => u.IdKhachHang == id);
            if (khachhang != null)
            {
                khachhang.IsActive = trangthai;
            }
            db.Entry(khachhang).State = EntityState.Modified;
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        public ActionResult LoaiKhachHang()
        {
            if (!Models.User.CurrentUser.Quyen.CoChucNang("xem_loai_khach_hang"))
            {
                return RedirectToAction("Error", "Admin");
            }
            var loaikhachhangs = db.LoaiKhachHangs.ToList();
            return View(loaikhachhangs);
        }

        public ActionResult ThemLoaiKhachHang()
        {
            if (!Models.User.CurrentUser.Quyen.CoChucNang("them_loai_khach_hang"))
            {
                return RedirectToAction("Error", "Admin");
            }
            return View();
        }
        [HttpPost]
        public ActionResult ThemLoaiKhachHang(LoaiKhachHang loaikhachhang)
        {
            if (ModelState.IsValid)
            {
                db.LoaiKhachHangs.Add(loaikhachhang);
                db.SaveChanges();
                return RedirectToAction("LoaiKhachHang");
            }
            return View(loaikhachhang);
        }

        public ActionResult SuaLoaiKhachHang(int id)
        {
            if (!Models.User.CurrentUser.Quyen.CoChucNang("xem_loai_khach_hang"))
            {
                return RedirectToAction("Error", "Admin");
            }
            var loaikhachhang = db.LoaiKhachHangs.SingleOrDefault(l => l.IdLoaiKhachHang == id);
            return View(loaikhachhang);
        }
        [HttpPost]
        public ActionResult SuaLoaiKhachHang(LoaiKhachHang loaikhachhang)
        {
            if (!Models.User.CurrentUser.Quyen.CoChucNang("sua_loai_khach_hang"))
            {
                return RedirectToAction("Error", "Admin");
            }
            if (ModelState.IsValid)
            {
                db.Entry(loaikhachhang).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("LoaiKhachHang");
            }
            return View(loaikhachhang);
        }
    }
}
