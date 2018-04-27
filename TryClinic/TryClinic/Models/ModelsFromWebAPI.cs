using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace TryClinic.Models
{

    public class EmployeeFromWebAPI
    {
       // [Key]
        public int Id { get; set; }


        [Required(ErrorMessage = "Employee Name is Required")]
        [StringLength(10, MinimumLength = 3)]
        public string Name { get; set; }


        [Required(ErrorMessage = "Employee Name is Required")]
        //[DataType(DataType.Currency)]
        public decimal Salary { get; set; }

        [DataType(DataType.Date)]
        [Range(typeof(DateTime), "1-Jan-1940", "1-Jan-2018")]
        public DateTime? HireDate { get; set; }

      //  [ForeignKey("Department")]
        public int DepartmentId { get; set; }

        public virtual DepartmentFromWebAPI Department { get; set; }


    }

    public class DepartmentFromWebAPI
    {
       // [Key]
        public int Id { get; set; }

        [Required(ErrorMessage = "Department Name is Required")]
        [StringLength(50, MinimumLength = 2)]
        public string Name { get; set; }


        [Required(ErrorMessage = "Department Location is Required")]
        [StringLength(50, MinimumLength = 3)]
        public string Location { get; set; }

        public virtual ICollection<EmployeeFromWebAPI> Employees { get; set; }
    }
}