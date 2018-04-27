namespace TryClinic.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InitialCreate1 : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Request", "DoctorID", "dbo.AspNetUsers");
            DropForeignKey("dbo.Request", "NurseID", "dbo.AspNetUsers");
            DropForeignKey("dbo.Request", "PatientID", "dbo.AspNetUsers");
            DropIndex("dbo.Request", new[] { "NurseID" });
            DropIndex("dbo.Request", new[] { "DoctorID" });
            DropIndex("dbo.Request", new[] { "PatientID" });
            DropPrimaryKey("dbo.Medicine_User");
            DropPrimaryKey("dbo.Request");
            AlterColumn("dbo.Request", "NurseID", c => c.String(nullable: false, maxLength: 128));
            AlterColumn("dbo.Request", "DoctorID", c => c.String(nullable: false, maxLength: 128));
            AlterColumn("dbo.Request", "PatientID", c => c.String(nullable: false, maxLength: 128));
            AddPrimaryKey("dbo.Medicine_User", new[] { "DoctorId", "MedicineId", "PatientId" });
            AddPrimaryKey("dbo.Request", new[] { "PatientID", "DoctorID", "NurseID" });
            CreateIndex("dbo.Request", "PatientID");
            CreateIndex("dbo.Request", "DoctorID");
            CreateIndex("dbo.Request", "NurseID");
            AddForeignKey("dbo.Request", "DoctorID", "dbo.AspNetUsers", "Id");//, cascadeDelete: true);
            AddForeignKey("dbo.Request", "NurseID", "dbo.AspNetUsers", "Id");//, cascadeDelete: true);
            AddForeignKey("dbo.Request", "PatientID", "dbo.AspNetUsers", "Id", cascadeDelete: true);
            DropColumn("dbo.Medicine_User", "Id");
            DropColumn("dbo.Request", "Id");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Request", "Id", c => c.Int(nullable: false, identity: true));
            AddColumn("dbo.Medicine_User", "Id", c => c.Int(nullable: false, identity: true));
            DropForeignKey("dbo.Request", "PatientID", "dbo.AspNetUsers");
            DropForeignKey("dbo.Request", "NurseID", "dbo.AspNetUsers");
            DropForeignKey("dbo.Request", "DoctorID", "dbo.AspNetUsers");
            DropIndex("dbo.Request", new[] { "NurseID" });
            DropIndex("dbo.Request", new[] { "DoctorID" });
            DropIndex("dbo.Request", new[] { "PatientID" });
            DropPrimaryKey("dbo.Request");
            DropPrimaryKey("dbo.Medicine_User");
            AlterColumn("dbo.Request", "PatientID", c => c.String(maxLength: 128));
            AlterColumn("dbo.Request", "DoctorID", c => c.String(maxLength: 128));
            AlterColumn("dbo.Request", "NurseID", c => c.String(maxLength: 128));
            AddPrimaryKey("dbo.Request", "Id");
            AddPrimaryKey("dbo.Medicine_User", "Id");
            CreateIndex("dbo.Request", "PatientID");
            CreateIndex("dbo.Request", "DoctorID");
            CreateIndex("dbo.Request", "NurseID");
            AddForeignKey("dbo.Request", "PatientID", "dbo.AspNetUsers", "Id");
            AddForeignKey("dbo.Request", "NurseID", "dbo.AspNetUsers", "Id");
            AddForeignKey("dbo.Request", "DoctorID", "dbo.AspNetUsers", "Id");
        }
    }
}
