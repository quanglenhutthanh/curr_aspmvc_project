using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;
namespace BabyToys.Models
{
    public class DatabaseInnitializer:DropCreateDatabaseIfModelChanges<DatabaseContext>
    {
        protected override void Seed(DatabaseContext context)
        {
            TinhThanh angiang =  new TinhThanh{TenTinh="An Giang",PhiVanChuyen=100000};
            var tinhthanhs= new List<TinhThanh>
            {
                angiang,
                new TinhThanh{TenTinh="Bà Rịa - Vũng Tàu",PhiVanChuyen=100000},
                new TinhThanh{TenTinh="thành phố Hồ Chí Minh",PhiVanChuyen=0}
            };
            tinhthanhs.ForEach(t => context.TinhThanhs.Add(t));
            context.SaveChanges();
            ThuongHieu alphabooks = new ThuongHieu{TenThuongHieu="Alphabook",QuocGia="Việt Nam",Logo="main2013113223742-tải xuống (1).jpg",IsActive=true};
            ThuongHieu dragonforce = new ThuongHieu { TenThuongHieu = "Dragon Force", QuocGia = "Trung Quốc", Logo = "main2013113223711-tải xuống.jpg",IsActive=true };
            ThuongHieu winwintoy = new ThuongHieu { TenThuongHieu = "Winwintoys", QuocGia = "Việt Nam", Logo = "main2013113223351-LoGo Winwintoys Đúng [Converted].jpg",IsActive=true };
            ThuongHieu bandai = new ThuongHieu { TenThuongHieu = "Bandai", QuocGia = "Nhật Bản", Logo = "main2013116223259-bandai.png",ThongTin="Bandai một công ty sản xuất đồ chơi của Nhật Bản",IsActive=true };
            ThuongHieu TakaraTomy = new ThuongHieu { TenThuongHieu = "Takara Tomy", QuocGia = "Nhật Bản", Logo = "main2013116223512-Takara_Tomy_Logo.png",IsActive=true };
            var thuonghieus = new List<ThuongHieu>
            {
                alphabooks,
                dragonforce,
                winwintoy,
                bandai,
                TakaraTomy
            };
            thuonghieus.ForEach(t => context.ThuongHieus.Add(t));
            context.SaveChanges();
            DanhMuc root = new DanhMuc { TenDanhMuc = "Root" };
            DanhMuc danhmuc1 = new DanhMuc { TenDanhMuc="Đồ chơi giáo dục",DanhMucCha=root,IsActive=true};
            DanhMuc danhmuc2 = new DanhMuc { TenDanhMuc = "Đồ chơi không dùng pin", DanhMucCha=danhmuc1,IsActive=true };
            DanhMuc danhmuc3 = new DanhMuc { TenDanhMuc = "Đồ chơi nhập vai", DanhMucCha=root,IsActive=true};
            DanhMuc danhmuc4 = new DanhMuc { TenDanhMuc = "Đồ chơi xếp hình", DanhMucCha = root ,IsActive=true};
            DanhMuc danhmuc5 = new DanhMuc { TenDanhMuc = "Đồ chơi búp bê", DanhMucCha = root,IsActive=true };
            DanhMuc danhmuc6 = new DanhMuc { TenDanhMuc = "Nhà búp bê", DanhMucCha = danhmuc5,IsActive=true };
            var danhmucs = new List<DanhMuc>
            {
                root,
                danhmuc1,
                danhmuc2,
                danhmuc3,
                danhmuc4,
                danhmuc5,
                danhmuc6
            };
            danhmucs.ForEach(d => context.DanhMucs.Add(d));
            context.SaveChanges();

            SanPham sanpham1 = new SanPham { TenSP = "Winwintoys 67042 - Bộ hình học cao thấp", MaSP = "WI777TB64KRFANVN-61999",ThuongHieu=winwintoy,DanhMuc=danhmuc2 ,HienThi=true,DanhGia=2};
            SanPham sanpham2 = new SanPham { TenSP = "Winwintoys 61392 - Ba bước phát triển", MaSP = "WI777TB86KUFANVN-62077", ThuongHieu = winwintoy, DanhMuc = danhmuc2,HienThi=false,DanhGia=3 };
            SanPham sanpham3 = new SanPham
            {
                TenSP = "Winwintoys 65232 - Đường luồn chữ cái",
                MaSP = "WI777TB29WUCANVN-33292",
                HinhDaiDien = "2131-Winwintoys-65232-Duong-luon-chu-cai-win_win_toys-8754-07923-1-zoom.jpg",
                Thumbnail = "thumbnail-20131014211558-Winwintoys-65232-Duong-luon-chu-cai-2131-Winwintoys-65232-Duong-luon-chu-cai-win_win_toys-8754-07923-1-zoom.jpg",
                MauSac = "Nhiều màu",
                ChatLieu = "Gỗ",
                KichThuoc = "30 x 22 x 1.6 cm",
                DanhMuc=danhmuc2,
                ThuongHieu=winwintoy,
                DanhGia=4
            };
            var sanphams = new List<SanPham>
            {
                sanpham1,
                sanpham2,
                sanpham3
            };
            for (int i = 0; i < 50; i++)
            {
                SanPham sanpham = new SanPham { TenSP = "San pham" + i, MaSP = (i * 10).ToString(),ThuongHieu = winwintoy, DanhMuc = danhmuc2, HienThi = true, HinhDaiDien = "noimageyet.jpg", Thumbnail = "thumb-noimageyet.jpg",DanhGia=i%5 };
                sanphams.Add(sanpham);
            }
                sanphams.ForEach(s => context.SanPhams.Add(s));
            context.SaveChanges();
            HinhAnh hinhanh1 = new HinhAnh
            {
                AnhGoc = "2111--2-win_win_toys-8754-07923-2-zoom.jpg",
                Thumbnail = "thumbnail-20131014211637-2111--2-win_win_toys-8754-07923-2-zoom.jpg"
            };
            HinhAnh hinhanh2 = new HinhAnh
            {
                AnhGoc = "2128--1-win_win_toys-8754-07923-3-zoom.jpg",
                Thumbnail = "thumbnail-20131014211654-2128--1-win_win_toys-8754-07923-3-zoom.jpg"
            };
            var hinhanhs = new List<HinhAnh>()
            {
                hinhanh1,
                hinhanh2
            };
            hinhanhs.ForEach(s => context.HinhAnhs.Add(s));
            context.SaveChanges();

            var xoa_san_pham = new ChucNang { TenChucNang = "Xóa sản phẩm", MaChucNang = "xoa_san_pham" };
            var them_san_pham = new ChucNang { TenChucNang = "Thêm sản phẩm", MaChucNang = "them_san_pham" };
            var sua_san_pham = new ChucNang { TenChucNang = "Sửa sản phẩm", MaChucNang = "sua_san_pham" };
            var xem_san_pham = new ChucNang { TenChucNang = "Xem sản phẩm", MaChucNang = "xem_san_pham" };
            var perms = new List<ChucNang>
            {
                xoa_san_pham,
                xem_san_pham
            };
            base.Seed(context);
            perms.ForEach(p => context.ChucNangs.Add(p));
            context.SaveChanges();

            Quyen administrator = new Quyen
            {
                TenQuyen = "Quản trị viên",
                ChucNangs = new List<ChucNang>
                {
                    xoa_san_pham,
                    them_san_pham,
                    sua_san_pham,
                    xem_san_pham
                }
            };

            Quyen user = new Quyen
            {
                TenQuyen = "Quản lí",
                ChucNangs = new List<ChucNang>
                {
                    xem_san_pham
                }
            };
            var roles = new List<Quyen>
            {
                administrator,
                user
            };
            roles.ForEach(r => context.Quyens.Add((r)));
            context.SaveChanges();
            User admin = new User { TenDangNhap = "admin", MatKhau = "2241224290212381311792231031409724444107218", Email = "admin@gmail.com", TenDayDu = "Administrator", Quyen = administrator, SoDienThoai = "0123456789" };
            User user1 = new User { TenDangNhap = "user1", MatKhau = "admin1", Email = "admin@gmail.com", TenDayDu = "Administrator", Quyen = user, SoDienThoai = "0123456789" };
            var users = new List<User>
            {
                admin,
                user1
            };
            users.ForEach(u => context.Users.Add(u));
            context.SaveChanges();

            HoaDon hoadon1 = new HoaDon { NgayLapHD=System.DateTime.Now,TienHang=100000,TienKhuyenMai=0,PhiVanChuyen=0,DiaChiGiaoHang="abc xyz",TrangThai=1};
            HoaDon hoadon2 = new HoaDon { NgayLapHD = System.DateTime.Now, TienHang = 100000, TienKhuyenMai = 0, PhiVanChuyen = 0, DiaChiGiaoHang = "abc xyz", TrangThai = 2 };
            HoaDon hoadon3 = new HoaDon { NgayLapHD = new DateTime(2013,09,01), TienHang = 100000, TienKhuyenMai = 0, PhiVanChuyen = 0, DiaChiGiaoHang = "abc xyz", TrangThai = 1 };
            HoaDon hoadon4 = new HoaDon { NgayLapHD = new DateTime(2013, 07, 01), TienHang = 100000, TienKhuyenMai = 0, PhiVanChuyen = 0, DiaChiGiaoHang = "abc xyz", TrangThai = 1 };
            var hoadons = new List<HoaDon>
            {
                hoadon1,
                hoadon2,
                hoadon3,
                hoadon4
            };
            HoaDon[] hoadon = new HoaDon[50];
            for (int i = 0; i < 50; i++)
            {
                hoadon[i] = new HoaDon { NgayLapHD = System.DateTime.Now, TienHang = 100000*i, TienKhuyenMai = 0, PhiVanChuyen = 0, DiaChiGiaoHang = "abc xyz", TrangThai = (i%3)+1 };
                hoadons.Add(hoadon[i]);
            }
            hoadons.ForEach(h => context.HoaDons.Add(h));
            context.SaveChanges();
            ChiTietHoaDon cthd1 = new ChiTietHoaDon { SanPham = sanpham1, HoaDon = hoadon1, SoLuong = 1, ThanhTien = 100000 };
            ChiTietHoaDon cthd2 = new ChiTietHoaDon { SanPham = sanpham2, HoaDon = hoadon1, SoLuong = 1, ThanhTien = 100000 };
            ChiTietHoaDon cthd3 = new ChiTietHoaDon { SanPham = sanpham3, HoaDon = hoadon2, SoLuong = 1, ThanhTien = 100000 };
            var cthds = new List<ChiTietHoaDon>
            {
                cthd1,
                cthd2,
                cthd3
            };

            for (int i = 0; i < 100; i++)
            {
                ChiTietHoaDon cthd = new ChiTietHoaDon { SanPham = sanpham1, HoaDon = hoadon[i/2], SoLuong = 1, ThanhTien = 1000000 };
                cthds.Add(cthd);
            }
            cthds.ForEach(h => context.ChiTietHoaDon.Add(h));
            context.SaveChanges();
            LoaiKhachHang khachhangthuong = new LoaiKhachHang { TenLoai = "Khách hàng thường", Diem = 1, GhiChu ="ghi chư", GiamGia = 0 };
            LoaiKhachHang khachhangbac = new LoaiKhachHang { TenLoai = "Khách hàng Bạc", Diem = 1000, GhiChu = "Mỗi 1.000 đồng trên hóa đơn giá trị sản phẩm của khách hàng sẽ được quy đổi thành 01 điểm.",GiamGia=3 };
            LoaiKhachHang khachhangvang = new LoaiKhachHang { TenLoai = "Khách hàng Vàng", Diem = 5000, GhiChu = "Mỗi 1.000 đồng trên hóa đơn giá trị sản phẩm của khách hàng sẽ được quy đổi thành 01 điểm.",GiamGia=5 };
            LoaiKhachHang khachhangbachkim = new LoaiKhachHang { TenLoai = "Khách hàng Bạch Kim", Diem = 8000, GhiChu = "Mỗi 1.000 đồng trên hóa đơn giá trị sản phẩm của khách hàng sẽ được quy đổi thành 01 điểm.", GiamGia=7};
            LoaiKhachHang khachhangkimcuong = new LoaiKhachHang { TenLoai = "Khách hàng Kim Cương", Diem = 10000, GhiChu = "Mỗi 1.000 đồng trên hóa đơn giá trị sản phẩm của khách hàng sẽ được quy đổi thành 01 điểm.", GiamGia=10};
            var loaikhachhangs = new List<LoaiKhachHang>
            {
                khachhangthuong,
                khachhangbac,
                khachhangvang,
                khachhangbachkim,
                khachhangkimcuong
            };
            loaikhachhangs.ForEach(k=>context.LoaiKhachHangs.Add(k));
            context.SaveChanges();

            KhachHang khachhang1 = new KhachHang { TenDayDu="abc",DiaChi="hochiminh",SoDienThoai="1234567890",Email="example@yahoo.com",TenDangNhap="tendangnhap",LoaiKhachHang=khachhangbac,TinhThanh=angiang};
            KhachHang khachhang2 = new KhachHang { TenDayDu = "xyz", DiaChi = "hochiminh", SoDienThoai = "1234567890", Email = "example@yahoo.com", TenDangNhap = "quangpropk", LoaiKhachHang = khachhangthuong, MatKhau = "2251022057731868917119086224872421513662",TinhThanh=angiang,IsActive=true };
            var khachhangs = new List<KhachHang>
            {
                khachhang1,
                khachhang2
            };
            khachhangs.ForEach(k => context.KhachHangs.Add(k));
            context.SaveChanges();
        }
    }
}