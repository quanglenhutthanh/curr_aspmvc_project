using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using BabyToys.Models;
using CaptchaMvc;
using CaptchaMvc.Attributes;
using CaptchaMvc.HtmlHelpers;
using CaptchaMvc.Models;
namespace BabyToys.Controllers
{
    public class HomeController : Controller
    {
        //
        // GET: /Home/
        DatabaseContext db = new DatabaseContext();
        public ActionResult Index()
        {
            int sl = int.Parse(BabyToys.Utilities.EditString.GetTextByKey("dsktSLSanpham"));
            var sanphams = db.SanPhams.Where(s=>s.HienThi==true).ToList().OrderByDescending(s => s.IdSanPham).Take(sl);
            return View(sanphams);
        }
        public ActionResult baiviet(int id)
        {
            var menu = db.Menus.SingleOrDefault(m => m.Id == id);
            ViewBag.NoiDung = "";
            if (menu != null)
            {
                ViewBag.TieuDe = menu.TieuDe;
                ViewBag.NoiDung = menu.NoiDung;
            }
            return View();
        }
        public ActionResult phivanchuyen()
        {
            var tinh = db.TinhThanhs.ToList();
            return View(tinh);
        }
        [ChildActionOnly]
        public ActionResult navigation()
        {            //ViewBag.DanhMuc = db.DanhMucs.ToList();
            return PartialView(db.Menus.ToList());
        }

        [ChildActionOnly]
        public ActionResult ModuleDanhMuc()
        {
            return PartialView(db.DanhMucs.Where(d=>d.IsActive==true).ToList());
        }

        [ChildActionOnly]
        public ActionResult ModuleThuongHieu()
        {
            return PartialView(db.Mails.ToList());
        }
        [ChildActionOnly]
        public ActionResult ModuleTaiKhoan()
        {
            return PartialView();
        }
        [ChildActionOnly]
        public ActionResult ModuleSanPhamNoiBat()
        {
            return PartialView(db.SanPhams.ToList().OrderByDescending(s=>s.DiemDanhGiaTrungBinh()).Take(8));
        }

        [ChildActionOnly]
        public ActionResult ModuleSanPhamNoiBatTheoDanhMuc(int id)
        {
            var sanphams = db.SanPhams.Where(s => s.IdDanhMuc == id).ToList();
            LoadSanPham(id, sanphams);
            return PartialView(sanphams.OrderByDescending(s=>s.DiemDanhGiaTrungBinh()).Take(5));
        }
        //load sp theo danh muc
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
        [ChildActionOnly]
        public ActionResult ModuleSanPhamNoiBatTheoThuongHieu(int id)
        {
            return PartialView(db.SanPhams.Where(t=>t.IdThuongHieu==id).ToList().OrderByDescending(s => s.DanhGia).Take(5));
        }
        public ActionResult LienHe()
        {
            return View();
        }

        [HttpPost]
        [ValidateInput(false)]
        public ActionResult LienHe(DanhGia danhgia)
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
                        ViewBag.Success = "Chúng tôi đã nhận được thông tin của bạn và sẽ gửi phản hồi lại cho bạn sớm nhất có thể!";
                        return View();
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
            return View(danhgia);
        }
    }
}
