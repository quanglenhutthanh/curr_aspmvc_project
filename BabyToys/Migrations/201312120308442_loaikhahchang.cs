namespace BabyToys.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class loaikhahchang : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.KhachHang", "IdLoaiKhachHang", "dbo.LoaiKhachHang");
            DropIndex("dbo.KhachHang", new[] { "IdLoaiKhachHang" });
            AlterColumn("dbo.KhachHang", "IdLoaiKhachHang", c => c.Int(nullable: false));
            CreateIndex("dbo.KhachHang", "IdLoaiKhachHang");
            AddForeignKey("dbo.KhachHang", "IdLoaiKhachHang", "dbo.LoaiKhachHang", "IdLoaiKhachHang", cascadeDelete: true);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.KhachHang", "IdLoaiKhachHang", "dbo.LoaiKhachHang");
            DropIndex("dbo.KhachHang", new[] { "IdLoaiKhachHang" });
            AlterColumn("dbo.KhachHang", "IdLoaiKhachHang", c => c.Int());
            CreateIndex("dbo.KhachHang", "IdLoaiKhachHang");
            AddForeignKey("dbo.KhachHang", "IdLoaiKhachHang", "dbo.LoaiKhachHang", "IdLoaiKhachHang");
        }
    }
}
