using BabyToys.Models;
using BabyToys.Models.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using PagedList;
using PagedList.Mvc;
using System.Data;
using System.Data.Entity;
namespace BabyToys.Controllers
{
    public class AdminNhapHangController : admin_baseController
    {
        //
        // GET: /AdminNhapHang/
        DatabaseContext db = new DatabaseContext();

        public ActionResult Index()
        {
            if (!Models.User.CurrentUser.Quyen.CoChucNang("nhap_hang"))
            {
                return RedirectToAction("Error", "Admin");
            }
            
            var cart = CartSession.GetCart(this.HttpContext);
            if (cart != null)
            {
                ViewBag.cart = cart;
            }
            ViewBag.error = TempData["error"];
            return View();
        }

        public ActionResult DanhSach(string from, string to, string indate, int? page, int? hienthi, int? sapxep)
        {
            if (!Models.User.CurrentUser.Quyen.CoChucNang("xem_phieu_nhap"))
            {
                return RedirectToAction("Error", "Admin");
            }
            var phieunhaps = db.PhieuNhaps.ToList();
            ViewBag.Count = phieunhaps.Count();
            if (!String.IsNullOrEmpty(indate))
            {
                DateTime InDate = DateTime.ParseExact(indate, "dd/MM/yyyy", null);

                phieunhaps = phieunhaps.Where(h => h.NgayLap >= InDate && h.NgayLap < InDate.AddDays(1)).ToList();
                ViewBag.Indate = indate;
            }
            if (!String.IsNullOrEmpty(from) && String.IsNullOrEmpty(to))
            {
                DateTime FromDate = DateTime.ParseExact(from, "dd/MM/yyyy", null);
                DateTime ToDate = System.DateTime.Now;
                ViewBag.FromDate = from;
                phieunhaps = phieunhaps.Where(h => h.NgayLap >= FromDate && h.NgayLap <= ToDate).ToList();
            }
            if (String.IsNullOrEmpty(from) && !String.IsNullOrEmpty(to))
            {
                DateTime ToDate = DateTime.ParseExact(to, "dd/MM/yyyy", null);
                DateTime FromDate = new DateTime(2013, 01, 01);
                ViewBag.ToDate = to;
                phieunhaps = phieunhaps.Where(h => h.NgayLap >= FromDate && h.NgayLap <= ToDate).ToList();
            }
            if (!String.IsNullOrEmpty(from) && !String.IsNullOrEmpty(to))
            {
                DateTime ToDate = DateTime.ParseExact(to, "dd/MM/yyyy", null);
                DateTime FromDate = DateTime.ParseExact(from, "dd/MM/yyyy", null);
                ViewBag.FromDate = from;
                ViewBag.ToDate = to;
                phieunhaps = phieunhaps.Where(h => h.NgayLap >= FromDate && h.NgayLap <= ToDate).ToList();
            }
            //phieunhaps = phieunhaps.OrderByDescending(h => h.IdPhieuNhap).ToList();
            switch (sapxep ?? 0)
            {
                case 0:
                    phieunhaps = phieunhaps.OrderByDescending(h => h.IdPhieuNhap).ToList();
                    break;
                case 1:
                    phieunhaps = phieunhaps.OrderBy(h => h.IdPhieuNhap).ToList();
                    break;
                case 5:
                    break;
                default:
                    break;
            }
            
            var pageNumber = page ?? 1;
            ViewBag.OnePageOfPhieuNhap = phieunhaps.ToPagedList(pageNumber, hienthi ?? 10);
            ViewBag.HienThi = hienthi ?? 10;
            ViewBag.SapXep = sapxep ?? 0;
            ViewBag.ThongBao = TempData["ThongBao"];
            return View();
        }

        [HttpPost]
        public ActionResult ThemVaoGioHang(string masanpham,int soluong,int gianhap)
        {
            var sanpham = db.SanPhams.SingleOrDefault(s => s.MaSP== masanpham);
            if (sanpham != null)
            {
                CartSession.ThemVaoCartSession(this.HttpContext, sanpham, soluong,gianhap);
                return RedirectToAction("Index");
            }
            else
            {
                TempData["error"] = "Mã sản phẩm không tồn tại";
            }
            return RedirectToAction("Index");
        }

        public ActionResult XoaKhoiGioHang(int idsanpham)
        {
            var sanpham = db.SanPhams.SingleOrDefault(s => s.IdSanPham == idsanpham);
            CartSession.XoaKhoiCartSession(this.HttpContext, sanpham);
            return RedirectToAction("Index");
        }

        public ActionResult LuuPhieuNhap()
        {
            var cart = CartSession.GetCart(this.HttpContext);
            DateTime now = System.DateTime.Now;
            if (cart != null)
            {
                //User u = Session["admin"] as User;
                //var user = db.Users.SingleOrDefault(us => us.Id == u.Id);
                PhieuNhap phieunhap = new PhieuNhap { NgayLap=now};
                db.PhieuNhaps.Add(phieunhap);
                db.SaveChanges();
                ChiTietPhieuNhap(phieunhap);
                CartSession.XoaCartSession(this.HttpContext);
                return RedirectToAction("Index");
            }
            TempData["error"] = "Chưa chọn sản phẩm";
            return RedirectToAction("Index");
        }

        public void ChiTietPhieuNhap(PhieuNhap phieunhap)
        {
            if (CartSession.CartTonTai(this.HttpContext))
            {
                var cart = CartSession.GetCart(this.HttpContext);
                foreach (var item in cart.ListItem)
                {
                    var sp = db.SanPhams.SingleOrDefault(s => s.IdSanPham == item.SanPham.IdSanPham);
                    if (String.IsNullOrEmpty(item.Gia.ToString()) || item.Gia ==0)
                    {
                        ChiTietPhieuNhap ctpn = new ChiTietPhieuNhap
                        {
                            SoLuong = item.SoLuong,
                            ThanhTien = item.ThanhTien,
                            SanPham = sp,
                            GiaNhap = sp.Gia,
                            PhieuNhap = phieunhap
                        };
                        db.ChiTietPhieuNhaps.Add(ctpn);
                       
                    }
                    else
                    {
                        ChiTietPhieuNhap ctpn = new ChiTietPhieuNhap
                        {
                            SoLuong = item.SoLuong,
                            ThanhTien = item.ThanhTien,
                            SanPham = sp,
                            GiaNhap = item.Gia,
                            PhieuNhap = phieunhap
                        };
                        sp.Gia = item.Gia;
                        db.Entry(sp).State = EntityState.Modified;
                        db.SaveChanges();
                        db.ChiTietPhieuNhaps.Add(ctpn);
                    }
                   
                    XuLiTonKho(sp, item.SoLuong);
                }
            }
        }

        public void XuLiTonKho(SanPham sanpham, int soluong)
        {
            var tonkho = db.TonKhos.Where(t => t.IdSanPham == sanpham.IdSanPham).ToList().OrderByDescending(t => t.Id).FirstOrDefault();
            int soluongmoi = soluong;
            if (tonkho != null)
            {
                soluongmoi = tonkho.SoLuong + soluong;
            }
            TonKho tonkhomoi = new TonKho { SanPham = sanpham, SoLuong = soluongmoi, ThoiGian = System.DateTime.Now,Xuat=0,Nhap=soluong };
            db.TonKhos.Add(tonkhomoi);
            db.SaveChanges();
        }
    }
}
