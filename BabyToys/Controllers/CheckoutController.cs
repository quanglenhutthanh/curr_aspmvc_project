using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using BabyToys.Models;
using BabyToys.Models.ViewModel;
using System.Data;
using System.IO;
using System.Data.Entity;
namespace BabyToys.Controllers
{
    public class CheckoutController : Controller
    {
        //
        // GET: /Checkout/
        DatabaseContext db = new DatabaseContext();

        public ActionResult Success()
        {
            return View();
        }
        public ActionResult Index()
        {
            if (CartSession.CartTonTai(this.HttpContext))
            {
                var cart = CartSession.GetCart(this.HttpContext);
                //if (cart.ListItem.Any(c => c.VuotSoLuong == true))
                //{
                //    return RedirectToAction("Index", "GioHang");
                //}
                ViewBag.TongCong = cart.TongCong();
                ViewBag.Cart = cart;
                //KhachHang khachhang = Session["khachhang"] as KhachHang;
                //if (khachhang != null)
                //{
                //    var tinh = db.TinhThanhs.SingleOrDefault(t => t.IdTinhThanh == khachhang.IdTinhThanh);

                //    ViewBag.IsLogin = 1;
                //    ViewBag.DiaChi = khachhang.DiaChi;
                //    ViewBag.Tinh = tinh.TenTinh;
                //    ViewBag.PhiVC = tinh.PhiVanChuyen;
                //}
                return View();
            }
            return RedirectToAction("Index", "GioHang");
        }

        [HttpPost]
        public ActionResult XuLi(FormCollection collection)
        {
            int phivanchuyen = 0;
            HoaDon hoadon = new HoaDon
            {
                PhiVanChuyen = phivanchuyen,
                TienKhuyenMai = 0,
                TenNguoiNhan = collection["guest_ten"],
                Email = collection["guest_email"],
                SoDienThoai = collection["guest_SDT"],
                DiaChiGiaoHang = collection["guest_diachi"],
                TrangThai = 1,
                NgayLapHD = System.DateTime.Now,
                TienHang = CartSession.TongTien(this.HttpContext)
            };
            //if (idtinhthanh > 0)
            //{
            //    var tinhthanh = db.TinhThanhs.SingleOrDefault(t => t.IdTinhThanh == idtinhthanh);
            //    hoadon.PhiVanChuyen = tinhthanh.PhiVanChuyen;
            //    hoadon.TinhThanh = tinhthanh;
            //}
            //KhachHang khachhang = Session["khachhang"] as KhachHang;

            //if (khachhang != null)
            //{
            //    var kh = db.KhachHangs.SingleOrDefault(k => k.IdKhachHang == khachhang.IdKhachHang);
            //    hoadon.IdKhachHang = khachhang.IdKhachHang;
            //    if (type == "DCDaCo")
            //    {
            //        TinhThanh tinh = db.TinhThanhs.SingleOrDefault(t => t.IdTinhThanh == kh.TinhThanh.IdTinhThanh);
            //        email = kh.Email;
            //        hoadon.DiaChiGiaoHang = kh.DiaChi;
            //        hoadon.Email = kh.Email;
            //        hoadon.TenNguoiNhan = kh.TenDayDu;
            //        hoadon.SoDienThoai = kh.SoDienThoai;
            //        hoadon.PhiVanChuyen = tinh.PhiVanChuyen;
            //        hoadon.TinhThanh = tinh;
            //        hoadon.TienKhuyenMai = CartSession.TongTien(this.HttpContext) * kh.LoaiKhachHang.GiamGia / 100;
            //    }
            //}

            db.HoaDons.Add(hoadon);
            db.SaveChanges();
            LuuChiTietHoaDon(hoadon);
            //IUserMailer mailer = new UserMailer();
            //try
            //{
            //    mailer.Order(hoadon, email).Send();
            //}
            //catch (Exception ex)
            //{
            //    Console.WriteLine(ex.ToString());
            //}
            CartSession.XoaCartSession(this.HttpContext);
            //if (khachhang != null)
            //{
            //    int diem = (int)((float)hoadon.TongGiaTri * 0.0002);
            //    TichLuyDiem(khachhang, diem, hoadon);
            //}
            return RedirectToAction("Success","Checkout");
        }
        public void TichLuyDiem(KhachHang khachhang, int diem, HoaDon hoadon)
        {
            var kh = db.KhachHangs.SingleOrDefault(k => k.IdKhachHang == khachhang.IdKhachHang);
            var hd = db.HoaDons.SingleOrDefault(h => h.IdHoaDon == hoadon.IdHoaDon);
            DiemTichLuy diemmoi = new DiemTichLuy
            {
                Diem = diem,
                khachhang = kh,
                hoadon = hd,
                ThoiGian = System.DateTime.Now
            };
            db.DiemTichLuy.Add(diemmoi);
            db.SaveChanges();
            KiemTraLoaiKhachHang(kh);
        }

        public void KiemTraLoaiKhachHang(KhachHang khachhang)
        {
            var kh = db.KhachHangs.SingleOrDefault(k => k.IdKhachHang == khachhang.IdKhachHang);
            foreach (var loai in db.LoaiKhachHangs.ToList())
            {
                if (kh.TongDiemTichLuy() >= loai.Diem)
                {
                    kh.IdLoaiKhachHang = loai.IdLoaiKhachHang;
                }
            }
            db.Entry(kh).State = EntityState.Modified;
            db.SaveChanges();
        }

        public void LuuChiTietHoaDon(HoaDon hoadon)
        {
            if (CartSession.CartTonTai(this.HttpContext))
            {
                var cart = CartSession.GetCart(this.HttpContext);
                foreach (var item in cart.ListItem)
                {
                    var sp = db.SanPhams.SingleOrDefault(s => s.IdSanPham == item.SanPham.IdSanPham);
                    ChiTietHoaDon cthd = new ChiTietHoaDon
                    {
                        SoLuong = item.SoLuong,
                        ThanhTien = item.ThanhTien,
                        SanPham = sp,
                        HoaDon = hoadon
                    };
                    db.ChiTietHoaDon.Add(cthd);
                    db.SaveChanges();
                    //XuLiTonKho(sp, item.SoLuong);
                }
            }
        }

        public void XuLiTonKho(SanPham sanpham, int soluong)
        {
            var tonkho = db.TonKhos.Where(t => t.IdSanPham == sanpham.IdSanPham).ToList().OrderByDescending(t => t.Id).FirstOrDefault();
            //int soluongmoi = soluong;
            if (tonkho != null)
            {
                int soluongmoi = tonkho.SoLuong - soluong;
                TonKho tonkhomoi = new TonKho { SanPham = sanpham, SoLuong = soluongmoi, ThoiGian = System.DateTime.Now, Nhap = 0, Xuat = soluong };
                db.TonKhos.Add(tonkhomoi);
                db.SaveChanges();
            }

        }
        [ChildActionOnly]
        public ActionResult ModuleThongTinGiaoHang1(HoaDon hoadon)
        {
            ViewBag.tinhthanh = db.TinhThanhs.ToList();
            return PartialView();
        }
        [ChildActionOnly]
        public ActionResult ModuleThongTinGiaoHang2(string DiaChi, string Tinh, int PhiVC)
        {
            //ViewBag.IdTinhThanh = new SelectList(db.TinhThanhs.ToList(), "IdTinhThanh", "TenTinh");
            ViewBag.tinhthanh = db.TinhThanhs.ToList();
            KhachHang khachhang = Session["khachhang"] as KhachHang;
            ViewBag.PhiVC = PhiVC;
            ViewBag.Tinh = Tinh;
            ViewBag.DiaChi = DiaChi;
            return PartialView();
        }


    }
}
