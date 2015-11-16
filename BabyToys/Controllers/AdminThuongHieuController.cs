using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using BabyToys.Models;
using PagedList;
using PagedList.Mvc;
using System.Web.Helpers;
using System.IO;
using System.Data;
using System.Data.Entity;
namespace BabyToys.Controllers
{
    public class AdminThuongHieuController : admin_baseController
    {
        //
        // GET: /AdminThuongHieu/
        DatabaseContext db = new DatabaseContext();
        public ActionResult Index(int? page, string currentFilter, string searchString)
        {
            if (!Models.User.CurrentUser.Quyen.CoChucNang("xem_thuong_hieu"))
            {
                return RedirectToAction("Error", "Admin");
            }
            if (Models.User.CurrentUser.Quyen.CoChucNang("sua_thuong_hieu"))
            {
                ViewBag.Xoa = true;
            }
            var pageNumber = page ?? 1;
            var thuonghieus = db.ThuongHieus.ToList();
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
                thuonghieus = thuonghieus.Where(t => t.TenThuongHieu.ToLower().Contains(searchString.ToLower())).ToList();
            }
            ViewBag.onePageOfThuongHieu = thuonghieus.ToPagedList(pageNumber, 10);
            return View();
        }
        public ActionResult CapNhatHienThi(int id, bool hienthi)
        {
            var thuonghieu = db.ThuongHieus.SingleOrDefault(s => s.IdThuongHieu == id);
            if (thuonghieu != null)
            {
                thuonghieu.IsActive = hienthi;
                db.Entry(thuonghieu).State = EntityState.Modified;
                db.SaveChanges();
            }
            return View();
        }
        public ActionResult ThemThuongHieu()
        {
            if (!Models.User.CurrentUser.Quyen.CoChucNang("them_thuong_hieu"))
            {
                return RedirectToAction("Error", "Admin");
            }
            return View();
        }
    
        [HttpPost]
        [ValidateInput(false)]
        public ActionResult ThemThuongHieu(ThuongHieu thuonghieu,string luu)
        {
            if (ModelState.IsValid)
            {
                var image = WebImage.GetImageFromRequest("anhdaidien");
                if (image != null)
                {
                    string ImageName = Path.GetFileName(image.FileName);
                    ImageName = "main" + DateTime.Now.Year + DateTime.Now.Month + DateTime.Now.Day +
                        DateTime.Now.Hour + DateTime.Now.Minute + DateTime.Now.Second + "-" + ImageName;
                    var pathToSave = Path.Combine(Server.MapPath("~/Content/Images/ThuongHieu"), ImageName);
                    image.Save(pathToSave);
                    thuonghieu.Logo = ImageName;
                }
                else
                {
                    thuonghieu.Logo="noimageyet.jpg";
                }
                db.ThuongHieus.Add(thuonghieu);
                db.SaveChanges();
                if ("Lưu và đóng".Equals(luu, StringComparison.OrdinalIgnoreCase))
                {
                    return RedirectToAction("Index");
                }
                if ("Lưu và tạo mới".Equals(luu, StringComparison.OrdinalIgnoreCase))
                {
                    return RedirectToAction("ThemThuongHieu");
                }
            }
            return View(thuonghieu);
        }

        public ActionResult SuaThuongHieu(int id)
        {
            if (!Models.User.CurrentUser.Quyen.CoChucNang("xem_thuong_hieu"))
            {
                return RedirectToAction("Error", "Admin");
            }
            var thuonghieu = db.ThuongHieus.SingleOrDefault(t => t.IdThuongHieu == id);
            return View(thuonghieu);
        }

        [HttpPost]
        [ValidateInput(false)]
        public ActionResult SuaThuongHieu(ThuongHieu thuonghieu)
        {
            if (ModelState.IsValid)
            {
                //var image = WebImage.GetImageFromRequest("anhdaidien");
                var image = WebImage.GetImageFromRequest("anhdaidien");
                if (image != null)
                {
                    string ImageName = Path.GetFileName(image.FileName);
                    ImageName = "main" + DateTime.Now.Year + DateTime.Now.Month + DateTime.Now.Day +
                        DateTime.Now.Hour + DateTime.Now.Minute + DateTime.Now.Second + "-" + ImageName;
                    var pathToSave = Path.Combine(Server.MapPath("~/Content/Images/ThuongHieu"), ImageName);
                    image.Save(pathToSave);
                    thuonghieu.Logo = ImageName;
                }
                db.Entry(thuonghieu).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(thuonghieu);
        }

    }
}
