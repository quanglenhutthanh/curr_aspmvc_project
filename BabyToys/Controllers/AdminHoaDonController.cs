using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using BabyToys.Models;
using PagedList;
using PagedList.Mvc;
using System.Data;
using BabyToys.Utilities;
using System.Data.Entity;
namespace BabyToys.Controllers
{
    public class AdminHoaDonController : admin_baseController
    {
        //
        // GET: /AdminHoaDon/
        DatabaseContext db = new DatabaseContext();
        [CustomActionFilter]
        public ActionResult Index(string from,string to,string indate,int? page,int? HienThi,int? SapXep)
        {
            if (!Models.User.CurrentUser.Quyen.CoChucNang("xem_hoa_don"))
            {
                return RedirectToAction("Error", "Admin");
            }
            if (Models.User.CurrentUser.Quyen.CoChucNang("sua_hoa_don"))
            {
                ViewBag.Xoa = true;
            }
            var hoadons = db.HoaDons.ToList();
            ViewBag.Count = hoadons.Count();
            ViewBag.ChuaXuLi = db.HoaDons.Where(h => h.TrangThai == 1).Count();
            if (!String.IsNullOrEmpty(indate))
            {
                DateTime InDate = DateTime.ParseExact(indate, "dd/MM/yyyy", null);
                hoadons = hoadons.Where(h => h.NgayLapHD >= InDate && h.NgayLapHD<InDate.AddDays(1)).ToList();
                ViewBag.Indate = indate;
            }
            if (!String.IsNullOrEmpty(from) && String.IsNullOrEmpty(to))
            {
                DateTime FromDate = DateTime.ParseExact(from, "dd/MM/yyyy", null);
                DateTime ToDate = System.DateTime.Now;
                ViewBag.FromDate = from;
                hoadons = hoadons.Where(h => h.NgayLapHD >= FromDate && h.NgayLapHD <= ToDate.AddDays(1)).ToList();
            }
            if (String.IsNullOrEmpty(from) && !String.IsNullOrEmpty(to))
            {
                DateTime ToDate = DateTime.ParseExact(to, "dd/MM/yyyy", null);
                DateTime FromDate = new DateTime(2013,01,01);
                ViewBag.ToDate = to;
                hoadons = hoadons.Where(h => h.NgayLapHD >= FromDate && h.NgayLapHD <= ToDate.AddDays(1)).ToList();
            }
            if (!String.IsNullOrEmpty(from) && !String.IsNullOrEmpty(to))
            {
                DateTime ToDate = DateTime.ParseExact(to, "dd/MM/yyyy", null);
                DateTime FromDate = DateTime.ParseExact(from, "dd/MM/yyyy", null);
                ViewBag.FromDate = from;
                ViewBag.ToDate = to;
                hoadons = hoadons.Where(h => h.NgayLapHD >= FromDate && h.NgayLapHD <= ToDate.AddDays(1)).ToList();
            }
            hoadons = hoadons.OrderByDescending(h => h.IdHoaDon).ToList();
            switch (SapXep ?? 0)
            {
                case 0:
                    hoadons = hoadons.Where(h => h.TrangThai == 1).ToList();
                    break;
                case 1:
                    hoadons = hoadons.Where(h => h.TrangThai==2).ToList();
                    break;
                case 2:
                    hoadons = hoadons.Where(h => h.TrangThai==3).ToList();
                    break;
                case 3:
                    hoadons = hoadons.Where(h => h.TrangThai == 4).ToList();
                    break;
                case 4:
                    hoadons = hoadons.OrderByDescending(h=>h.IdHoaDon).ToList();
                    break;
                case 5:
                    hoadons = hoadons.OrderBy(h=>h.IdHoaDon).ToList();
                    break;
                case 6:
                    hoadons = hoadons.OrderByDescending(h => h.TongGiaTri).ToList();
                    break;
                case 7:
                    hoadons = hoadons.OrderBy(h => h.TongGiaTri).ToList();
                    break;
                case 8:
                    break;
                default:
                    break;
            }
            
            var pageNumber = page ?? 1;
            ViewBag.OnePageOfHoaDon = hoadons.ToPagedList(pageNumber, HienThi??10);
            ViewBag.HienThi = HienThi??10;
            ViewBag.SapXep = SapXep??0;
            ViewBag.ThongBao = TempData["ThongBao"];
            return View();
        }
        [HttpPost]
        public ActionResult CapNhatTrangThai(int id, int trangthai)
        {
            var hoadon = db.HoaDons.SingleOrDefault(h => h.IdHoaDon == id);
            if(hoadon!=null)
            {
                //if (trangthai == 4)
                //{
                //    foreach (var cthd in hoadon.ChiTietHoaDons)
                //    {
                //        XuLiTonKho(cthd.SanPham, cthd.SoLuong);
                //    }
                //}
                hoadon.TrangThai = trangthai;
                db.Entry(hoadon).State = EntityState.Modified;
                db.SaveChanges();
                TempData["ThongBao"] = "<script>$( document ).ready(function(){ alert('Xóa thành công!') })</script>";
                return RedirectToAction("Index");
            }
            return RedirectToAction("Index");
        }

        public ActionResult ChiTiet(int id)
        {
            if (!Models.User.CurrentUser.Quyen.CoChucNang("xem_hoa_don"))
            {
                return RedirectToAction("Error", "Admin");
            }
            if (Models.User.CurrentUser.Quyen.CoChucNang("sua_hoa_don"))
            {
                ViewBag.Xoa = true;
            }
            var hoadon = db.HoaDons.SingleOrDefault(h => h.IdHoaDon == id);
            return View(hoadon);
        }

        public void XuLiTonKho(SanPham sanpham, int soluong)
        {
            var tonkho = db.TonKhos.Where(t => t.IdSanPham == sanpham.IdSanPham).ToList().OrderByDescending(t => t.Id).FirstOrDefault();
            int soluongmoi = soluong;
            if (tonkho != null)
            {
                soluongmoi = tonkho.SoLuong + soluong;
            }
            TonKho tonkhomoi = new TonKho { SanPham = sanpham, SoLuong = soluongmoi, ThoiGian = System.DateTime.Now, Nhap = 0, Xuat = -soluong };
            db.TonKhos.Add(tonkhomoi);
            db.SaveChanges();
        }
    }
}
