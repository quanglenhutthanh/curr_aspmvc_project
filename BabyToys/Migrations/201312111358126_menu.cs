namespace BabyToys.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class menu : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Menu",
                c => new
                {
                    Id = c.Int(nullable: false, identity: true),
                    TieuDe = c.String(),
                    NoiDung = c.String(),
                    link = c.String(),
                })
                .PrimaryKey(t => t.Id);

            DropTable("dbo.test");
        }

        public override void Down()
        {
            CreateTable(
                "dbo.test",
                c => new
                {
                    ID = c.Int(nullable: false, identity: true),
                })
                .PrimaryKey(t => t.ID);

            DropTable("dbo.Menu");
        }
    }
}
