namespace BabyToys.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class cap_nhat_ton_kho : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.TonKho", "Xuat", c => c.Int(nullable: false));
            AddColumn("dbo.TonKho", "Nhap", c => c.Int(nullable: false));
            DropColumn("dbo.TonKho", "HanhDong");
            DropColumn("dbo.TonKho", "ThayDoi");
        }
        
        public override void Down()
        {
            AddColumn("dbo.TonKho", "ThayDoi", c => c.Int(nullable: false));
            AddColumn("dbo.TonKho", "HanhDong", c => c.String());
            DropColumn("dbo.TonKho", "Nhap");
            DropColumn("dbo.TonKho", "Xuat");
        }
    }
}
