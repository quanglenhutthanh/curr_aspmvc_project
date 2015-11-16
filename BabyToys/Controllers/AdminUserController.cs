using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using BabyToys.Models;
using PagedList.Mvc;
using PagedList;
using System.Data;
using BabyToys.Models.ViewModel;
using System.Data.Entity;
namespace BabyToys.Controllers
{
    public class AdminUserController : admin_baseController
    {
        //
        // GET: /User/
        DatabaseContext db = new DatabaseContext();
        #region 1.User
        public ActionResult Index(int? page)
        {
            //if (!Models.User.CurrentUser.Quyen.CoChucNang("xem_user"))
            //{
            //    return RedirectToAction("Error", "Admin");
            //}
            //if (Models.User.CurrentUser.Quyen.CoChucNang("sua_user"))
            //{
            //    ViewBag.Xoa = true;
            //}
            var uses = db.Users.Include("Quyen").ToList();
            var pageNumber = page ?? 1;
            ViewBag.OnePageOfUser = uses.ToPagedList(pageNumber, 10);
            ViewBag.ThongBao = TempData["ThongBao"];
            return View();
        }

        public ActionResult ThemUser()
        {
            //if (!Models.User.CurrentUser.Quyen.CoChucNang("them_user"))
            //{
            //    return RedirectToAction("Error", "Admin");
            //}

            ViewBag.IdQuyen = new SelectList(db.Quyens.ToList(), "IdQuyen", "TenQuyen");
            return View();
        }
        [HttpPost]
        public ActionResult ThemUser(NhanVienViewModel register)
        {
            //if (ModelState.IsValid)
            //{
            //    BabyToys.DAL.RegisterDAL reg = new DAL.RegisterDAL();
            //    reg.ThemNhanVien(register);
            //    return RedirectToAction("Index");
            //}
            ViewBag.IdQuyen = new SelectList(db.Quyens.ToList(), "IdQuyen", "TenQuyen");
            return View(register);
        }
        public ActionResult SuaUser(int id = 0)
        {
            //if (!Models.User.CurrentUser.Quyen.CoChucNang("xem_user"))
            //{
            //    return RedirectToAction("Error", "Admin");
            //}
            User us = Session["admin"] as User;
            if (us.Id == id)
            {
                TempData["ThongBao"] = "Không thể sửa thông tin user trực tuyến";
                return RedirectToAction("Index");
            }
            var user = db.Users.SingleOrDefault(u => u.Id == id);
            NhanVienViewModel userModel = new NhanVienViewModel()
            //userModel = user;
            {
                Id = user.Id,
                TenDangNhap = user.TenDangNhap,
                TenDayDu = user.TenDayDu,
                MatKhau = user.MatKhau,
                XacNhanMatKhau = user.MatKhau,
                Email = user.Email,
                DiaChi = user.DiaChi,
                SoDienThoai = user.SoDienThoai,
                IsActive = user.IsActive,
                NgayTao=user.NgayTao
            };
            //userModel = user as RegisterModel;
            ViewBag.IdQuyen = new SelectList(db.Quyens.ToList(), "IdQuyen", "TenQuyen", user.IdQuyen);
            return View(userModel);
        }
        [HttpPost]
        public ActionResult SuaUser(NhanVienViewModel user)
        {
            //if (!Models.User.CurrentUser.Quyen.CoChucNang("sua_user"))
            //{
            //    return RedirectToAction("Error", "Admin");
            //}
            if (ModelState.IsValid)
            {
                var user_u = db.Users.SingleOrDefault(u => u.Id == user.Id);
                if (user_u.MatKhau != user.MatKhau)
                {
                    user.MatKhau = Utilities.EditString.mahoa_md5(user.MatKhau);
                }
                BabyToys.DAL.RegisterDAL reg = new DAL.RegisterDAL();
                reg.SuaNhanVien(user);
                return RedirectToAction("Index");
            }
            ViewBag.IdQuyen = new SelectList(db.Quyens.ToList(), "IdQuyen", "TenQuyen", user.IdQuyen);
            return View(user);
        }
        public ActionResult DoiMatKhauUser()
        {
            User user = Session["admin"] as User;
            if(user!=null)
            {
                NhanVienViewModel userModel = new NhanVienViewModel()
                //userModel = user;
                {
                    Id = user.Id,
                    TenDangNhap = user.TenDangNhap,
                    TenDayDu = user.TenDayDu,
                    MatKhau = user.MatKhau,
                    XacNhanMatKhau = user.MatKhau,
                    Email = user.Email,
                    DiaChi = user.DiaChi,
                    SoDienThoai = user.SoDienThoai,
                    IsActive = user.IsActive,
                    NgayTao = user.NgayTao
                };
                //userModel = user as RegisterModel;
                ViewBag.IdQuyen = new SelectList(db.Quyens.ToList(), "IdQuyen", "TenQuyen", user.IdQuyen);
                return View(userModel);
            }

            return RedirectToAction("AdminLogin");
        }

        [HttpPost]
        public ActionResult DoiMatKhauUser(NhanVienViewModel user)
        {
            if (ModelState.IsValid)
            {
                var user_u = db.Users.SingleOrDefault(u => u.Id == user.Id);
                if (user_u.MatKhau != user.MatKhau)
                {
                    user.MatKhau = Utilities.EditString.mahoa_md5(user.MatKhau);
                }
                BabyToys.DAL.RegisterDAL reg = new DAL.RegisterDAL();
                reg.SuaNhanVien(user);
                return RedirectToAction("Index");
            }
            ViewBag.IdQuyen = new SelectList(db.Quyens.ToList(), "IdQuyen", "TenQuyen", user.IdQuyen);
            return View(user);
        }
        [HttpPost]
        public ActionResult CapNhatTrangThai(int id, bool trangthai)
        {
            var user = db.Users.SingleOrDefault(u => u.Id == id);
            if (user != null)
            {
                user.IsActive = trangthai;
            }
            db.Entry(user).State = EntityState.Modified;
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        #endregion

        #region 2.Chuc Nang
        public ActionResult ChucNang(int? page)
        {
            var chucnangs = db.ChucNangs.ToList();
            var pageNumber = page ?? 1;
            ViewBag.OnePage = chucnangs.ToPagedList(pageNumber, 10);
            ViewBag.ThongBao = TempData["ThongBao"];
            ViewBag.Id = TempData["Id"];
            ViewBag.Ten = TempData["Ten"];
            ViewBag.Ma = TempData["Ma"];
            return View();
        }

        [HttpPost]
        public ActionResult ChucNang(FormCollection collection)
        {
            string id = collection["txtID"];
            string ten = collection["txtTen"];
            string ma = collection["txtCode"];
            if (String.IsNullOrEmpty(ten))
            {
                TempData["ThongBao"] = "Tên không được để trống";
                TempData["Id"] = id;
                TempData["Ten"] = ten;
                TempData["Code"] = ma;
                return RedirectToAction("ChucNang");
            }
            if (String.IsNullOrEmpty(ma))
            {
                TempData["ThongBao"] = "Mã không được để trống";
                TempData["Id"] = id;
                TempData["Ten"] = ten;
                TempData["Code"] = ma;
                return RedirectToAction("ChucNang");
            }
            if (String.IsNullOrEmpty(id))
            {
                ChucNang chucnang = new ChucNang { TenChucNang = ten, MaChucNang = ma };
                db.ChucNangs.Add(chucnang);
                db.SaveChanges();
                return RedirectToAction("ChucNang");
            }
            else
            {
                int idd = int.Parse(id.ToString());
                var chucnang = db.ChucNangs.SingleOrDefault(c => c.IdChucNang == idd);
                chucnang.TenChucNang = ten;
                chucnang.MaChucNang = ma;
                db.Entry(chucnang).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("ChucNang");
            }
            //return RedirectToAction("ChucNang");
        }


        #endregion

        #region 3.Phan Quyen
        public ActionResult PhanQuyen(int? page)
        {
            //if (!Models.User.CurrentUser.Quyen.CoChucNang("xem_phan_quyen"))
            //{
            //    return RedirectToAction("Error", "Admin");
            //}
            //if (Models.User.CurrentUser.Quyen.CoChucNang("sua_phan_quyen"))
            //{
            //    ViewBag.Xoa = true;
            //}
            var phanquyens = db.Quyens.ToList();
            var pageNumber = page ?? 1;
            ViewBag.OnePage = phanquyens.ToPagedList(pageNumber, 10);
            ViewBag.ThongBao = TempData["ThongBao"];
            return View();
        }
        public ActionResult ThemPhanQuyen()
        {
            //if (!Models.User.CurrentUser.Quyen.CoChucNang("them_phan_quyen"))
            //{
            //    return RedirectToAction("Error", "Admin");
            //}
            var viewModel = new List<AssignedPermission>();
            foreach (var chucnang in db.ChucNangs)
            {
                viewModel.Add(new AssignedPermission
                {
                    IdChucNang = chucnang.IdChucNang,
                    TenChucNang = chucnang.TenChucNang,
                    Assigned = false
                });
            }
            ViewBag.Permissions = viewModel;
            ViewBag.ThongBao = TempData["ThongBao"];
            return View();
        }
        [HttpPost]
        public ActionResult ThemPhanQuyen(FormCollection collection, int[] selectedPermissions)
        {
            string ten = collection["txtTen"];
            if (String.IsNullOrEmpty(ten))
            {
                TempData["ThongBao"] = "Tên không được để trống";
                return RedirectToAction("ThemPhanQuyen");
            }
            Quyen role = new Quyen { TenQuyen = ten, ChucNangs = new List<ChucNang>() };
            if (selectedPermissions != null)
            {
                foreach (var item in selectedPermissions)
                {
                    var chucnang = db.ChucNangs.SingleOrDefault(p => p.IdChucNang == item);
                    role.ChucNangs.Add(chucnang);
                }
            }
            db.Quyens.Add(role);
            db.SaveChanges();
            return RedirectToAction("ThemPhanQuyen");
        }
        public ActionResult SuaPhanQuyen(int id = 0)
        {
            //if (!Models.User.CurrentUser.Quyen.CoChucNang("xem_phan_quyen"))
            //{
            //    return RedirectToAction("Error", "Admin");
            //}
            var quyen = db.Quyens.Include("ChucNangs").SingleOrDefault(r => r.IdQuyen == id);
            DanhSachChucNang(quyen);
            return View(quyen);
        }
        [HttpPost]
        public ActionResult SuaPhanQuyen(FormCollection collection, string[] selectedPermissions)
        {
            //if (!Models.User.CurrentUser.Quyen.CoChucNang("sua_phan_quyen"))
            //{
            //    return RedirectToAction("Error", "Admin");
            //}
            int id = int.Parse(collection["txtId"]);
            string ten = collection["txtTen"];

            var quyen = db.Quyens.SingleOrDefault(r => r.IdQuyen == 1);
            if (quyen != null)
            {
                quyen.TenQuyen = ten;
                CapNhatChucNang(quyen, selectedPermissions);

                db.Entry(quyen).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("PhanQuyen");
            }
            DanhSachChucNang(quyen);
            return View(quyen);
        }


        private void DanhSachChucNang(Quyen quyen)
        {
            var TatCaChucNang = db.ChucNangs;
            var RolePermission = new HashSet<int>(quyen.ChucNangs.Select(r => r.IdChucNang));
            var viewModel = new List<AssignedPermission>();
            foreach (var chucnang in TatCaChucNang)
            {
                viewModel.Add(new AssignedPermission
                {
                    IdChucNang = chucnang.IdChucNang,
                    TenChucNang = chucnang.TenChucNang,
                    Assigned = RolePermission.Contains(chucnang.IdChucNang)
                });
            }
            ViewBag.Permissions = viewModel;
        }
        private void CapNhatChucNang(Quyen quyen, string[] selectedPermissions)
        {
            if (selectedPermissions == null)
            {
                return;
            }
            var selectedPermission = new HashSet<string>(selectedPermissions);
            var PermissionRole = quyen.ChucNangs.Select(c => c.IdChucNang);
            foreach (var permission in db.ChucNangs)
            {
                if (selectedPermission.Contains(permission.IdChucNang.ToString()))
                {
                    if (!PermissionRole.Contains(permission.IdChucNang))
                    {
                        quyen.ChucNangs.Add(permission);
                    }
                }
                else
                {
                    if (PermissionRole.Contains(permission.IdChucNang))
                    {
                        quyen.ChucNangs.Remove(permission);
                    }
                }
            }
        }
        #endregion
    }
}
