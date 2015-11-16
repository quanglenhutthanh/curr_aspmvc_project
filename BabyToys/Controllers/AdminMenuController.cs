using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using BabyToys.Models;
using System.Data.Entity;
namespace BabyToys.Controllers
{
    public class AdminMenuController : admin_baseController
    {
        //
        // GET: /AdminMenu/
        DatabaseContext db = new DatabaseContext();
        #region
        public List<DanhMucDropdownListItem> DropdownListDanhMuc()
        {
            List<DanhMucDropdownListItem> danhmucs = new List<DanhMucDropdownListItem>();
            foreach (var danhmuc in db.DanhMucs.Where(d => d.DanhMucCha.IdDanhMuc == null))
            {
                danhmucs.Add(new DanhMucDropdownListItem { Value = danhmuc.IdDanhMuc, Text = danhmuc.TenDanhMuc });
                string prefix = "";
                LayDanhMuc(danhmucs, danhmuc.IdDanhMuc, prefix);
            }
            return danhmucs;
        }
        public void LayDanhMuc(List<DanhMucDropdownListItem> danhmucs, int id, string prefix)
        {
            var dsdanhmuc = db.DanhMucs.Where(d => d.DanhMucCha.IdDanhMuc == id);
            string tmp_prefix = prefix;
            if (dsdanhmuc != null)
            {
                foreach (var danhmuc in dsdanhmuc)
                {
                    string text = tmp_prefix + "----" + danhmuc.TenDanhMuc;
                    danhmucs.Add(new DanhMucDropdownListItem { Value = danhmuc.IdDanhMuc, Text = text });
                    prefix += "----";
                    LayDanhMuc(danhmucs, danhmuc.IdDanhMuc, prefix);
                }
            }
        }
        #endregion

        public List<DanhMucDropdownListItem> DropdownListMenu(int IdDanhMucChon)
        {
            List<DanhMucDropdownListItem> menus = new List<DanhMucDropdownListItem>();
            var rootMenu = db.Menus.SingleOrDefault(m => m.Id == 16);
            menus.Add(new DanhMucDropdownListItem{Value = rootMenu.Id,Text=rootMenu.TieuDe});
            foreach (var menu in db.Menus.Where(d => d.ParentMenu.Id == 16).OrderBy(m => m.MenuOrder))
            {
                if (menu.Id != IdDanhMucChon)
                {
                    menus.Add(new DanhMucDropdownListItem { Value = menu.Id, Text = menu.TieuDe });
                }
                string prefix = "";
                LayMenu(menus, menu.Id, prefix,IdDanhMucChon);
            }
            return menus;
        }
        public void LayMenu(List<DanhMucDropdownListItem> menus, int id, string prefix,int IdDanhMucChon)
        {
            var dsmenu = db.Menus.Where(d => d.ParentMenu.Id == id);
            string tmp_prefix = prefix;
            if (dsmenu != null)
            {
                foreach (var menu in dsmenu)
                {
                    string text = tmp_prefix + "----" + menu.TieuDe;
                    if (menu.Id != IdDanhMucChon)
                    {
                        menus.Add(new DanhMucDropdownListItem { Value = menu.Id, Text = text });
                    }
                    prefix += "----";
                    LayMenu(menus, menu.Id, prefix,IdDanhMucChon);
                }
            }
        }
        public ActionResult Index()
        {
            if (!Models.User.CurrentUser.Quyen.CoChucNang("xem_menu"))
            {
                return RedirectToAction("Error", "Admin");
            }
            return View(db.Menus.Where(m => m.Id != 16).ToList());
        }

        public ActionResult ThemMoi()
        {
            if (!Models.User.CurrentUser.Quyen.CoChucNang("them_menu"))
            {
                return RedirectToAction("Error", "Admin");
            }
            ViewBag.IdDanhMuc = new SelectList(DropdownListDanhMuc(), "Value", "Text");
            ViewBag.IdParentMenu = new SelectList(DropdownListMenu(0), "Value", "Text");
            return View();
        }

        [HttpPost]
        [ValidateInput(false)]
        public ActionResult ThemMoi(FormCollection collection)
        {
            if (!Models.User.CurrentUser.Quyen.CoChucNang("them_menu"))
            {
                return RedirectToAction("Error", "Admin");
            }
            int type = 1;
            string TenMenu = collection["name"];
            type = int.Parse(collection["type"]);
            int ThuTu = int.Parse(collection["thutu"]);
            Menu menu = new Menu();
            menu.TieuDe = TenMenu;

            switch (type)
            {
                case 1:
                    {

                        int max_id=(db.Menus!=null?db.Menus.Select(m=>m.Id).Max():0)+1;
                        string NoiDung = collection["noidung"];
                        menu.type=1;
                        menu.NoiDung = NoiDung;
                        menu.link = "/home/baiviet/" + max_id;
                        break;
                    }
                case 2:
                    {
                        //int idChuyenMuc = 0;
                        int idChuyenMuc = int.Parse(collection["IdDanhMuc"]);
                        var danhmuc=db.DanhMucs.SingleOrDefault(d=>d.IdDanhMuc==idChuyenMuc);
                        menu.type = 2;
                        menu.link = "/danh-muc/"+danhmuc.IdDanhMuc+"/"+danhmuc.TenDanhMuc;
                        break;
                    }
                case 3:
                    {
                        string url = collection["url"];
                        menu.type = 3;
                        menu.link = url;
                        break;
                    }
            }
            int parentId = int.Parse(collection["IdParentMenu"]);
            var parentMenu = db.Menus.SingleOrDefault(m => m.Id == parentId);
            menu.ParentMenu = parentMenu;
            menu.MenuOrder = ThuTu;
            db.Menus.Add(menu);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        public ActionResult Sua(int id)
        {
            if (!Models.User.CurrentUser.Quyen.CoChucNang("xem_menu"))
            {
                return RedirectToAction("Error", "Admin");
            }
            var menu = db.Menus.SingleOrDefault(m => m.Id == id);
            Nullable<int> IdSelected = null;
            if (menu.ParentMenu != null)
            {
                IdSelected = menu.ParentMenu.Id;
            }
            ViewBag.IdParentMenu = new SelectList(DropdownListMenu(0), "Value", "Text",IdSelected);
            ViewBag.IdDanhMuc = new SelectList(DropdownListDanhMuc(), "Value", "Text",menu.danhmuc);
            return View(menu);
        }
        [HttpPost]
        [ValidateInput(false)]
        public ActionResult Sua(FormCollection collection)
        {
            if (!Models.User.CurrentUser.Quyen.CoChucNang("sua_menu"))
            {
                return RedirectToAction("Error", "Admin");
            }
            int type = 1;
            int id=int.Parse(collection["id"]);
            string TenMenu = collection["name"];
            int ThuTu = int.Parse(collection["thutu"]);
            type = int.Parse(collection["type"]);
            var menu = db.Menus.SingleOrDefault(m => m.Id == id);
            menu.TieuDe = TenMenu;
            switch (type)
            {
                case 1:
                    {
                        string NoiDung = collection["noidung"];
                        menu.type = 1;
                        menu.NoiDung = NoiDung;
                        menu.link = "/home/baiviet/" + id;
                        break;
                    }
                case 2:
                    {
                        //int idChuyenMuc = 0;
                        int idChuyenMuc = int.Parse(collection["IdDanhMuc"]);
                        var danhmuc = db.DanhMucs.SingleOrDefault(d => d.IdDanhMuc == idChuyenMuc);
                        menu.type = 2;
                        menu.danhmuc = danhmuc.IdDanhMuc;
                        menu.link = "/danh-muc/" + danhmuc.IdDanhMuc + "/" + danhmuc.TenDanhMuc;
                        break;
                    }
                case 3:
                    {
                        string url = collection["url"];
                        menu.type = 3;
                        menu.link = url;
                        break;
                    }
            }
            menu.MenuOrder = ThuTu;
            db.Entry(menu).State = EntityState.Modified;
            db.SaveChanges();
            int parentId = int.Parse(collection["IdParentMenu"]);
            var me = db.Menus.SingleOrDefault(m => m.Id == menu.Id);
            var parentMenu = db.Menus.SingleOrDefault(m => m.Id == parentId);
            me.ParentMenu = parentMenu;
            db.Entry(me).State = EntityState.Modified;
            db.SaveChanges();
            return RedirectToAction("Index");
        }
        
        public ActionResult Xoa(int id)
        {
            if (!Models.User.CurrentUser.Quyen.CoChucNang("sua_menu"))
            {
                return RedirectToAction("Error", "Admin");
            }
            var menu = db.Menus.SingleOrDefault(m => m.Id == id);
            db.Menus.Remove(menu);
            db.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}
