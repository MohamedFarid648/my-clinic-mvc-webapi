namespace WebAPI1.Migrations
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;
    using WebAPI1.Models;

    internal sealed class Configuration : DbMigrationsConfiguration<WebAPI1.Models.MyDBContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
        }

        protected override void Seed(WebAPI1.Models.MyDBContext context)
        {
            //  This method will be called after migrating to the latest version.

            //  You can use the DbSet<T>.AddOrUpdate() helper extension method 
            //  to avoid creating duplicate seed data.

            if (context.Departments.Count() == 0) { 
                context.Departments.Add(new Department() { Name = "SD", Location = "Cairo" });
                context.Departments.Add(new Department() { Name = "Gaming", Location = "Alex" });
                context.Departments.Add(new Department() { Name = "DataScience", Location = "Mansoura" });
            }

            if (context.Employees.Count() == 0)
            {

                context.Employees.Add(new Employee() { Name = "Ali", Salary = 2000m, DepartmentId = 1 });
                context.Employees.Add(new Employee() { Name = "Ahmed", Salary = 8000m, DepartmentId = 2 });
                context.Employees.Add(new Employee() { Name = "Mostafa", Salary = 7000m, DepartmentId = 1 });
                context.Employees.Add(new Employee() { Name = "Amr", Salary = 7000m, DepartmentId = 2});
                context.Employees.Add(new Employee() { Name = "Khalid", Salary = 6000m, DepartmentId = 3 });
                context.Employees.Add(new Employee() { Name = "Hussien", Salary = 7000m, DepartmentId = 3 });




            }


        }
        }
}
