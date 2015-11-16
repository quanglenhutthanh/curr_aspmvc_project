using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.IO;
using System.Linq;
using System.Web;

namespace BabyToys.Models
{
    public class SanPham
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int IdSanPham { get; set; }
        [Required(ErrorMessage = "Mã sản phẩm không được để trống")]
        public string MaSP { get; set; }
        [Required(ErrorMessage = "Tên sản phẩm không được để trống")]
        public string TenSP { get; set; }
        public string HinhDaiDien { get; set; }
        //public string Hinh
        //{
        //    if (!File.Exists(HinhDaiDien))
        //    {
        //        return "G:\\Programming\\current\\TraiCayViTa\\BabyToys\\Content\\Images\\khongxoa\\noimage.jpg";
        //    }
        //    return HinhDaiDien;
        //}
        public string Thumbnail { get; set; }
        public int DanhGia { get; set; }
        public string MoTaSP { get; set; }
        public bool HienThi { get; set; }
        public string ChatLieu { get; set; }
        public string MauSac { get; set; }
        public string KichThuoc { get; set; }
        public Nullable<int> LuotXem { get; set; }
        [Required(ErrorMessage = "Chọn danh mục cho sản phẩm")]
        public int IdDanhMuc { get; set; }
        [Required(ErrorMessage = "Chọn thương hiệu cho sản phẩm")]
        public int IdThuongHieu { get; set; }
        [RegularExpression(@"^(\d+)$", ErrorMessage = "Cần nhập giá kiểu số")]
        public int Gia { get; set; }
        
        public virtual ICollection<ChiTietHoaDon> ChiTietHoaDons { get; set; }
        public virtual DanhMuc DanhMuc { get; set; }
        public virtual ICollection<HinhAnh> HinhAnhs { get; set; }
        public virtual ThuongHieu ThuongHieu { get; set; }
        public virtual List<DanhGia> ListDanhGia { get; set; }
        public virtual List<KhuyenMai> ListKhuyenMai { get; set; }
        public virtual List<ChiTietPhieuNhap> ListPhieuNhap { get; set; }
        public virtual List<TonKho> ListTonKho { get; set; }
        
        public int SoLuong()
        {
            try
            {
                if (ListTonKho.Count > 0)
                {
                    return ListTonKho.OrderByDescending(t => t.Id).FirstOrDefault().SoLuong;
                }
            }
            catch
            {
                return 0;
            }
            return 0;
        }

        public int GiaKhuyenMai()
        {
            try
            {
                if (ListKhuyenMai.Count > 0)
                {
                    foreach (var item in ListKhuyenMai)
                    {
                        DateTime ngayhientai = System.DateTime.Now;
                        if (ngayhientai >= item.TuNgay && ngayhientai <= item.DenNgay)
                        {
                            return Gia - Gia * item.PhanTramGiam / 100;
                        }
                    }
                }
            }
            catch
            {
                return 0;
            }
            return 0;
        }
        
        public int GiaHienTai
        {
            get { return (GiaKhuyenMai() > 0) ? GiaKhuyenMai() : Gia; }
        }
        
        public float DiemDanhGiaTrungBinh()
        {
            if (ListDanhGia == null)
            {
                return 0;
            }
            int tongdiem=0;
            foreach (var item in ListDanhGia)
            {
                tongdiem += item.DiemDanhGia;
            }
            float diemtrungbinh = (float)tongdiem / (float)(ListDanhGia.Count);
            return diemtrungbinh;
        }

        public  string Hinh()
        {
            string imagePath = Path.Combine(System.Web.HttpContext.Current.Server.MapPath("~/Content/Images/SanPham"), HinhDaiDien);
            if(File.Exists(imagePath))
            {
                return HinhDaiDien;
            }
            return "noimage.jpg";
        }
    }
}