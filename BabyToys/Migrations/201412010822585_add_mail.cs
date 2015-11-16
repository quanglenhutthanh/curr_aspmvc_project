namespace BabyToys.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class add_mail : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Mail",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        MailName = c.String(),
                    })
                .PrimaryKey(t => t.ID);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.Mail");
        }
    }
}
