using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;
using System.Linq;
using System.Web;

namespace BabyToys.Models
{
    public class DatabaseContext: DbContext
    {
        public DatabaseContext()
            : base("BabyToysConnectionString")
        {
        }
        public DbSet<SanPham> SanPhams { get; set; }
        public DbSet<KhuyenMai> KhuyenMais { get; set; }
        public DbSet<DanhGia> DanhGia { get; set; }
        public DbSet<ChiTietHoaDon> ChiTietHoaDon { get; set; }
        public DbSet<DanhMuc> DanhMucs { get; set; }
        public DbSet<HinhAnh> HinhAnhs { get; set; }
        public DbSet<HoaDon> HoaDons { get; set; }
        public DbSet<Quyen> Quyens { get; set; }
        public DbSet<ChucNang> ChucNangs { get; set; }
        //public DbSet<Permission> Permission { get; set; }
        public DbSet<LoaiKhachHang> LoaiKhachHangs { get; set; }
        public DbSet<KhachHang> KhachHangs { get; set; }
        public DbSet<DiemTichLuy> DiemTichLuy { get; set; }
        public DbSet<TinhThanh> TinhThanhs { get; set; }
        public DbSet<ThuongHieu> ThuongHieus { get; set; }
        public DbSet<DoTuoi> DoTuois { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<Log> Logs { get; set; }
        public DbSet<PhieuNhap> PhieuNhaps { get; set; }
        public DbSet<ChiTietPhieuNhap> ChiTietPhieuNhaps { get; set; }
        public DbSet<TonKho> TonKhos { get; set; }
        public DbSet<Menu> Menus { get; set; }
        public DbSet<Setting> Settings { get; set; }

        public DbSet<Mail> Mails { get; set; }
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();
            modelBuilder.Entity<DanhMuc>()
               .HasOptional(p => p.DanhMucCha)
               .WithMany(p => p.DanhMucCons);

            modelBuilder.Entity<ChucNang>()
              .HasMany(c => c.Roles).WithMany(i => i.ChucNangs)
              .Map(t => t.MapLeftKey("PermissionId")
                  .MapRightKey("RoleId")
                  .ToTable("PermissonRole"));
        }
    }
}