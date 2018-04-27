namespace TryClinic.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InitialCreate6 : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.ContactUsForm", "Name", c => c.String(nullable: false));
            AlterColumn("dbo.ContactUsForm", "Message", c => c.String(nullable: false));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.ContactUsForm", "Message", c => c.String());
            AlterColumn("dbo.ContactUsForm", "Name", c => c.String());
        }
    }
}
