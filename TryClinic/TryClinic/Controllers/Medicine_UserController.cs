using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using TryClinic.Models;

namespace TryClinic.Controllers
{
    [Authorize(Roles = "Doctor")]

    public class Medicine_UserController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Medicine_User
        public ActionResult Index()
        {
            var medicine_User = db.Medicine_Users.Include(m => m.Doctor).Include(m => m.Medicine).Include(m => m.Patient);
            return View(medicine_User.ToList());
        }

        // GET: Medicine_User/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Medicine_User medicine_User = db.Medicine_Users.SingleOrDefault(m=>m.Id==id);
            if (medicine_User == null)
            {
                return HttpNotFound();
            }
            return View(medicine_User);
        }

        // GET: Medicine_User/Create
        public ActionResult Create()
        {
           
            ViewBag.DoctorID = new SelectList(General.getUsers("Doctor"), "Id", "UserName");
            ViewBag.MedicineId = new SelectList(db.Medicines, "Id", "Name");
            ViewBag.PatientID = new SelectList(General.getUsers("Patient"), "Id", "UserName");
            return View();
        }

        // POST: Medicine_User/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "PatientId,DoctorId,MedicineId,Quantity")] Medicine_User medicine_User)
        {
            if (ModelState.IsValid)
            {
                db.Medicine_Users.Add(medicine_User);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.DoctorId = new SelectList(db.Users, "Id", "FirstName", medicine_User.DoctorId);
            ViewBag.MedicineId = new SelectList(db.Medicines, "Id", "Name", medicine_User.MedicineId);
            ViewBag.PatientId = new SelectList(db.Users, "Id", "FirstName", medicine_User.PatientId);
            return View(medicine_User);
        }

        // GET: Medicine_User/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Medicine_User medicine_User = db.Medicine_Users.SingleOrDefault(m => m.Id == id);
            if (medicine_User == null)
            {
                return HttpNotFound();
            }
            ViewBag.Doctors = new SelectList(General.getUsers("Doctor"), "Id", "UserName");
            ViewBag.Medicines = new SelectList(db.Medicines, "Id", "Name");
            ViewBag.Patients = new SelectList(General.getUsers("Patient"), "Id", "UserName");
            return View(medicine_User);
        }

        // POST: Medicine_User/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,PatientId,DoctorId,MedicineId,Quantity")] Medicine_User medicine_User)
        {
            if (ModelState.IsValid)
            {
                /*Medicine_User medicine_User2 = db.Medicine_Users.SingleOrDefault(m => m.Id == medicine_User.Id);
                medicine_User2.MedicineId = medicine_User.MedicineId;
                medicine_User2.PatientId = medicine_User.PatientId;
                medicine_User2.DoctorId = medicine_User.DoctorId;
                medicine_User2.Quantity = medicine_User.Quantity;
                */
                db.Entry(medicine_User).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.Doctors = new SelectList(General.getUsers("Doctor"), "Id", "UserName");
            ViewBag.Medicines = new SelectList(db.Medicines, "Id", "Name");
            ViewBag.Patients = new SelectList(General.getUsers("Patient"), "Id", "UserName");
            return View(medicine_User);
        }

        // GET: Medicine_User/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Medicine_User medicine_User = db.Medicine_Users.SingleOrDefault(m => m.Id == id);
            if (medicine_User == null)
            {
                return HttpNotFound();
            }
            return View(medicine_User);
        }

        // POST: Medicine_User/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
           
            Medicine_User medicine_User = db.Medicine_Users.Find(id);
            db.Medicine_Users.Remove(medicine_User);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
