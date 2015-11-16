namespace BabyToys.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class update_mail_1 : DbMigration
    {
        public override void Up()
        {
            DropColumn("dbo.Mail", "Image");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Mail", "Image", c => c.String());
        }
    }
}
