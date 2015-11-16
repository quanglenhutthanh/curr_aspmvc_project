namespace BabyToys.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class cap_nhat_khachhang_user : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.User", "NgayTao", c => c.DateTime(nullable: false));
            DropColumn("dbo.KhachHang", "IsOnline");
            DropColumn("dbo.User", "IsOnline");
        }
        
        public override void Down()
        {
            AddColumn("dbo.User", "IsOnline", c => c.Boolean(nullable: false));
            AddColumn("dbo.KhachHang", "IsOnline", c => c.Boolean(nullable: false));
            DropColumn("dbo.User", "NgayTao");
        }
    }
}
