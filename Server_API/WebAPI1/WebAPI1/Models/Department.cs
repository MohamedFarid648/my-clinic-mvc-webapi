using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace WebAPI1.Models
{
    public class Department
    {
        [Key]
        public int Id { get; set; }

        [Required(ErrorMessage ="Department Name is Required")]
        [StringLength(50,MinimumLength =2)]
        public string Name { get; set; }


        [Required(ErrorMessage = "Department Location is Required")]
        [StringLength(50, MinimumLength = 3)]
        public string Location { get; set; }

        public virtual ICollection<Employee> Employees { get; set; }
    }
}