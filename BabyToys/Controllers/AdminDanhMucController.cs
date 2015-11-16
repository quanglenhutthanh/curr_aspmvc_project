using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using BabyToys.Models;
using System.Data;
using System.Data.Entity;
namespace BabyToys.Controllers
{
    public class AdminDanhMucController : admin_baseController
    {
        //
        // GET: /AdminDanhMuc/
        DatabaseContext db = new DatabaseContext();
        #region Xai chung
        public List<DanhMucDropdownListItem> DropdownListDanhMuc(int IdDanhMucDaChon)
        {
            List<DanhMucDropdownListItem> danhmucs = new List<DanhMucDropdownListItem>();
            foreach (var danhmuc in db.DanhMucs.Where(d => d.DanhMucCha.IdDanhMuc == null))
            {
                danhmucs.Add(new DanhMucDropdownListItem { Value = danhmuc.IdDanhMuc, Text = danhmuc.TenDanhMuc });
                string prefix = "";
                LayDanhMuc(danhmucs, danhmuc.IdDanhMuc, IdDanhMucDaChon, prefix);
            }
            return danhmucs;
        }
        public void LayDanhMuc(List<DanhMucDropdownListItem> danhmucs, int id, int IdDanhMucDaChon, string prefix)
        {
            var dsdanhmuc = db.DanhMucs.Where(d => d.DanhMucCha.IdDanhMuc == id && d.IdDanhMuc != IdDanhMucDaChon);
            string tmp_prefix = prefix;
            if (dsdanhmuc != null)
            {
                foreach (var danhmuc in dsdanhmuc)
                {
                    if (danhmuc.DanhMucCha.IdDanhMuc != IdDanhMucDaChon)
                    {
                        string text = tmp_prefix + "----" + danhmuc.TenDanhMuc;
                        danhmucs.Add(new DanhMucDropdownListItem { Value = danhmuc.IdDanhMuc, Text = text });
                        prefix += "----";
                        LayDanhMuc(danhmucs, danhmuc.IdDanhMuc, IdDanhMucDaChon, prefix);
                    }
                }
            }
        }
        #endregion

        public ActionResult Index(int? id,string currentFilter, string searchString)
        {
            if (!Models.User.CurrentUser.Quyen.CoChucNang("xem_danh_muc"))
            {
                return RedirectToAction("Error", "Admin");
            }
            if (Models.User.CurrentUser.Quyen.CoChucNang("sua_danh_muc"))
            {
                ViewBag.Xoa = true;
            }
            var danhmucs = db.DanhMucs.ToList().Where(d => d.IdDanhMuc != 1);
            if (searchString == null)
            {
                searchString = currentFilter;
            }
            ViewBag.CurrentFilter = searchString;
            if (!String.IsNullOrEmpty(searchString))
            {
                danhmucs = danhmucs.Where(d => d.TenDanhMuc.ToLower().Contains(searchString.ToLower()));
            }
            return View(danhmucs);
        }
        
        public ActionResult CapNhatHienThi(int id, bool hienthi)
        {
            var danhmuc = db.DanhMucs.SingleOrDefault(s => s.IdDanhMuc == id);
            if (danhmuc != null)
            {
                danhmuc.IsActive = hienthi;
                db.Entry(danhmuc).State = EntityState.Modified;
                db.SaveChanges();
            }
            return View();
        }

        public ActionResult ThemMoi()
        {
            if (!Models.User.CurrentUser.Quyen.CoChucNang("them_danh_muc"))
            {
                return RedirectToAction("Error", "Admin");
            }
            ViewBag.IdDanhMucCha = new SelectList(DropdownListDanhMuc(0),"Value","Text");
            return View();
        }

        [HttpPost]
        public ActionResult ThemMoi(DanhMuc danhmuc,FormCollection collection,string luu)
        {
            if (ModelState.IsValid)
            {
                int idDanhMucCha = int.Parse(collection["IdDanhMucCha"]);
                var danhmuccha = db.DanhMucs.SingleOrDefault(d => d.IdDanhMuc == idDanhMucCha);
                danhmuc.DanhMucCha = danhmuccha;
                db.DanhMucs.Add(danhmuc);
                db.SaveChanges();
                if ("Lưu và đóng".Equals(luu, StringComparison.OrdinalIgnoreCase))
                {
                    return RedirectToAction("Index");
                }
                if ("Lưu và tạo mới".Equals(luu, StringComparison.OrdinalIgnoreCase))
                {
                    return RedirectToAction("ThemMoi");
                }
            }
            ViewBag.IdDanhMucCha = new SelectList(DropdownListDanhMuc(0), "Value", "Text");
            return View(danhmuc);
        }

        public ActionResult Sua(int id)
        {
            if (!Models.User.CurrentUser.Quyen.CoChucNang("xem_danh_muc"))
            {
                return RedirectToAction("Error", "Admin");
            }
            var danhmuc = db.DanhMucs.SingleOrDefault(d => d.IdDanhMuc == id);
            ViewBag.IdDanhMucCha = new SelectList(DropdownListDanhMuc(id), "Value", "Text",danhmuc.DanhMucCha.IdDanhMuc);
            return View(danhmuc);
        }

        [HttpPost]
        [ValidateInput(false)]
        public ActionResult Sua(DanhMuc danhmuc, FormCollection collection)
        {
            if (!Models.User.CurrentUser.Quyen.CoChucNang("sua_danh_muc"))
            {
                return RedirectToAction("Error", "Admin");
            }
            if (ModelState.IsValid)
            {
                db.Entry(danhmuc).State = EntityState.Modified;
                db.SaveChanges();

                int idDanhMucCha = int.Parse(collection["IdDanhMucCha"]);
                var danhmuccha = db.DanhMucs.SingleOrDefault(d => d.IdDanhMuc == idDanhMucCha);
                var dm = db.DanhMucs.SingleOrDefault(d => d.IdDanhMuc == danhmuc.IdDanhMuc);
                dm.DanhMucCha = danhmuccha;
                db.Entry(dm).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.IdDanhMucCha = new SelectList(DropdownListDanhMuc(danhmuc.IdDanhMuc), "Value", "Text",danhmuc.DanhMucCha.IdDanhMuc);
            return View(danhmuc);
        }
        #region Xoa (bo)
        public ActionResult Xoa(int id)
        {
            XoaDanhMuc(id);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        //[HttpPost]
        //public ActionResult XoaNhieuDanhMuc(int[] deleteInputs)
        //{
        //    if (deleteInputs != null)
        //    {
        //        foreach (var item in deleteInputs)
        //        {
        //            var danhmuc = db.DanhMucs.SingleOrDefault(d => d.IdDanhMuc == item);
        //            XoaDanhMuc(danhmuc.IdDanhMuc);
        //            db.SaveChanges();
        //            TempData["ThongBao"] = "<script>$( document ).ready(function(){ alert('Xóa thành công!') })</script>";
        //            return RedirectToAction("Index");
        //        }
        //    }
        //    TempData["ThongBao"] = "<script>$( document ).ready(function(){ alert('Bạn chưa chọn danh muc!') })</script>";
        //    return RedirectToAction("Index");
        //}
        public void XoaDanhMuc(int id)
        {
            var danhmuc = db.DanhMucs.SingleOrDefault(m => m.IdDanhMuc == id);
            var danhmuccons = db.DanhMucs.Where(p => p.DanhMucCha.IdDanhMuc == id);
            if (danhmuccons.Count() > 0)
            {
                foreach (var dm in danhmuccons)
                {
                    XoaDanhMuc(dm.IdDanhMuc);
                }
                //db.SaveChanges();
            }
            db.DanhMucs.Remove(danhmuc);
        }
        #endregion

        
    }
}
