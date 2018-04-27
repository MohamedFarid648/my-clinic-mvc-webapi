using System.Data.Entity;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System;
using System.Data.Entity.Validation;
using System.Linq;
using System.Web.Mvc;
using System.Web.Services.Description;
using System.Runtime.InteropServices;

namespace TryClinic.Models
{
    /*
         Inheritance in EF:
        http://www.entityframeworktutorial.net/code-first/inheritance-strategy-in-code-first.aspx

        Table by hirarchy:
        https://weblogs.asp.net/manavi/inheritance-mapping-strategies-with-entity-framework-code-first-ctp5-part-1-table-per-hierarchy-tph
        One Table in DB for All Types

        Table By Class
        https://weblogs.asp.net/manavi/inheritance-mapping-strategies-with-entity-framework-code-first-ctp5-part-2-table-per-type-tpt
         
    */

    public enum Gender { Male, Female }
    public enum Status { Seen, UnSeen };

    // You can add profile data for the user by adding more properties to your ApplicationUser class, please visit https://go.microsoft.com/fwlink/?LinkID=317594 to learn more.
    public class ApplicationUser : IdentityUser
    {
        [StringLength(10, ErrorMessage = "First Name between 10 and 3", MinimumLength = 3)]
        [Required(ErrorMessage = "First Name is Required")]
        public string FirstName { get; set; }

        [StringLength(10, ErrorMessage = "Last Name between 10 and 3", MinimumLength = 3)]
        [Required(ErrorMessage = "Last Name is Required")]
        public string LastName { get; set; }

        [Required(ErrorMessage = "Date Of Birth  is Required")]
        public DateTime DateOfBirth { get; set; }

        [Required(ErrorMessage = "Gender  is Required")]
        public Gender Gender { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public ApplicationUser()
        {
            //Clinic = new Clinic();
        }



        [ForeignKey("Clinic")]
        public int Clinic_Id { get; set; }
        public virtual Clinic Clinic { get; set; }



        public async Task<ClaimsIdentity> GenerateUserIdentityAsync(UserManager<ApplicationUser> manager)
        {
            // Note the authenticationType must match the one defined in CookieAuthenticationOptions.AuthenticationType
            var userIdentity = await manager.CreateIdentityAsync(this, DefaultAuthenticationTypes.ApplicationCookie);
            // Add custom user claims here
            return userIdentity;
        }
    }

    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext()
            : base("DefaultConnection", throwIfV1Schema: false)
        {
        }

        public static ApplicationDbContext Create()
        {
            return new ApplicationDbContext();
        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            //SP for all entities
            // modelBuilder.Types().Configure(t => t.MapToStoredProcedures());
            //Don't Forget delete SP Update For ApplicationUser(because it make PasswordHash=Null if you didn't send it

            //SP for one entity
            /* modelBuilder.Entity<Medicine>().MapToStoredProcedures();
             modelBuilder.Entity<Clinic>().MapToStoredProcedures();
             modelBuilder.Entity<Request>().MapToStoredProcedures();
             modelBuilder.Entity<Medicine_User>().MapToStoredProcedures();
            */



            //modelBuilder.Entity<ApplicationUser>().Property(p => p.DateOfBirth).HasColumnType("datetime2");

            base.OnModelCreating(modelBuilder);
        }

        public virtual DbSet<Clinic> Clinics { get; set; }
        public virtual DbSet<Request> Requests { get; set; }
        public virtual DbSet<Medicine> Medicines { get; set; }
        public virtual DbSet<Medicine_User> Medicine_Users { get; set; }
        public virtual DbSet<ContactUsForm> ContactUsForms { get; set; }

      }





    [Table("Medicine")]
    public partial class Medicine
    {
        // [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Medicine()
        {
            Medicine_Users = new HashSet<Medicine_User>();
        }

        [Key]
        public int Id { get; set; }

        [StringLength(30)]
        public string Name { get; set; }

        //[System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Medicine_User> Medicine_Users { get; set; }
    }


    [Table("Request")]
    public partial class Request
    {



        // [Index(IsUnique = true)]
        //[DatabaseGeneratedAttribute(DatabaseGeneratedOption.Identity)]
        [Key]
        public int Id { get; set; }


        [StringLength(30)]
        public string Name { get; set; }

        public DateTime Date { get; set; }


        [Index("ReqUser", 1, IsUnique = true)]
        // [Key, Column(Order = 2)]
        [ForeignKey("Nurse")]
        [StringLength(128)]
        public string NurseID { get; set; }

        [Index("ReqUser", 2, IsUnique = true)]
        //[Key, Column(Order = 1)]
        [ForeignKey("Doctor")]
        [StringLength(128)]
        public string DoctorID { get; set; }


        //[Key, Column(Order = 0)]
        [Index("ReqUser", 3, IsUnique = true)]
        [ForeignKey("Patient")]
        [StringLength(128)]
        public string PatientID { get; set; }

        public virtual ApplicationUser Nurse { get; set; }//Nurse

        public virtual ApplicationUser Doctor { get; set; }//Doctor

        public virtual ApplicationUser Patient { get; set; }//Patient
    }


    [Table("Clinic")]
    public partial class Clinic
    {
        // [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Clinic()
        {
            Users = new HashSet<ApplicationUser>();
        }

        public int Id { get; set; }

        [StringLength(30)]
        public string Name { get; set; }

        [StringLength(30)]
        public string Address { get; set; }

        //[System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<ApplicationUser> Users { get; set; }
    }




    public partial class Medicine_User
    {

        // [Index(IsUnique =true)]
        //[DatabaseGeneratedAttribute(DatabaseGeneratedOption.Identity)]
        [Key]
        public int Id { get; set; }

        //[Key, Column(Order = 2)]
        [Index("MedUser", 1, IsUnique = true)]
        [ForeignKey("Patient")]
        [Required]
        [StringLength(128)]
        public string PatientId { get; set; }

        //[Key,Column(Order =0)]
        [Index("MedUser", 2, IsUnique = true)]
        [ForeignKey("Doctor")]
        [Required]
        [StringLength(128)]
        public string DoctorId { get; set; }

        // [Key,Column(Order =1)]
        [Index("MedUser", 3, IsUnique = true)]
        [Required]
        [ForeignKey("Medicine")]
        public int MedicineId { get; set; }

        public int? Quantity { get; set; }


        public virtual ApplicationUser Patient { get; set; }//Patient

        public virtual ApplicationUser Doctor { get; set; }//Doctor

        public virtual Medicine Medicine { get; set; }



    }


    [Table("ContactUsForm")]
    public partial class ContactUsForm
    {
        [Key]
        public int Id { get; set; }

        [Required(ErrorMessage = "Your Name is Required ^_^")]
        public string Name { get; set; }

        [Display(Name = "Date")]
        public DateTime MyDate { get; set; }

        [Required(ErrorMessage = "Message can't be Empty ^_^")]
        public string Message { get; set; }



        [RegularExpression(".+\\@.+\\..+", ErrorMessage = "Enter a valid Email please ^_^")]
        [Required(ErrorMessage = "Enter your Email Please ^_^")]
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }


        public Status Status { get; set; }


    }





}