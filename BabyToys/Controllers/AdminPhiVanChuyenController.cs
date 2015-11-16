using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using BabyToys.Models;
using System.Data.Entity.Validation;
using System.Data.Entity;

namespace BabyToys.Controllers
{
    public class AdminPhiVanChuyenController : admin_baseController
    {
        //
        // GET: /AdminPhiVanChuyen/

        public ActionResult Index()
        {
            if (!Models.User.CurrentUser.Quyen.CoChucNang("xem_phi_van_chuyen"))
            {
                return RedirectToAction("Error", "Admin");
            }
            if (Models.User.CurrentUser.Quyen.CoChucNang("sua_phi_van_chuyen"))
            {
                ViewBag.Xoa = true;
            }
            return RedirectToAction("QLPhiVanChuyen");
        }
        DatabaseContext db = new DatabaseContext();
        public ActionResult XemPhiVanChuyen()
        {

            var tinh = db.TinhThanhs.ToList();
            return View(tinh);
        }
        public ActionResult QLPhiVanChuyen()
        {
            if (!Models.User.CurrentUser.Quyen.CoChucNang("xem_phi_van_chuyen"))
            {
                return RedirectToAction("Error", "Admin");
            }
            if (Models.User.CurrentUser.Quyen.CoChucNang("sua_phi_van_chuyen"))
            {
                ViewBag.Xoa = true;
            }
            var tinh = db.TinhThanhs.ToList();
            return View(tinh);
        }
        public ActionResult ThemPhiVC()
        {
            if (!Models.User.CurrentUser.Quyen.CoChucNang("them_phi_van_chuyen"))
            {
                return RedirectToAction("Error", "Admin");
            }
            return View();
        }

        [HttpPost]
        public ActionResult ThemPhiVC(TinhThanh tinh)
        {
            if (!Models.User.CurrentUser.Quyen.CoChucNang("them_phi_van_chuyen"))
            {
                return RedirectToAction("Error", "Admin");
            }
            if (ModelState.IsValid)
            {
                db.TinhThanhs.Add(tinh);
                db.SaveChanges();
                return RedirectToAction("QLPhiVanChuyen");
            }
            return View();
        }
        public ActionResult SuaPhiVC(int id)
        {
            if (!Models.User.CurrentUser.Quyen.CoChucNang("xem_phi_van_chuyen"))
            {
                return RedirectToAction("Error", "Admin");
            }
            var tinh = db.TinhThanhs.SingleOrDefault(t => t.IdTinhThanh == id);
            return View(tinh);
        }
        [HttpPost]
        public ActionResult SuaPhiVC(TinhThanh tinh)
        {
            if (!Models.User.CurrentUser.Quyen.CoChucNang("sua_phi_van_chuyen"))
            {
                return RedirectToAction("Error", "Admin");
            }
            try
            {
                if (ModelState.IsValid)
            {
                db.Entry(tinh).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("QLPhiVanChuyen");
            }
            }
            catch (Exception ex)
            {

            }
            return View();
        }

        public ActionResult XoaPhiVC(int id)
        {
            if (!Models.User.CurrentUser.Quyen.CoChucNang("sua_phi_van_chuyen"))
            {
                return RedirectToAction("Error", "Admin");
            }
            try
            {
                var tinh = db.TinhThanhs.SingleOrDefault(t => t.IdTinhThanh == id);
                db.TinhThanhs.Remove(tinh);
                db.SaveChanges();
            }
            catch
            {
                return RedirectToAction("QLPhiVanChuyen");
            }
            return RedirectToAction("QLPhiVanChuyen");
        }
    }
}
