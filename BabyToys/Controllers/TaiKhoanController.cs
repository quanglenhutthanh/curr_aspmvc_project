using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using CaptchaMvc;
using CaptchaMvc.Attributes;
using CaptchaMvc.HtmlHelpers;
using CaptchaMvc.Models;
using BabyToys.Models.ViewModel;
using BabyToys.Models;
using System.Data;
using System.Web.Security;
using System.Data.Entity;
namespace BabyToys.Controllers
{
    public class TaiKhoanController : Controller
    {
        //
        // GET: /TaiKhoan/
        DatabaseContext db = new DatabaseContext();
        public ActionResult Index()
        {
            KhachHang khachhang = Session["khachhang"] as KhachHang;
            if (khachhang == null)
            {
                return RedirectToAction("DangNhap");
            }
            return View();
        }
        public ActionResult DangNhap()
        {
            return View();
        }

        [ChildActionOnly]
        public ActionResult pDangNhap(string returnUrl)
        {
            LoginModel login = new LoginModel { returnUrl=returnUrl};
            return PartialView();
        }
        [HttpPost]
        public ActionResult pDangNhap(LoginModel login)
        {
            if (ModelState.IsValid)
            {
                string matkhau = Utilities.EditString.mahoa_md5(login.PassWord);
                var user = db.KhachHangs.Where(u => u.TenDangNhap == login.LoginName &&
                                                  u.MatKhau == matkhau); 
                
                if (user != null && user.Count() == 1)
                {
                    KhachHang u = user.First();
                    if (u.IsActive == false)
                    {
                        ViewBag.error = "Tài khoản này đã bị khóa!";
                        return PartialView(login);
                    }
                    Session["khachhang"] = u;
                    FormsAuthentication.SetAuthCookie(login.LoginName, login.RememberMe);
                    Response.Redirect(login.returnUrl);
                }
                else
                {
                    ViewBag.error = "Tên đăng nhập hoặc mật khẩu không đúng!";
                    return PartialView(login);
                }
            }
            return PartialView(login);
        }
        public ActionResult DangKy()
        {
            ViewBag.IdTinhThanh = new SelectList(db.TinhThanhs.ToList(), "IdTinhThanh", "TenTinh");
            return View();
        }
        [HttpPost]
        public ActionResult DangKy(KhachHangViewModel khachhang)
        {
            if (ModelState.IsValid)
            {
                if (this.IsCaptchaValid("Captcha is not valid"))
                {
                    if (db.KhachHangs.Any(k => k.TenDangNhap == khachhang.TenDangNhap))
                    {
                        ViewBag.error = "Tên đăng nhập đã tồn tại";
                        ViewBag.IdTinhThanh = new SelectList(db.TinhThanhs.ToList(), "IdTinhThanh", "TenTinh");
                        return View(khachhang);   
                    }
                    if (khachhang.TenDangNhap.ToLower().Contains("admin") 
                        || khachhang.TenDangNhap.ToLower().Contains("babytoys") 
                        || khachhang.TenDangNhap.ToLower().Contains("quản trị"))
                    {
                        ViewBag.error = "Tên không được sử dụng nhữn từ như như: admin,quản lí,babytoys";
                        ViewBag.IdTinhThanh = new SelectList(db.TinhThanhs.ToList(), "IdTinhThanh", "TenTinh");
                        return View(khachhang);
                    }
                    khachhang.IdLoaiKhachHang = 1;
                    khachhang.IsActive = true;
                    BabyToys.DAL.RegisterDAL reg = new DAL.RegisterDAL();
                    reg.ThemKhachHang(khachhang);
                    var kh = db.KhachHangs.SingleOrDefault(k => k.TenDangNhap == khachhang.TenDangNhap);
                    Session["khachhang"] = kh;
                    return RedirectToAction("DangKiThanhCong"); 
                }
                else
                {
                    ViewBag.CaptchaError = "Mã xác nhận không đúng";
                }
            }
            ViewBag.IdTinhThanh = new SelectList(db.TinhThanhs.ToList(), "IdTinhThanh", "TenTinh");
            return View(khachhang);
        }
        public ActionResult DangXuat()
        {
            Session.Clear();
            return RedirectToAction("index");
        }
        public ActionResult DangNhapThanhCong(string returnUrl)
        {
            ViewBag.returnurl = returnUrl;
            return View();
        }

        public ActionResult DangKiThanhCong()
        {
            return View();
        }
        public ActionResult DoiMatKhau()
        {
            KhachHang khachhang = Session["khachhang"] as KhachHang;
            if (khachhang == null)
            {
                return RedirectToAction("DangNhap");
            }
            var kh = db.KhachHangs.SingleOrDefault(k => k.IdKhachHang == khachhang.IdKhachHang);
            return View(kh);
        }

        [HttpPost]
        public ActionResult DoiMatKhau(KhachHang khachhang,FormCollection collection)
        {
            string oldpass = collection["oldpass"];
            oldpass= Utilities.EditString.mahoa_md5(oldpass);
            string newpass = collection["newpass"];
            string confirm = collection["confirm"];
            var kh = db.KhachHangs.SingleOrDefault(k => k.IdKhachHang == khachhang.IdKhachHang && k.MatKhau ==oldpass);
            if (kh != null)
            {
                kh.MatKhau = Utilities.EditString.mahoa_md5(newpass);
                db.Entry(kh).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            else
            {
                ViewBag.thongbao = "Mật khẩu cũ không đúng!";
            }
            return View(khachhang);
        }

        //public ActionResult QuenMatKhau()
        //{
        //    return View();
        //}

        //[HttpPost]
        //public ActionResult QuenMatKhau(string TenDangNhap,string Email)
        //{
        //    var khachhang=db.KhachHangs.SingleOrDefault(k=>k.TenDangNhap==TenDangNhap);
        //    if (khachhang != null)
        //    {
        //        if (khachhang.Email != Email)
        //        {
        //            ViewBag.thongbao = "Email không đúng với email đã đăng kí";
        //            ViewBag.tendangnhap = TenDangNhap;
        //            ViewBag.email = Email;
        //            return View();
        //        }
        //        IUserMailer mailer = new UserMailer();
        //        Guid newPass = Guid.NewGuid();
        //        string passToSend = newPass.ToString().Substring(0, 8);
        //        try
        //        {
        //            mailer.PasswordReset(Email, passToSend).Send();
        //        }
        //        catch {
        //            ViewBag.thongbao = "Không gửi được mail";
        //            ViewBag.tendangnhap = TenDangNhap;
        //            ViewBag.email = Email;
        //            return View();
        //        }
        //        khachhang.MatKhau=Utilities.EditString.mahoa_md5(passToSend);
        //        db.Entry(khachhang).State = EntityState.Modified;
        //        db.SaveChanges();
        //        return RedirectToAction("KhoiPhucMatKhauThanhCong");
        //    }
        //    else
        //    {
        //        ViewBag.thongbao = "Tài khoản không tồn tại";
        //    }
        //    ViewBag.tendangnhap = TenDangNhap;
        //    ViewBag.email = Email;
        //    return View();
        //}

        public ActionResult KhoiPhucMatKhauThanhCong(string TenDangNhap,string Email)
        {
            KhachHang khachhang = Session["khachhang"] as KhachHang;
            if (khachhang != null)
            {
                return RedirectToAction("DangNhap");
            }
            return View();
        }
        public ActionResult ThayDOiThongTinCaNhan()
        {
            KhachHang khachhang = Session["khachhang"] as KhachHang;
            if (khachhang == null)
            {
                return RedirectToAction("DangNhap");
            }
            ViewBag.IdTinhThanh = new SelectList(db.TinhThanhs.ToList(), "IdTinhThanh", "TenTinh");
            var kh = db.KhachHangs.SingleOrDefault(k => k.IdKhachHang == khachhang.IdKhachHang);
            return View(kh);
        }
        [HttpPost]
        public ActionResult ThayDOiThongTinCaNhan(KhachHang khachhang)
        {
            if (khachhang == null)
            {
                return RedirectToAction("DangNhap");
            }
            if (ModelState.IsValid)
            {
                db.Entry(khachhang).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.IdTinhThanh = new SelectList(db.TinhThanhs.ToList(),"IdTinhThanh","TenTinh");
            return View();
        }
        public ActionResult XemDiemTichLuy()
        {
            KhachHang khachhang = Session["khachhang"] as KhachHang;
            if (khachhang == null)
            {
                return RedirectToAction("DangNhap");
            }
            var kh = db.KhachHangs.SingleOrDefault(k => k.IdKhachHang == khachhang.IdKhachHang);
            return View(kh);
        }
        public ActionResult LichSuDonHang()
        {
            KhachHang khachhang = Session["khachhang"] as KhachHang;
            if (khachhang == null)
            {
                return RedirectToAction("DangNhap");
            }
            var kh = db.KhachHangs.SingleOrDefault(k => k.IdKhachHang == khachhang.IdKhachHang);
            return View(kh);
        }
    }
}
