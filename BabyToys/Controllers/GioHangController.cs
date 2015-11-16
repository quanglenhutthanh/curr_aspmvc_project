using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using BabyToys.Models;
using BabyToys.Models.ViewModel;
namespace BabyToys.Controllers
{
    public class GioHangController : Controller
    {
        //
        // GET: /GioHang/
        DatabaseContext db = new DatabaseContext();
        public ActionResult Index()
        {
     
            var cart = CartSession.GetCart(this.HttpContext);
            ViewBag.TongCong = cart.TongCong();
            return View(cart.ListItem);
        }
        
        public ActionResult ThemVaoGioHang(int idsanpham,int soluong)
        {
            var sanpham = db.SanPhams.SingleOrDefault(s => s.IdSanPham == idsanpham);
            CartSession.ThemVaoCartSession(this.HttpContext,sanpham, soluong,sanpham.GiaHienTai);
            return RedirectToAction("Index");
        }
        public ActionResult XoaKhoiGioHang(int idsanpham)
        {
            var sanpham = db.SanPhams.SingleOrDefault(s => s.IdSanPham == idsanpham);
            CartSession.XoaKhoiCartSession(this.HttpContext, sanpham);
            return RedirectToAction("Index");
        }

        public ActionResult XoaGioHang()
        {
            CartSession.XoaCartSession(this.HttpContext);
            return RedirectToAction("Index");
        }
        [HttpPost]
        public ActionResult CapNhatGioHang(FormCollection collection)
        {
            int id = int.Parse(collection["idsanpham"]);
            int soluong = int.Parse(collection["soluong"]);
            var sanpham = db.SanPhams.SingleOrDefault(s => s.IdSanPham == id);
            CartSession.CapNhatCartSession(this.HttpContext,sanpham, soluong);
            return RedirectToAction("Index");
        }
    }
}
