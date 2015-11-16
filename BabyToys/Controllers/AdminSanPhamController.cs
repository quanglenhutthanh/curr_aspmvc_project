using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using BabyToys.Models;
using System.Web.Helpers;
using System.IO;
using System.Text.RegularExpressions;
using PagedList.Mvc;
using PagedList;
using System.Data;
using BabyToys.Utilities;
using System.Data.Entity;
using BabyToys.Models.ViewModel;
namespace BabyToys.Controllers
{
    public class AdminSanPhamController : admin_baseController
    {
        //
        // GET: /AdminSanPham/
        DatabaseContext db = new DatabaseContext();
        public ActionResult Index()
        {
            return RedirectToAction("DanhSach");
        }
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
        public ActionResult CapNhatHienThi(int id, bool hienthi)
        {
            if (Models.User.CurrentUser.Quyen.CoChucNang("sua_san_pham"))
            {
                var sanpham = db.SanPhams.SingleOrDefault(s => s.IdSanPham == id);
                sanpham.HienThi = hienthi;
                db.Entry(sanpham).State = EntityState.Modified;
                db.SaveChanges();
                return View();
            }
            //TempData["ThongBao"] = "<script>$( document ).ready(function(){ alert('Bạn không có quyền thực hiện chức này!') })</script>";
            return RedirectToAction("Error", "Admin");
        }
        public void LuuChiTietAnh(SanPham sanpham, int number)
        {
            for (int i = 100; i < 111; i++)
            {
                var image = WebImage.GetImageFromRequest("FileUpLoad" + i);
                if (image != null)
                {
                    string ImageName = Path.GetFileName(image.FileName);
                    string main = "main-" + Utilities.EditString.BoDauTrenChuoi(System.DateTime.Now.ToString()) + "-" + ImageName;
                    var pathToSave = Path.Combine(Server.MapPath("~/Content/Images/SanPham"), main);
                    image.Save(pathToSave);

                    string thumbnail = "thumbnail-" + Utilities.EditString.BoDauTrenChuoi(System.DateTime.Now.ToString()) + "-" + ImageName;
                    var pathToSaveThumbnail = Path.Combine(Server.MapPath("~/Content/Images/SanPham"), thumbnail);
                    image.Resize(250, 250, true, true);
                    image.Save(pathToSaveThumbnail);
                    HinhAnh hinh = new HinhAnh { SanPham = sanpham, AnhGoc = main, Thumbnail = thumbnail };
                    db.HinhAnhs.Add(hinh);
                    db.SaveChanges();
                }
            }
        }

        #endregion

        #region 1.Danh sach
        [CustomActionFilter]
        public ActionResult DanhSach(int? page, int? HienThi, int? SapXep, string currentFilter, string searchString, int? IdDanhMuc, int? IdThuongHieu)
        {
            if (!Models.User.CurrentUser.Quyen.CoChucNang("xem_san_pham"))
            {
                return RedirectToAction("Error", "Admin");
            }
            if (Models.User.CurrentUser.Quyen.CoChucNang("sua_san_pham"))
            {
                ViewBag.Xoa = true;
            }
            List<SanPham> dssanphams = new List<SanPham>();
            var sanphams = from s in db.SanPhams
                           where s.IdDanhMuc == IdDanhMuc
                           select s;
            ViewBag.Count = db.SanPhams.Count();
            foreach (var sp in sanphams)
            {
                dssanphams.Add(sp);
            }

            LoadSanPham(IdDanhMuc ?? 1, dssanphams);
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
                dssanphams = dssanphams.Where(s => s.TenSP.ToLower().Contains(searchString.ToLower())).ToList();
                //ViewBag.TenSanPham = TenSanPham;
            }
            if (!String.IsNullOrEmpty(IdThuongHieu.ToString()))
            {
                dssanphams = dssanphams.Where(s => s.IdThuongHieu == IdThuongHieu).ToList();
            }
            //dssanphams = dssanphams.Where(s => s.IdDanhMuc == IdDanhMuc).ToList();
            dssanphams = dssanphams.OrderByDescending(s => s.IdSanPham).ToList();
            switch (SapXep ?? 0)
            {
                case 0: break;
                case 1:
                    dssanphams = dssanphams.OrderBy(s => s.Gia).ToList();
                    break;
                case 2:
                    dssanphams = dssanphams.OrderByDescending(s => s.Gia).ToList();
                    break;
                case 3:
                    dssanphams = dssanphams.OrderBy(s => s.TenSP).ToList();
                    break;
                case 4:
                    dssanphams = dssanphams.OrderByDescending(s => s.TenSP).ToList();
                    break;
                default:
                    break;
            }
            var pageNumber = page ?? 1;
            ViewBag.IdDanhMuc = new SelectList(DropdownListDanhMuc(), "Value", "Text");
            ViewBag.IdThuongHieu = new SelectList(db.ThuongHieus.ToList(), "IdThuongHieu", "TenThuongHieu");
            ViewBag.onePageOfProduct = dssanphams.ToPagedList(pageNumber, HienThi ?? 10);
            ViewBag.HienThi = HienThi ?? 10;
            ViewBag.SapXep = SapXep ?? 0;
            ViewBag.DanhMuc = IdDanhMuc;
            ViewBag.ThuongHieu = IdThuongHieu;
            ViewBag.ThongBao = TempData["ThongBao"];

            return View();
        }
        #endregion

        #region 2.Sua
        public ActionResult Sua(int id = 0)
        {
            if (!Models.User.CurrentUser.Quyen.CoChucNang("xem_san_pham"))
            {
                return RedirectToAction("Error", "Admin");
            }
            var sanpham = db.SanPhams.SingleOrDefault(s => s.IdSanPham == id);
            if (sanpham.ListKhuyenMai.Any())
            {
                DateTime mindate = (from k in db.KhuyenMais where k.IdSanPham == id select k.DenNgay).Max();
                mindate = mindate.AddDays(1);
                ViewBag.mindate = String.Format("{0:dd/MM/yyyy}", mindate);
            }
            //ViewBag.maxdate = (from k in db.KhuyenMais where k.IdSanPham == id select k.TuNgay).Max();
            ViewBag.HinhAnh = db.HinhAnhs.ToList().Where(h => h.SanPham == sanpham);
            ViewBag.SoHinhCoTheUp = 10 - (db.HinhAnhs.ToList().Where(h => h.SanPham == sanpham).Count());
            ViewBag.IdDanhMuc = new SelectList(DropdownListDanhMuc(), "Value", "Text", sanpham.IdDanhMuc);
            ViewBag.IdThuongHieu = new SelectList(db.ThuongHieus.ToList(), "IdThuongHieu", "TenThuongHieu", sanpham.IdDanhMuc);
            return View(sanpham);

        }
        [HttpPost]
        [ValidateInput(false)]
        [CustomActionFilter]
        public ActionResult Sua(SanPham sanpham, string sua, FormCollection collection)
        {
            if (!Models.User.CurrentUser.Quyen.CoChucNang("sua_san_pham"))
            {
                return RedirectToAction("Error", "Admin");
            }
            if ("Upload".Equals(sua, StringComparison.OrdinalIgnoreCase))
            {
                //LuuChiTietAnh(sanpham,(10 - (db.HinhAnhs.ToList().Where(h => h.SanPham == sanpham).Count())));
                var sp = db.SanPhams.SingleOrDefault(s => s.IdSanPham == sanpham.IdSanPham);
                LuuChiTietAnh(sp, (10 - (db.HinhAnhs.ToList().Where(h => h.SanPham == sp).Count())));
                ViewBag.HinhAnh = db.HinhAnhs.ToList().Where(h => h.SanPham == sp);
                ViewBag.SoHinhCoTheUp = 10 - (db.HinhAnhs.ToList().Where(h => h.SanPham == sp).Count());
                ViewBag.IdDanhMuc = new SelectList(DropdownListDanhMuc(), "Value", "Text", sanpham.IdDanhMuc);
                ViewBag.IdThuongHieu = new SelectList(db.ThuongHieus.ToList(), "IdThuongHieu", "TenThuongHieu", sanpham.IdDanhMuc);
                return View(sanpham);
            }
            if (ModelState.IsValid)
            {
                var image = WebImage.GetImageFromRequest("anhdaidien");
                if (image != null)
                {
                    string ImageName = Path.GetFileName(image.FileName);
                    ImageName = EditString.BoDauTrenChuoi(ImageName);
                    //anh goc
                    ImageName = "main-" + Utilities.EditString.BoDauTrenChuoi(System.DateTime.Now.ToString()) + "-" + ImageName;
                    var pathToSave = Path.Combine(Server.MapPath("~/Content/Images/SanPham"), ImageName);
                    image.Save(pathToSave);
                    sanpham.HinhDaiDien = ImageName;
                    //thumbnail
                    ImageName = "thumbnail-" + Utilities.EditString.BoDauTrenChuoi(System.DateTime.Now.ToString()) + "-" + ImageName;
                    var pathToSaveThumbnail = Path.Combine(Server.MapPath("~/Content/Images/SanPham"), ImageName);
                    image.Resize(250, 250, true, true);
                    image.Save(pathToSaveThumbnail);
                    sanpham.Thumbnail = ImageName;
                }
                //else
                //{
                //    sanpham.HinhDaiDien = "noimageyet.jpg";
                //    sanpham.Thumbnail = "thumb-noimageyet.jpg";
                //}
                
                db.Entry(sanpham).State = EntityState.Unchanged;
                db.Entry(sanpham).State = EntityState.Modified;
                db.SaveChanges();
                if ("Lưu".Equals(sua, StringComparison.OrdinalIgnoreCase))
                {
                    SuaKhuyenMai(collection);
                    return RedirectToAction("Sua", new { id = sanpham.IdSanPham });
                }
                if ("Lưu và đóng".Equals(sua, StringComparison.OrdinalIgnoreCase))
                {
                    return RedirectToAction("DanhSach");
                }
                if ("Lưu và tiếp tục".Equals(sua, StringComparison.OrdinalIgnoreCase))
                {
                    return RedirectToAction("Sua", new { id = sanpham.IdSanPham });
                }
            }

            ViewBag.HinhAnh = db.HinhAnhs.ToList().Where(h => h.SanPham == sanpham);
            ViewBag.SoHinhCoTheUp = 10 - (db.HinhAnhs.ToList().Where(h => h.SanPham == sanpham).Count());
            ViewBag.IdDanhMuc = new SelectList(DropdownListDanhMuc(), "Value", "Text", sanpham.IdDanhMuc);
            ViewBag.IdThuongHieu = new SelectList(db.ThuongHieus.ToList(), "IdThuongHieu", "TenThuongHieu", sanpham.IdDanhMuc);
            return View(sanpham);
        }

        public ActionResult XoaDanhGia(int id)
        {
            var danhgia = db.DanhGia.SingleOrDefault(d => d.Id == id);
            var sanpham = db.SanPhams.SingleOrDefault(s => s.IdSanPham == danhgia.SanPham.IdSanPham);
            if (danhgia != null)
            {
                db.DanhGia.Remove(danhgia);
                db.SaveChanges();
            }
            return RedirectToAction("Sua", new { id = sanpham.IdSanPham });
        }

        public ActionResult XoaKhuyenMai(int id)
        {
            var khuyenmai = db.KhuyenMais.SingleOrDefault(m => m.IdKhuyenMai == id);
            var sanpham = db.SanPhams.SingleOrDefault(s => s.IdSanPham == khuyenmai.IdSanPham);
            if (khuyenmai != null)
            {
                db.KhuyenMais.Remove(khuyenmai);
                db.SaveChanges();
            }
            return RedirectToAction("Sua", new { id = sanpham.IdSanPham });
        }
        #endregion

        #region 3.Them Moi
        public ActionResult ThemMoi()
        {
            //User selected=Models.User.CurrentUser
            if (!Models.User.CurrentUser.Quyen.CoChucNang("them_san_pham"))
            {
                return RedirectToAction("Error", "Admin");
            }
            ViewBag.IdDanhMuc = new SelectList(DropdownListDanhMuc(), "Value", "Text");
            ViewBag.IdThuongHieu = new SelectList(db.ThuongHieus.ToList(), "IdThuongHieu", "TenThuongHieu");
            return View();
        }

        [HttpPost]
        [ValidateInput(false)]
        public ActionResult ThemMoi(SanPham sanpham, string luu, FormCollection collection)
        {
            ViewBag.Loi = TempData["Loi"];
            if (db.SanPhams.ToList().Any(s => s.MaSP == sanpham.MaSP))
            {
                ModelState.AddModelError("MaSP", "Mã sản phẩm đã tồn tại trong CSDL");
                
            }
            if (ModelState.IsValid)
            {
                var image = WebImage.GetImageFromRequest("anhdaidien");
                if (image != null)
                {
                    string ImageName = Path.GetFileName(image.FileName);
                    ImageName = EditString.BoDauTrenChuoi(ImageName);
                    //anh goc
                    ImageName = "main" + Utilities.EditString.BoDauTrenChuoi(System.DateTime.Now.ToString()) + "-" + ImageName;
                    var pathToSave = Path.Combine(Server.MapPath("~/Content/Images/SanPham"), ImageName);
                    image.Save(pathToSave);
                    sanpham.HinhDaiDien = ImageName;
                    //thumbnail/
                    ImageName = "thumbnail-" + Utilities.EditString.BoDauTrenChuoi(System.DateTime.Now.ToString()) + "-" + ImageName;
                    var pathToSaveThumbnail = Path.Combine(Server.MapPath("~/Content/Images/SanPham"), ImageName);
                    image.Resize(250, 250, true, true);
                    image.Save(pathToSaveThumbnail);
                    sanpham.Thumbnail = ImageName;
                }
                else
                {
                    sanpham.HinhDaiDien = "noimageyet.jpg";
                    sanpham.Thumbnail = "thumb-noimageyet.jpg";
                }
               
                   
                db.SanPhams.Add(sanpham);
                db.SaveChanges();
                LuuChiTietAnh(sanpham, 10);
                string tungay = collection["from"];
                string denngay = collection["to"];
                if (!String.IsNullOrEmpty(tungay) && !String.IsNullOrEmpty(denngay))
                {
                    LuuKhuyenMai(sanpham, collection);
                }

                if ("Lưu và đóng".Equals(luu, StringComparison.OrdinalIgnoreCase))
                {
                    return RedirectToAction("DanhSach");
                }
                if ("Lưu và tạo mới".Equals(luu, StringComparison.OrdinalIgnoreCase))
                {
                    return RedirectToAction("ThemMoi");
                }
            }
            ViewBag.IdThuongHieu = new SelectList(db.ThuongHieus.ToList(), "IdThuongHieu", "TenThuongHieu");
            ViewBag.IdDanhMuc = new SelectList(DropdownListDanhMuc(), "Value", "Text");
            return View(sanpham);
        }

          
        public ActionResult SuaKhuyenMai(FormCollection collection)
        {
            int id = int.Parse(collection["idsp"].ToString());
            var sanpham = db.SanPhams.SingleOrDefault(s => s.IdSanPham == id);
        
            if (sanpham != null)
            {
                LuuKhuyenMai(sanpham, collection);
            }
            return RedirectToAction("Sua", new { id = sanpham.IdSanPham });
        }

        public void LuuKhuyenMai(SanPham sanpham, FormCollection collection)
        {
            string tungay = collection["from"];
            string denngay = collection["to"];
            string giam = collection["giamgia"];
            SanPham sp = db.SanPhams.SingleOrDefault(s => s.IdSanPham == sanpham.IdSanPham);
            KhuyenMai khuyenmai = new KhuyenMai
            {
                TuNgay =  DateTime.ParseExact(tungay, "dd/MM/yyyy", null),
                DenNgay = DateTime.ParseExact(denngay, "dd/MM/yyyy", null),
                PhanTramGiam = giam != null ? int.Parse(giam.ToString()) : 0,
                SanPham=sp
            };
            db.KhuyenMais.Add(khuyenmai);
            db.SaveChanges();
        }
        #endregion

        #region 4.Xoa

        public ActionResult Xoa(int id)
        {
            if (Models.User.CurrentUser.Quyen.CoChucNang("xoa_san_pham"))
            {
                var sanpham = db.SanPhams.SingleOrDefault(s => s.IdSanPham == id);
                if (sanpham != null)
                {
                    XoaSanPham(sanpham);
                }
                TempData["ThongBao"] = "<script>$( document ).ready(function(){ alert('Xóa thành công!') })</script>";
                return RedirectToAction("DanhSach");
            }
            return RedirectToAction("Error", "Admin");
        }
        [HttpPost]
        public ActionResult XoaNhieuSanPham(int[] deleteInputs, int? HienThi, int? page)
        {
            if (Models.User.CurrentUser.Quyen.CoChucNang("xoa_san_pham"))
            {
                if (deleteInputs != null)
                {
                    foreach (var item in deleteInputs)
                    {
                        var sanpham = db.SanPhams.SingleOrDefault(s => s.IdSanPham == item);
                        XoaSanPham(sanpham);
                    }
                    TempData["ThongBao"] = "<script>$( document ).ready(function(){ alert('Xóa thành công!') })</script>";
                    return RedirectToAction("DanhSach");
                }
                TempData["ThongBao"] = "<script>$( document ).ready(function(){ alert('Bạn chưa chọn sản phẩm!') })</script>";
                return RedirectToAction("DanhSach");
            }
            return RedirectToAction("Error", "Admin");
        }
        public void XoaSanPham(SanPham sanpham)
        {
            var hinhanhs = db.HinhAnhs.ToList().Where(h => h.SanPham == sanpham);
            if (hinhanhs != null)
            {
                foreach (var hinhanh in hinhanhs)
                {
                    string anhgoc = hinhanh.AnhGoc;
                    XoaAnhTrongThuMuc(anhgoc);
                    string thumbnail = hinhanh.Thumbnail;
                    XoaAnhTrongThuMuc(thumbnail);

                    db.HinhAnhs.Remove(hinhanh);
                    db.SaveChanges();
                }
            }

            string hinhdaidien = sanpham.HinhDaiDien;
            XoaAnhTrongThuMuc(hinhdaidien);
            string thumbnailsp = sanpham.Thumbnail;
            XoaAnhTrongThuMuc(thumbnailsp);

            db.SanPhams.Remove(sanpham);
            db.SaveChanges();
        }
        public ActionResult XoaAnh(int id, int IdSanPham)
        {
            var hinhanh = db.HinhAnhs.SingleOrDefault(h => h.ID == id);
            string anhgoc = hinhanh.AnhGoc;
            XoaAnhTrongThuMuc(anhgoc);
            string thumbnail = hinhanh.Thumbnail;
            XoaAnhTrongThuMuc(thumbnail);
            db.HinhAnhs.Remove(hinhanh);
            db.SaveChanges();
            //XoaAnhTrongThuMuc(
            return RedirectToAction("Sua", new { id = IdSanPham });
        }
        public void XoaAnhTrongThuMuc(string name)
        {
            if (!String.IsNullOrEmpty(name))
            {
                var path = Path.Combine(Server.MapPath("~/Content/Images/SanPham"), name);
                if (System.IO.File.Exists(path))
                {
                    System.IO.File.Delete(path);
                }
            }
        }
        #endregion

        public ActionResult LienHe()
        {
            var lienhes = db.DanhGia.Where(d => d.IdSanPham == null);
            return View(lienhes);
        }

        public ActionResult ChiTietLienHe(int Id)
        {
            var lienhe = db.DanhGia.SingleOrDefault(d => d.Id == Id);
            return View(lienhe);
        }
        public ActionResult TonKho(string from,string to, int? hienthi, int? page)
        {
            DateTime FromDate = new DateTime();
            DateTime ToDate = new DateTime();
            if (!String.IsNullOrEmpty(from) && String.IsNullOrEmpty(to))
            {
                FromDate = DateTime.ParseExact(from, "dd/MM/yyyy", null);
                ToDate = System.DateTime.Now;
                ViewBag.FromDate = from;
            }
            if (String.IsNullOrEmpty(from) && !String.IsNullOrEmpty(to))
            {
                ToDate = DateTime.ParseExact(to, "dd/MM/yyyy", null);
                FromDate = new DateTime(2013, 01, 01);
                ViewBag.ToDate = to;
            }
            if (!String.IsNullOrEmpty(from) && !String.IsNullOrEmpty(to))
            {
                ToDate = DateTime.ParseExact(to, "dd/MM/yyyy", null);
                FromDate = DateTime.ParseExact(from, "dd/MM/yyyy", null);
                ViewBag.FromDate = from;
                ViewBag.ToDate = to;
            }
            if (String.IsNullOrEmpty(from) && String.IsNullOrEmpty(to))
            {
                ToDate = System.DateTime.Now;
                FromDate = new DateTime(2013,01,01);
                ViewBag.FromDate = from;
                ViewBag.ToDate = to;
            }
            List<TonKhoViewModel> tonkhos = new List<TonKhoViewModel>();
            var sanphams = db.SanPhams.ToList().OrderByDescending(s => s.IdSanPham);
            foreach (var sanpham in sanphams)
            {
                TonKhoViewModel tonkho = new TonKhoViewModel();
                tonkho.SanPham = sanpham;
                if (sanpham.ListTonKho.Count > 0)
                {
                    List<TonKho> temp = sanpham.ListTonKho;
                    sanpham.ListTonKho = sanpham.ListTonKho.Where(t => t.ThoiGian >= FromDate && t.ThoiGian < ToDate).ToList();
                    if (sanpham.ListTonKho.Count <= 0)
                    {
                        TonKho tk = temp.Where(t => t.ThoiGian < FromDate).ToList().OrderByDescending(t => t.Id).First();
                        tonkho.TonDau = tk.SoLuong;
                        tonkho.TonCuoi = tk.SoLuong;
                        tonkho.Nhap = 0;
                        tonkho.Xuat = 0;
                    }
                    else
                    {
                        tonkho.TonDau = sanpham.ListTonKho.First().SoLuong - sanpham.ListTonKho.First().Nhap + sanpham.ListTonKho.First().Xuat;
                        tonkho.TonCuoi = sanpham.ListTonKho.OrderByDescending(t => t.Id).First().SoLuong;
                        tonkho.Nhap = (sanpham.ListTonKho.Select(t => t.Nhap)).Sum();
                        tonkho.Xuat = (sanpham.ListTonKho.Select(t => t.Xuat)).Sum();
                    }
                }
                else
                {
                    tonkho.TonDau = 0;
                    tonkho.TonCuoi = 0;
                    tonkho.Nhap = 0;
                    tonkho.Xuat = 0;
                }
                tonkhos.Add(tonkho);
            }
            var pageNumber = page ?? 1;
            ViewBag.tonkhos = tonkhos.ToPagedList(pageNumber, hienthi ?? 10);
            ViewBag.HienThi = hienthi ?? 10;
            return View();   
        }
        public ActionResult ChiTietTonKho(int id,int? hienthi, int? page)
        {
            List<TonKhoViewModel> dstonkho = new List<TonKhoViewModel>();
            var sanphams = db.SanPhams.ToList().OrderByDescending(s => s.IdSanPham);
            foreach (var sanpham in sanphams)
            {
                if (sanpham.ListTonKho.Count > 0)
                {
                    foreach (var item in sanpham.ListTonKho)
                    {
                        TonKhoViewModel tonkho = new TonKhoViewModel();
                        tonkho.ThoiGian = String.Format("{0:dd/mm/yyyy hh:mm:ss}",item.ThoiGian);
                        tonkho.SanPham = sanpham;
                        tonkho.TonDau = item.SoLuong-item.Nhap+item.Xuat;
                        tonkho.TonCuoi = item.SoLuong;
                        tonkho.Nhap = item.Nhap;
                        tonkho.Xuat = item.Xuat;
                        dstonkho.Add(tonkho);
                    }
                }
                else
                {
                    TonKhoViewModel tonkho = new TonKhoViewModel();
                    tonkho.SanPham = sanpham;
                    tonkho.TonDau = 0;
                    tonkho.TonCuoi = 0;
                    tonkho.Nhap = 0;
                    tonkho.Xuat = 0;
                    dstonkho.Add(tonkho);
                }
                
            }
            dstonkho = dstonkho.Where(t => t.SanPham.IdSanPham == id).ToList();
            var pageNumber = page ?? 1;
            ViewBag.id = id;
            ViewBag.tonkhos = dstonkho.ToPagedList(pageNumber, hienthi ?? 10);
            ViewBag.HienThi = hienthi;
            return View();
        }

        

    }
}
