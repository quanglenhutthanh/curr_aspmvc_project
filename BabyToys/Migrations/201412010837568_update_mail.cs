namespace BabyToys.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class update_mail : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Mail", "Image", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Mail", "Image");
        }
    }
}
