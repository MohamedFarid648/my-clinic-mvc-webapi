namespace TryClinic.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InitialCreate4 : DbMigration
    {
        public override void Up()
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
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.AspNetUsers", t => t.PatientId)
                .Index(t => t.PatientId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.PatientForm", "PatientId", "dbo.AspNetUsers");
            DropIndex("dbo.PatientForm", new[] { "PatientId" });
            DropTable("dbo.PatientForm");
        }
    }
}
