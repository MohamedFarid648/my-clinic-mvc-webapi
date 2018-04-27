namespace TryClinic.Migrations
{
    using Microsoft.AspNet.Identity.EntityFramework;
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<TryClinic.Models.ApplicationDbContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
        }

        protected override void Seed(TryClinic.Models.ApplicationDbContext context)
        {
            //Just in First Time

            if (context.Roles.Count() == 0)
            {
                context.Roles.Add(new IdentityRole("Admin"));
                context.Roles.Add(new IdentityRole("Doctor"));
                context.Roles.Add(new IdentityRole("Patient"));
                context.Roles.Add(new IdentityRole("Nurse"));
                context.Roles.Add(new IdentityRole("Guest"));
            }
            if (context.Clinics.Count() == 0)
            {
                context.Clinics.Add(new Models.Clinic() { Name = "MyFirstClinic", Address = "Cairo" });
            }
            //  This method will be called after migrating to the latest version.

            //  You can use the DbSet<T>.AddOrUpdate() helper extension method 
            //  to avoid creating duplicate seed data. E.g.
            //
            //    context.People.AddOrUpdate(
            //      p => p.FullName,
            //      new Person { FullName = "Andrew Peters" },
            //      new Person { FullName = "Brice Lambson" },
            //      new Person { FullName = "Rowan Miller" }
            //    );
            //
            //context.Clinics.Add(new Models.Clinic { Name = "MyClinic", Address = "Cairo" });

           // context.Database.ExecuteSqlCommand("ALTER TABLE dbo.Request ADD CONSTRAINT Doctor_Request FOREIGN KEY (DoctorId) REFERENCES dbo.AspNetUsers(Id) ON UPDATE NO ACTION ON DELETE SET NULL");

        }
    }
}
