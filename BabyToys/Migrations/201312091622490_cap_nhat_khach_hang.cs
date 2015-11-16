namespace BabyToys.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class cap_nhat_khach_hang : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.KhachHang", "NgayTao", c => c.DateTime(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.KhachHang", "NgayTao");
        }
    }
}
