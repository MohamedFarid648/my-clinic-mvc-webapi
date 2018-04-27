namespace TryClinic.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InitialCreate2 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Medicine_User", "Id", c => c.Int(nullable: false, identity: true));
            AddColumn("dbo.Request", "Id", c => c.Int(nullable: false, identity: true));
            CreateIndex("dbo.Medicine_User", "Id", unique: true);
            CreateIndex("dbo.Request", "Id", unique: true);
        }
        
        public override void Down()
        {
            DropIndex("dbo.Request", new[] { "Id" });
            DropIndex("dbo.Medicine_User", new[] { "Id" });
            DropColumn("dbo.Request", "Id");
            DropColumn("dbo.Medicine_User", "Id");
        }
    }
}
