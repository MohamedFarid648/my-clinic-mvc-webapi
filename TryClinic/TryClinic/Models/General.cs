using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace TryClinic.Models
{
    public class General
    {
        public static ICollection<ApplicationUser> getUsers(string role)
        {
            ApplicationDbContext db = new ApplicationDbContext();


            var UserRole = db.Roles.FirstOrDefault(r => r.Name.Equals(role));
            /*
             Go first to 
             OnModelCreating in ApplicationDbContext and run some queries on migration history table
             */
            /*
             
             SELECT TOP (1) 
            [Extent1].[Id] AS [Id], 
            [Extent1].[Name] AS [Name]
            FROM [dbo].[AspNetRoles] AS [Extent1]
            WHERE N'Doctor' = [Extent1].[Name]
             */
            ICollection<ApplicationUser> users = (from u in db.Users
                                                  where u.Roles.Select(x => x.RoleId).Contains(UserRole.Id)
                                                  select u).ToList();
            return users;

        }




    }

    public static class CheckUserInRole
    {


       /* public static MvcHtmlString If(this MvcHtmlString value, bool evaluation)
        {
            return evaluation ? value : MvcHtmlString.Empty;
        }*/
    }

}