namespace BabyToys.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class cap_nhat_hoa_don1 : DbMigration
    {
        public override void Up()
        {
            DropColumn("dbo.HoaDon", "IdTinhThanh");
            RenameColumn(table: "dbo.HoaDon", name: "TinhThanh_IdTinhThanh", newName: "IdTinhThanh");
            AlterColumn("dbo.HoaDon", "IdTinhThanh", c => c.Int());
        }
        
        public override void Down()
        {
            AlterColumn("dbo.HoaDon", "IdTinhThanh", c => c.String());
            RenameColumn(table: "dbo.HoaDon", name: "IdTinhThanh", newName: "TinhThanh_IdTinhThanh");
            AddColumn("dbo.HoaDon", "IdTinhThanh", c => c.String());
        }
    }
}
