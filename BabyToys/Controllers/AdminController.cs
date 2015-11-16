using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using BabyToys.Models;
namespace BabyToys.Controllers
{
    public class AdminController : Controller
    {
        //
        // GET: /Admin/
        DatabaseContext db = new DatabaseContext();
        public ActionResult Index()
        {
            User user = Session["admin"] as User;
            ViewBag.Name = user.TenDangNhap;
            ViewBag.Role = user.Quyen.TenQuyen;
            ViewBag.per = user.Quyen.CoChucNang("xoa_san_pham");
            ViewBag.doanhso = (db.HoaDons.ToList().Select(t => t.TongGiaTri)).Sum();
            ViewBag.tongsanpham = db.SanPhams.ToList().Count;
            ViewBag.tongkhachhang = db.KhachHangs.ToList().Count;
            ViewBag.tongdonhang = db.HoaDons.ToList().Count;
            ViewBag.chuaxuli = db.HoaDons.Where(h => h.TrangThai == 1).ToList().Count();
            DateTime ngay=System.DateTime.Now.AddDays(-1);
            ViewBag.hoadontrongngay = db.HoaDons.Where(h => h.NgayLapHD >= ngay).ToList().Count;
            ViewBag.khachhangmoi = db.KhachHangs.Where(h => h.NgayTao >= ngay).ToList().Count;
            return View();
        }
        public ActionResult Error()
        {
            return View();
        }
        [NonAction]
        protected override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            if (Session["admin"] == null)
            {
                filterContext.Result = new RedirectResult("~/AdminLogin");
            }
            base.OnActionExecuting(filterContext);
        }
    }
}
