namespace TryClinic.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InitialCreate5 : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.PatientForm", "PatientId", "dbo.AspNetUsers");
            DropIndex("dbo.PatientForm", new[] { "PatientId" });
            CreateTable(
                "dbo.ContactUsForm",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        MyDate = c.DateTime(nullable: false),
                        Message = c.String(),
                        Email = c.String(nullable: false),
                        Status = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            DropTable("dbo.PatientForm");
        }
        
        public override void Down()
        {
            CreateTable(
                "dbo.PatientForm",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        MyDate = c.DateTime(nullable: false),
                        Message = c.String(),
                        PatientId = c.String(maxLength: 128),
                    })
                .PrimaryKey(t => t.Id);
            
            DropTable("dbo.ContactUsForm");
            CreateIndex("dbo.PatientForm", "PatientId");
            AddForeignKey("dbo.PatientForm", "PatientId", "dbo.AspNetUsers", "Id");
        }
    }
}
