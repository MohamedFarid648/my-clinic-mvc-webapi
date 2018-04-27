namespace TryClinic.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InitialCreate3 : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Request", "DoctorID", "dbo.AspNetUsers");
            DropForeignKey("dbo.Request", "NurseID", "dbo.AspNetUsers");
            DropForeignKey("dbo.Request", "PatientID", "dbo.AspNetUsers");
            DropIndex("dbo.Medicine_User", new[] { "DoctorId" });
            DropIndex("dbo.Medicine_User", new[] { "MedicineId" });
            DropIndex("dbo.Medicine_User", new[] { "PatientId" });
            DropIndex("dbo.Medicine_User", new[] { "Id" });
            DropIndex("dbo.Request", new[] { "PatientID" });
            DropIndex("dbo.Request", new[] { "DoctorID" });
            DropIndex("dbo.Request", new[] { "NurseID" });
            DropIndex("dbo.Request", new[] { "Id" });
            DropPrimaryKey("dbo.Medicine_User");
            DropPrimaryKey("dbo.Request");
            AlterColumn("dbo.Request", "PatientID", c => c.String(maxLength: 128));
            AlterColumn("dbo.Request", "DoctorID", c => c.String(maxLength: 128));
            AlterColumn("dbo.Request", "NurseID", c => c.String(maxLength: 128));
            AddPrimaryKey("dbo.Medicine_User", "Id");
            AddPrimaryKey("dbo.Request", "Id");
            CreateIndex("dbo.Medicine_User", new[] { "PatientId", "DoctorId", "MedicineId" }, unique: true, name: "MedUser");
            CreateIndex("dbo.Request", new[] { "NurseID", "DoctorID", "PatientID" }, unique: true, name: "ReqUser");
            AddForeignKey("dbo.Request", "DoctorID", "dbo.AspNetUsers", "Id");
            AddForeignKey("dbo.Request", "NurseID", "dbo.AspNetUsers", "Id");
            AddForeignKey("dbo.Request", "PatientID", "dbo.AspNetUsers", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Request", "PatientID", "dbo.AspNetUsers");
            DropForeignKey("dbo.Request", "NurseID", "dbo.AspNetUsers");
            DropForeignKey("dbo.Request", "DoctorID", "dbo.AspNetUsers");
            DropIndex("dbo.Request", "ReqUser");
            DropIndex("dbo.Medicine_User", "MedUser");
            DropPrimaryKey("dbo.Request");
            DropPrimaryKey("dbo.Medicine_User");
            AlterColumn("dbo.Request", "NurseID", c => c.String(nullable: false, maxLength: 128));
            AlterColumn("dbo.Request", "DoctorID", c => c.String(nullable: false, maxLength: 128));
            AlterColumn("dbo.Request", "PatientID", c => c.String(nullable: false, maxLength: 128));
            AddPrimaryKey("dbo.Request", new[] { "PatientID", "DoctorID", "NurseID" });
            AddPrimaryKey("dbo.Medicine_User", new[] { "DoctorId", "MedicineId", "PatientId" });
            CreateIndex("dbo.Request", "Id", unique: true);
            CreateIndex("dbo.Request", "NurseID");
            CreateIndex("dbo.Request", "DoctorID");
            CreateIndex("dbo.Request", "PatientID");
            CreateIndex("dbo.Medicine_User", "Id", unique: true);
            CreateIndex("dbo.Medicine_User", "PatientId");
            CreateIndex("dbo.Medicine_User", "MedicineId");
            CreateIndex("dbo.Medicine_User", "DoctorId");
            AddForeignKey("dbo.Request", "PatientID", "dbo.AspNetUsers", "Id", cascadeDelete: true);
            AddForeignKey("dbo.Request", "NurseID", "dbo.AspNetUsers", "Id", cascadeDelete: true);
            AddForeignKey("dbo.Request", "DoctorID", "dbo.AspNetUsers", "Id", cascadeDelete: true);
        }
    }
}
