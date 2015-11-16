using BabyToys.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using PagedList;
using PagedList.Mvc;
namespace BabyToys.Controllers
{
    public class TimKiemController : Controller
    {
        //
        // GET: /TimKiem/
        DatabaseContext db = new DatabaseContext();
        #region
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
        public ActionResult Index(int? page, int? HienThi, int? SapXep, string currentFilter, string searchString, int? IdDanhMuc, int? IdThuongHieu,int? gt,int? gd)
        {
            //lay danh sach theo iddanhmuc
            List<SanPham> dssanphams = new List<SanPham>();
            var sanphams = from s in db.SanPhams
                           where s.IdDanhMuc == IdDanhMuc
                           select s;
            
            //load san pham dung de quy
            foreach (var sp in sanphams)
            {
                dssanphams.Add(sp);
            }
            
            LoadSanPham(IdDanhMuc ?? 1, dssanphams);
            
            //tim kiem thi quay lai trang 1
            if (searchString != null)
            {
                page = 1;
            }
            else
            {
                //gan gia tri tim kiem hien tai
                searchString = currentFilter;
            }

            //luu lai gia tri tim kiem
            ViewBag.CurrentFilter = searchString;
            if (!String.IsNullOrEmpty(searchString))
            {
                dssanphams = dssanphams.Where(s => Utilities.EditString.BoDauTrenChuoi(s.TenSP.ToLower()).Contains(Utilities.EditString.BoDauTrenChuoi(searchString.ToLower()))).ToList();
            }

            //tim theo thương hiệu
            if (!String.IsNullOrEmpty(IdThuongHieu.ToString()))
            {
                dssanphams = dssanphams.Where(s => s.IdThuongHieu == IdThuongHieu).ToList();
            }
            
            dssanphams = dssanphams.OrderByDescending(s => s.IdSanPham).ToList();
            
            ////lay gia lon nhat va nho nhat
            //List<SanPham> a = db.SanPhams.ToList();

            //ViewBag.giamin = a.Min(s => s.GiaHienTai);
            //ViewBag.giamax = a.Max(s => s.GiaHienTai);

            //int? giatu = gt ?? a.Min(s => s.GiaHienTai);
            //int? giaden = gd ?? a.Max(s => s.GiaHienTai);

            ////tim theo gia
            //dssanphams = dssanphams.Where(s => s.GiaHienTai >= giatu && s.GiaHienTai <= giaden).ToList();
            //ViewBag.gt = giatu;
            //ViewBag.gd = giaden;
            
            //sap xep
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
            
            ViewBag.Count = dssanphams.Count();
            var pageNumber = page ?? 1;
            var dsdanhmuc = DropdownListDanhMuc();
            dsdanhmuc = dsdanhmuc.Where(d => d.Value != 1).ToList();
            ViewBag.IdDanhMuc = new SelectList(dsdanhmuc, "Value", "Text");
            ViewBag.IdThuongHieu = new SelectList(db.ThuongHieus.ToList(), "IdThuongHieu", "TenThuongHieu");
            ViewBag.onePageOfProduct = dssanphams.ToPagedList(pageNumber, HienThi ?? 6);
            ViewBag.HienThi = HienThi ?? 6;
            ViewBag.SapXep = SapXep ?? 0;
            ViewBag.DanhMuc = IdDanhMuc;
            ViewBag.ThuongHieu = IdThuongHieu;
            return View();
        }

    }
}
