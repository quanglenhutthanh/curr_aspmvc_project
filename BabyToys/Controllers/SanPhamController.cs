using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using BabyToys.Models;
using PagedList;
using PagedList.Mvc;
using CaptchaMvc;
using CaptchaMvc.Attributes;
using CaptchaMvc.HtmlHelpers;
using CaptchaMvc.Models;
using BabyToys.Models.ViewModel;
namespace BabyToys.Controllers
{
    public class SanPhamController : Controller
    {
        //
        // GET: /SanPham/
        DatabaseContext db = new DatabaseContext();


       
        //load sp de quy theo danh muc
        public void LoadSanPham(int id, List<SanPham> sanphams)
        {
            var dsdanhmuc = db.DanhMucs.Where(d => d.DanhMucCha.IdDanhMuc == id);
            if (dsdanhmuc != null)
            {
                foreach (var danhmuc in dsdanhmuc)
                {
                    var dssanpham = db.SanPhams.Where(b => b.IdDanhMuc == danhmuc.IdDanhMuc);
                    if (dssanpham != null)
                    {
                        foreach (var bv in dssanpham)
                        {
                            sanphams.Add(bv);
                        }
                    }
                    LoadSanPham(danhmuc.IdDanhMuc, sanphams);
                }
            }
        }

        //trang chu
        public ActionResult Index(int? page, int? hienthi, int? sapxep)
        {
            var sanphams = db.SanPhams.Where(s=>s.HienThi==true).ToList();
            sanphams = sanphams.OrderByDescending(s => s.IdSanPham).ToList();
            switch (sapxep ?? 0)
            {
                case 0: break;
                case 1:
                    sanphams = sanphams.OrderBy(s => s.Gia).ToList();
                    break;
                case 2:
                    sanphams = sanphams.OrderByDescending(s => s.Gia).ToList();
                    break;
                case 3:
                    sanphams = sanphams.OrderBy(s => s.TenSP).ToList();
                    break;
                case 4:
                    sanphams = sanphams.OrderByDescending(s => s.TenSP).ToList();
                    break;
                default:
                    break;
            }
            var pageNumber = page ?? 1;
            ViewBag.onePageOfSanPham = sanphams.ToPagedList(pageNumber, hienthi ?? 9);
            ViewBag.HienThi = hienthi ?? 9;
            ViewBag.SapXep = sapxep ?? 0;

            return View();
        }
        
        public ActionResult SanPhamTheoDanhMuc(int id, int? page, int? hienthi,int? sapxep)
        {
            var danhmuc = db.DanhMucs.SingleOrDefault(d => d.IdDanhMuc == id);
            ViewBag.Id = danhmuc.IdDanhMuc;
            ViewBag.Ten = danhmuc.TenDanhMuc;
            var sanphams = db.SanPhams.Where(s => s.IdDanhMuc == id && s.HienThi==true).ToList();
            LoadSanPham(id, sanphams);
            sanphams = sanphams.OrderByDescending(s => s.IdSanPham).ToList();
            switch (sapxep ?? 0)
            {
                case 0: break;
                case 1:
                    sanphams = sanphams.OrderBy(s => s.Gia).ToList();
                    break;
                case 2:
                    sanphams = sanphams.OrderByDescending(s => s.Gia).ToList();
                    break;
                case 3:
                    sanphams = sanphams.OrderBy(s => s.TenSP).ToList();
                    break;
                case 4:
                    sanphams = sanphams.OrderByDescending(s => s.TenSP).ToList();
                    break;
                default:
                    break;
            }
            var pageNumber = page ?? 1;
            ViewBag.onePageOfSanPham = sanphams.ToPagedList(pageNumber, hienthi ?? 9);
            ViewBag.HienThi = hienthi ?? 9;
            ViewBag.SapXep = sapxep ?? 0;
            
            return View(danhmuc);
        }

        public ActionResult SanPhamTheoThuongHieu(int id, int? page, int? hienthi, int? sapxep)
        {
            var thuonghieu = db.ThuongHieus.SingleOrDefault(t => t.IdThuongHieu == id);
            var sanphams = db.SanPhams.Where(s => s.IdThuongHieu == id && s.HienThi==true).ToList();
            sanphams = sanphams.OrderByDescending(s => s.IdSanPham).ToList();
            switch (sapxep ?? 0)
            {
                case 0: break;
                case 1:
                    sanphams = sanphams.OrderBy(s => s.Gia).ToList();
                    break;
                case 2:
                    sanphams = sanphams.OrderByDescending(s => s.Gia).ToList();
                    break;
                case 3:
                    sanphams = sanphams.OrderBy(s => s.TenSP).ToList();
                    break;
                case 4:
                    sanphams = sanphams.OrderByDescending(s => s.TenSP).ToList();
                    break;
                default:
                    break;
            }
            var pageNumber = page ?? 1;
            ViewBag.onePageOfSanPham = sanphams.ToPagedList(pageNumber, hienthi ?? 9);
            ViewBag.HienThi = hienthi ?? 9;
            ViewBag.SapXep = sapxep ?? 0;
            ViewBag.id = id;
            return View(thuonghieu);
        }
        public ActionResult ChiTiet(int id=0)
        {
            var sanpham = db.SanPhams.SingleOrDefault(s => s.IdSanPham == id);
            ViewBag.ChiTietHinh = db.HinhAnhs.ToList().Where(h => h.SanPham == sanpham);
            return View(sanpham);
        }
        [ChildActionOnly]
        public ActionResult DanhGia(int id)
        {
            var sanpham = db.SanPhams.SingleOrDefault(s => s.IdSanPham == id);
            DanhGia danhgia = new DanhGia();
            danhgia.IdSanPham = sanpham.IdSanPham;
            return PartialView(danhgia);
        }
        [HttpPost]
        [ValidateInput(false)]
        public ActionResult DanhGia(DanhGia danhgia)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    if (this.IsCaptchaValid("Captcha is not valid"))
                    {
                        var sanpham = db.SanPhams.SingleOrDefault(s => s.IdSanPham == danhgia.IdSanPham);
                        danhgia.ThoiGian = System.DateTime.Now;
                        db.DanhGia.Add(danhgia);
                        db.SaveChanges();
                        Response.Redirect("/san-pham/" + danhgia.IdSanPham + "/" + Utilities.EditString.BoDauTrenChuoi(sanpham.TenSP));
                    }
                    else
                    {
                        ViewBag.CaptchaError = "Mã xác nhận không đúng";
                    }
                }
            }
            catch
            {
                ViewBag.CaptchaError = "Mã xác nhận không đúng";
            }
            return PartialView(danhgia);
        }
        
        
    }
}
