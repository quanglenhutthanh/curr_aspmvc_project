namespace BabyToys.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class menu_update : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Menu", "danhmuc", c => c.Int());
            AddColumn("dbo.Menu", "type", c => c.Int());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Menu", "type");
            DropColumn("dbo.Menu", "danhmuc");
        }
    }
}
