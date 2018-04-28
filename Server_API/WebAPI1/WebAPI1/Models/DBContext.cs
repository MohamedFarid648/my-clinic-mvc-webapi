
using System.Data.Entity;

namespace WebAPI1.Models
{
    public class MyDBContext: DbContext
    {

        public MyDBContext():base("MyDBContext") {


        }

        public DbSet<Employee> Employees { get; set; }
        public DbSet<Department> Departments { get; set; }

    }
}