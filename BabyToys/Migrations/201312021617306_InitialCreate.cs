namespace BabyToys.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InitialCreate : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.SanPham",
                c => new
                    {
                        IdSanPham = c.Int(nullable: false, identity: true),
                        MaSP = c.String(nullable: false),
                        TenSP = c.String(nullable: false),
                        HinhDaiDien = c.String(),
                        Thumbnail = c.String(),
                        DanhGia = c.Int(nullable: false),
                        MoTaSP = c.String(),
                        HienThi = c.Boolean(nullable: false),
                        ChatLieu = c.String(),
                        MauSac = c.String(),
                        KichThuoc = c.String(),
                        LuotXem = c.Int(),
                        IdDanhMuc = c.Int(nullable: false),
                        IdThuongHieu = c.Int(nullable: false),
                        Gia = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.IdSanPham)
                .ForeignKey("dbo.DanhMuc", t => t.IdDanhMuc, cascadeDelete: true)
                .ForeignKey("dbo.ThuongHieu", t => t.IdThuongHieu, cascadeDelete: true)
                .Index(t => t.IdDanhMuc)
                .Index(t => t.IdThuongHieu);
            
            CreateTable(
                "dbo.ChiTietHoaDon",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        SoLuong = c.Int(nullable: false),
                        ThanhTien = c.Decimal(nullable: false, precision: 18, scale: 2),
                        IdHoaDon = c.Int(nullable: false),
                        IdSanPham = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.HoaDon", t => t.IdHoaDon, cascadeDelete: true)
                .ForeignKey("dbo.SanPham", t => t.IdSanPham, cascadeDelete: true)
                .Index(t => t.IdHoaDon)
                .Index(t => t.IdSanPham);
            
            CreateTable(
                "dbo.HoaDon",
                c => new
                    {
                        IdHoaDon = c.Int(nullable: false, identity: true),
                        NgayLapHD = c.DateTime(nullable: false),
                        TienHang = c.Decimal(nullable: false, precision: 18, scale: 2),
                        PhiVanChuyen = c.Decimal(nullable: false, precision: 18, scale: 2),
                        TienKhuyenMai = c.Decimal(nullable: false, precision: 18, scale: 2),
                        DiaChiGiaoHang = c.String(),
                        TenNguoiNhan = c.String(),
                        Email = c.String(),
                        SoDienThoai = c.String(),
                        IdTinhThanh = c.String(),
                        TrangThai = c.Int(nullable: false),
                        IdKhachHang = c.Int(),
                        TinhThanh_IdTinhThanh = c.Int(),
                    })
                .PrimaryKey(t => t.IdHoaDon)
                .ForeignKey("dbo.KhachHang", t => t.IdKhachHang)
                .ForeignKey("dbo.TinhThanh", t => t.TinhThanh_IdTinhThanh)
                .Index(t => t.IdKhachHang)
                .Index(t => t.TinhThanh_IdTinhThanh);
            
            CreateTable(
                "dbo.KhachHang",
                c => new
                    {
                        IdKhachHang = c.Int(nullable: false, identity: true),
                        TenDangNhap = c.String(),
                        TenDayDu = c.String(),
                        MatKhau = c.String(),
                        SoDienThoai = c.String(),
                        Email = c.String(),
                        DiaChi = c.String(),
                        GhiChu = c.String(),
                        IsOnline = c.Boolean(nullable: false),
                        IsActive = c.Boolean(nullable: false),
                        IdTinhThanh = c.Int(nullable: false),
                        IdLoaiKhachHang = c.Int(),
                    })
                .PrimaryKey(t => t.IdKhachHang)
                .ForeignKey("dbo.TinhThanh", t => t.IdTinhThanh, cascadeDelete: true)
                .ForeignKey("dbo.LoaiKhachHang", t => t.IdLoaiKhachHang)
                .Index(t => t.IdTinhThanh)
                .Index(t => t.IdLoaiKhachHang);
            
            CreateTable(
                "dbo.TinhThanh",
                c => new
                    {
                        IdTinhThanh = c.Int(nullable: false, identity: true),
                        TenTinh = c.String(nullable: false),
                        PhiVanChuyen = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.IdTinhThanh);
            
            CreateTable(
                "dbo.LoaiKhachHang",
                c => new
                    {
                        IdLoaiKhachHang = c.Int(nullable: false, identity: true),
                        TenLoai = c.String(nullable: false),
                        Diem = c.Int(nullable: false),
                        GiamGia = c.Int(nullable: false),
                        GhiChu = c.String(),
                    })
                .PrimaryKey(t => t.IdLoaiKhachHang);
            
            CreateTable(
                "dbo.DiemTichLuy",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        IdKhachHang = c.Int(nullable: false),
                        ThoiGian = c.DateTime(nullable: false),
                        Diem = c.Int(nullable: false),
                        IdHoaDon = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.HoaDon", t => t.IdHoaDon, cascadeDelete: true)
                .ForeignKey("dbo.KhachHang", t => t.IdKhachHang, cascadeDelete: true)
                .Index(t => t.IdHoaDon)
                .Index(t => t.IdKhachHang);
            
            CreateTable(
                "dbo.DanhMuc",
                c => new
                    {
                        IdDanhMuc = c.Int(nullable: false, identity: true),
                        TenDanhMuc = c.String(),
                        MoTa = c.String(),
                        GhiChu = c.String(),
                        IsActive = c.Boolean(nullable: false),
                        DanhMucCha_IdDanhMuc = c.Int(),
                    })
                .PrimaryKey(t => t.IdDanhMuc)
                .ForeignKey("dbo.DanhMuc", t => t.DanhMucCha_IdDanhMuc)
                .Index(t => t.DanhMucCha_IdDanhMuc);
            
            CreateTable(
                "dbo.HinhAnh",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        AnhGoc = c.String(),
                        Thumbnail = c.String(),
                        SanPham_IdSanPham = c.Int(),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.SanPham", t => t.SanPham_IdSanPham)
                .Index(t => t.SanPham_IdSanPham);
            
            CreateTable(
                "dbo.ThuongHieu",
                c => new
                    {
                        IdThuongHieu = c.Int(nullable: false, identity: true),
                        TenThuongHieu = c.String(),
                        QuocGia = c.String(),
                        Logo = c.String(),
                        ThongTin = c.String(),
                        GhiChu = c.String(),
                        IsActive = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.IdThuongHieu);
            
            CreateTable(
                "dbo.DanhGia",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        HoTen = c.String(nullable: false),
                        Email = c.String(nullable: false),
                        BinhLuan = c.String(),
                        DiemDanhGia = c.Int(nullable: false),
                        ThoiGian = c.DateTime(nullable: false),
                        IdSanPham = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.SanPham", t => t.IdSanPham, cascadeDelete: true)
                .Index(t => t.IdSanPham);
            
            CreateTable(
                "dbo.KhuyenMai",
                c => new
                    {
                        IdKhuyenMai = c.Int(nullable: false, identity: true),
                        TuNgay = c.DateTime(nullable: false),
                        DenNgay = c.DateTime(nullable: false),
                        PhanTramGiam = c.Int(nullable: false),
                        IdSanPham = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.IdKhuyenMai)
                .ForeignKey("dbo.SanPham", t => t.IdSanPham, cascadeDelete: true)
                .Index(t => t.IdSanPham);
            
            CreateTable(
                "dbo.ChiTietPhieuNhap",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        SoLuong = c.Int(nullable: false),
                        ThanhTien = c.Decimal(nullable: false, precision: 18, scale: 2),
                        IdPhieuNhap = c.Int(nullable: false),
                        IdSanPham = c.Int(nullable: false),
                        GiaNhap = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.PhieuNhap", t => t.IdPhieuNhap, cascadeDelete: true)
                .ForeignKey("dbo.SanPham", t => t.IdSanPham, cascadeDelete: true)
                .Index(t => t.IdPhieuNhap)
                .Index(t => t.IdSanPham);
            
            CreateTable(
                "dbo.PhieuNhap",
                c => new
                    {
                        IdPhieuNhap = c.Int(nullable: false, identity: true),
                        NgayLap = c.DateTime(nullable: false),
                        User_Id = c.Int(),
                    })
                .PrimaryKey(t => t.IdPhieuNhap)
                .ForeignKey("dbo.User", t => t.User_Id)
                .Index(t => t.User_Id);
            
            CreateTable(
                "dbo.User",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        TenDangNhap = c.String(),
                        MatKhau = c.String(),
                        TenDayDu = c.String(),
                        SoDienThoai = c.String(),
                        Email = c.String(),
                        DiaChi = c.String(),
                        IdQuyen = c.Int(nullable: false),
                        IsActive = c.Boolean(nullable: false),
                        IsOnline = c.Boolean(nullable: false),
                        IdTinhThanh = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.TinhThanh", t => t.IdTinhThanh)
                .ForeignKey("dbo.Quyen", t => t.IdQuyen, cascadeDelete: true)
                .Index(t => t.IdTinhThanh)
                .Index(t => t.IdQuyen);
            
            CreateTable(
                "dbo.Quyen",
                c => new
                    {
                        IdQuyen = c.Int(nullable: false, identity: true),
                        TenQuyen = c.String(nullable: false, maxLength: 30),
                    })
                .PrimaryKey(t => t.IdQuyen);
            
            CreateTable(
                "dbo.ChucNang",
                c => new
                    {
                        IdChucNang = c.Int(nullable: false, identity: true),
                        TenChucNang = c.String(),
                        MaChucNang = c.String(),
                    })
                .PrimaryKey(t => t.IdChucNang);
            
            CreateTable(
                "dbo.TonKho",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        IdSanPham = c.Int(nullable: false),
                        HanhDong = c.String(),
                        ThayDoi = c.Int(nullable: false),
                        ThoiGian = c.DateTime(nullable: false),
                        SoLuong = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.SanPham", t => t.IdSanPham, cascadeDelete: true)
                .Index(t => t.IdSanPham);
            
            CreateTable(
                "dbo.DoTuoi",
                c => new
                    {
                        IdDoTuoi = c.Int(nullable: false, identity: true),
                        TuTuoi = c.Int(nullable: false),
                        DenTuoi = c.Int(nullable: false),
                        GhiChu = c.String(),
                    })
                .PrimaryKey(t => t.IdDoTuoi);
            
            CreateTable(
                "dbo.Log",
                c => new
                    {
                        IdLog = c.Int(nullable: false, identity: true),
                        Controller = c.String(),
                        Action = c.String(),
                        IP = c.String(),
                        Time = c.DateTime(nullable: false),
                        User_Id = c.Int(),
                    })
                .PrimaryKey(t => t.IdLog)
                .ForeignKey("dbo.User", t => t.User_Id)
                .Index(t => t.User_Id);
            
            CreateTable(
                "dbo.test",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                    })
                .PrimaryKey(t => t.ID);
            
            CreateTable(
                "dbo.PermissonRole",
                c => new
                    {
                        PermissionId = c.Int(nullable: false),
                        RoleId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.PermissionId, t.RoleId })
                .ForeignKey("dbo.ChucNang", t => t.PermissionId, cascadeDelete: true)
                .ForeignKey("dbo.Quyen", t => t.RoleId, cascadeDelete: true)
                .Index(t => t.PermissionId)
                .Index(t => t.RoleId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.PermissonRole", "RoleId", "dbo.Quyen");
            DropForeignKey("dbo.PermissonRole", "PermissionId", "dbo.ChucNang");
            DropForeignKey("dbo.Log", "User_Id", "dbo.User");
            DropForeignKey("dbo.TonKho", "IdSanPham", "dbo.SanPham");
            DropForeignKey("dbo.User", "IdQuyen", "dbo.Quyen");
            DropForeignKey("dbo.User", "IdTinhThanh", "dbo.TinhThanh");
            DropForeignKey("dbo.PhieuNhap", "User_Id", "dbo.User");
            DropForeignKey("dbo.ChiTietPhieuNhap", "IdSanPham", "dbo.SanPham");
            DropForeignKey("dbo.ChiTietPhieuNhap", "IdPhieuNhap", "dbo.PhieuNhap");
            DropForeignKey("dbo.KhuyenMai", "IdSanPham", "dbo.SanPham");
            DropForeignKey("dbo.DanhGia", "IdSanPham", "dbo.SanPham");
            DropForeignKey("dbo.HinhAnh", "SanPham_IdSanPham", "dbo.SanPham");
            DropForeignKey("dbo.DanhMuc", "DanhMucCha_IdDanhMuc", "dbo.DanhMuc");
            DropForeignKey("dbo.DiemTichLuy", "IdKhachHang", "dbo.KhachHang");
            DropForeignKey("dbo.DiemTichLuy", "IdHoaDon", "dbo.HoaDon");
            DropForeignKey("dbo.KhachHang", "IdLoaiKhachHang", "dbo.LoaiKhachHang");
            DropForeignKey("dbo.KhachHang", "IdTinhThanh", "dbo.TinhThanh");
            DropForeignKey("dbo.HoaDon", "TinhThanh_IdTinhThanh", "dbo.TinhThanh");
            DropForeignKey("dbo.HoaDon", "IdKhachHang", "dbo.KhachHang");
            DropForeignKey("dbo.ChiTietHoaDon", "IdSanPham", "dbo.SanPham");
            DropForeignKey("dbo.ChiTietHoaDon", "IdHoaDon", "dbo.HoaDon");
            DropForeignKey("dbo.SanPham", "IdThuongHieu", "dbo.ThuongHieu");
            DropForeignKey("dbo.SanPham", "IdDanhMuc", "dbo.DanhMuc");
            DropIndex("dbo.PermissonRole", new[] { "RoleId" });
            DropIndex("dbo.PermissonRole", new[] { "PermissionId" });
            DropIndex("dbo.Log", new[] { "User_Id" });
            DropIndex("dbo.TonKho", new[] { "IdSanPham" });
            DropIndex("dbo.User", new[] { "IdQuyen" });
            DropIndex("dbo.User", new[] { "IdTinhThanh" });
            DropIndex("dbo.PhieuNhap", new[] { "User_Id" });
            DropIndex("dbo.ChiTietPhieuNhap", new[] { "IdSanPham" });
            DropIndex("dbo.ChiTietPhieuNhap", new[] { "IdPhieuNhap" });
            DropIndex("dbo.KhuyenMai", new[] { "IdSanPham" });
            DropIndex("dbo.DanhGia", new[] { "IdSanPham" });
            DropIndex("dbo.HinhAnh", new[] { "SanPham_IdSanPham" });
            DropIndex("dbo.DanhMuc", new[] { "DanhMucCha_IdDanhMuc" });
            DropIndex("dbo.DiemTichLuy", new[] { "IdKhachHang" });
            DropIndex("dbo.DiemTichLuy", new[] { "IdHoaDon" });
            DropIndex("dbo.KhachHang", new[] { "IdLoaiKhachHang" });
            DropIndex("dbo.KhachHang", new[] { "IdTinhThanh" });
            DropIndex("dbo.HoaDon", new[] { "TinhThanh_IdTinhThanh" });
            DropIndex("dbo.HoaDon", new[] { "IdKhachHang" });
            DropIndex("dbo.ChiTietHoaDon", new[] { "IdSanPham" });
            DropIndex("dbo.ChiTietHoaDon", new[] { "IdHoaDon" });
            DropIndex("dbo.SanPham", new[] { "IdThuongHieu" });
            DropIndex("dbo.SanPham", new[] { "IdDanhMuc" });
            DropTable("dbo.PermissonRole");
            DropTable("dbo.test");
            DropTable("dbo.Log");
            DropTable("dbo.DoTuoi");
            DropTable("dbo.TonKho");
            DropTable("dbo.ChucNang");
            DropTable("dbo.Quyen");
            DropTable("dbo.User");
            DropTable("dbo.PhieuNhap");
            DropTable("dbo.ChiTietPhieuNhap");
            DropTable("dbo.KhuyenMai");
            DropTable("dbo.DanhGia");
            DropTable("dbo.ThuongHieu");
            DropTable("dbo.HinhAnh");
            DropTable("dbo.DanhMuc");
            DropTable("dbo.DiemTichLuy");
            DropTable("dbo.LoaiKhachHang");
            DropTable("dbo.TinhThanh");
            DropTable("dbo.KhachHang");
            DropTable("dbo.HoaDon");
            DropTable("dbo.ChiTietHoaDon");
            DropTable("dbo.SanPham");
        }
    }
}
