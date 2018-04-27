using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using TryClinic.Models;

namespace TryClinic.Controllers
{
    public class AdminController : Controller
    {
        ApplicationDbContext db = new ApplicationDbContext();
        // GET: Admin
        [Authorize(Roles = "Admin")]
        public ActionResult AllDoctors()
        {

             return View(General.getUsers("Doctor"));
        }
        [Authorize(Roles = "Admin")]
        public ActionResult AddDoctor()
        {

            return RedirectToAction("Register", "Account", new { Role = "Doctor" });
        }
        [Authorize(Roles = "Admin")]
        public ActionResult AllNurses()
        {/*
            var role = db.Roles.FirstOrDefault(r => r.Name.Equals("Nurse"));
            List<ApplicationUser> Nurses = (from u in db.Users
                                            where u.Roles.Select(x => x.RoleId).Contains(role.Id)
                                            select u).ToList();*/
           
             return View(General.getUsers("Nurse"));
            // return View(Nurses);
        }
        [Authorize(Roles ="Admin")]
        public ActionResult AddNurse()
        {

            return RedirectToAction("Register", "Account", new { Role = "Nurse" });
        }
        [Authorize(Roles = "Admin")]
        public ActionResult AllPatients()
        {

            /* var role = db.Roles.FirstOrDefault(r => r.Name.Equals("Patient"));
             List<ApplicationUser> Nurses = (from u in db.Users
                                             where u.Roles.Select(x => x.RoleId).Contains(role.Id)
                                             select u).ToList();*/
            return View(General.getUsers("Patient"));

           // return View(Nurses);
        }
        [Authorize(Roles = "Admin")]
        public ActionResult AddPatient()
        {

            return RedirectToAction("Register", "Account", new { Role = "Patient" });
        }
        [Authorize(Roles = "Admin")]
        public ActionResult AllUsers()
        {
            var Users = db.Users.Include(a => a.Clinic);
            return View(Users.ToList());
        }

        // GET: Admin/Delete/5
        [Authorize(Roles = "Admin")]
        public ActionResult Delete(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ApplicationUser applicationUser = db.Users.Find(id);
            if (applicationUser == null)
            {
                return HttpNotFound();
            }
            return View(applicationUser);
        }

        // POST: Admin/Delete/5
        [Authorize(Roles = "Admin")]
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(string id)
        {
            ApplicationUser applicationUser = db.Users.Find(id);
            db.Users.Remove(applicationUser);
            db.SaveChanges();
            return RedirectToAction("AllUsers");
        }



        [Authorize]
        public ActionResult Edit(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ApplicationUser applicationUser = db.Users.Find(id);
            if (applicationUser == null)
            {
                return HttpNotFound();
            }
            ViewBag.Clinic_Id = new SelectList(db.Clinics, "Id", "Name", applicationUser.Clinic_Id);
            return View(applicationUser);
        }

        // POST: Users/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,FirstName,LastName,DateOfBirth,Gender,Clinic_Id,Email,PhoneNumber,UserName")] ApplicationUser applicationUser)
        {
            if (ModelState.IsValid)
            {
                //db.Entry(applicationUser).State = EntityState.Modified;

                ApplicationUser myUser = db.Users.FirstOrDefault(u => u.Id == applicationUser.Id);
                myUser.UserName = applicationUser.UserName;
                myUser.Email = applicationUser.Email;
                myUser.DateOfBirth = applicationUser.DateOfBirth;
                myUser.Clinic_Id = applicationUser.Clinic_Id;
                myUser.FirstName = applicationUser.FirstName;
                myUser.LastName = applicationUser.LastName;
                myUser.PhoneNumber = applicationUser.PhoneNumber;
                myUser.Gender = applicationUser.Gender;
                db.SaveChanges();
                return RedirectToAction("Edit");
            }
            ViewBag.Clinic_Id = new SelectList(db.Clinics, "Id", "Name", applicationUser.Clinic_Id);
            return View(applicationUser);
        }



    }



}