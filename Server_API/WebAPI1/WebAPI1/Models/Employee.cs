using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace WebAPI1.Models
{
    public class Employee
    {
        [Key]
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

        [ForeignKey("Department")]
        public int DepartmentId { get; set; }

        public virtual Department Department { get;set;}


    }
}