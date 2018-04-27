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
    [Authorize(Roles ="Nurse")]
    public class NurseController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Requests
        public ActionResult AllRequests()
        {
            var reqs = (from r in db.Requests
                        join u in db.Users
                        on r.DoctorID equals u.Id
                        join u2 in db.Users
                        on r.PatientID equals u2.Id
                        join u3 in db.Users
                        on r.NurseID equals u3.Id 
                        select r).ToList();
                       ;



            var requests = db.Requests;//.Include(r => r.Doctor).Include(r => r.Nurse).Include(r => r.Patient);
            return View(requests.ToList());
        }

        // GET: Nurse/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Request request = db.Requests.Find(id);
            if (request == null)
            {
                return HttpNotFound();
            }
            return View(request);
        }

        // GET: Nurse/Create
        public ActionResult Create()
        {
            ViewBag.DoctorID = new SelectList(General.getUsers("Doctor"), "Id", "UserName");
            ViewBag.NurseID = new SelectList(General.getUsers("Nurse"), "Id", "UserName");
            ViewBag.PatientID = new SelectList(General.getUsers("Patient"), "Id", "UserName");
            return View();
        }

        // POST: Nurse/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Name,Date,NurseID,DoctorID,PatientID")] Request request)
        {
            if (ModelState.IsValid)
            {
                db.Requests.Add(request);
                db.SaveChanges();
                return RedirectToAction("AllRequests");
            }

            ViewBag.Doctors = new SelectList(General.getUsers("Doctor"), "Id", "UserName");
            ViewBag.Nurses = new SelectList(General.getUsers("Nurse"), "Id", "UserName");
            ViewBag.Patients = new SelectList(General.getUsers("Patient"), "Id", "UserName");
            return View(request);
        }

        // GET: Nurse/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Request request = db.Requests.Find(id);
            if (request == null)
            {
                return HttpNotFound();
            }
            ViewBag.Doctors = new SelectList(General.getUsers("Doctor"), "Id", "UserName");
            ViewBag.Nurses = new SelectList(General.getUsers("Nurse"), "Id", "UserName");
            ViewBag.Patients = new SelectList(General.getUsers("Patient"), "Id", "UserName");
            return View(request);
        }

        // POST: Nurse/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Name,Date,NurseID,DoctorID,PatientID")] Request request)
        {
            if (ModelState.IsValid)
            {
                db.Entry(request).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("AllRequests");
            }
            ViewBag.DoctorID = new SelectList(General.getUsers("Doctor"), "Id", "UserName");
            ViewBag.NurseID = new SelectList(General.getUsers("Nurse"), "Id", "UserName");
            ViewBag.PatientID = new SelectList(General.getUsers("Patient"), "Id", "UserName");
            return View(request);
        }

        // GET: Nurse/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Request request = db.Requests.Find(id);
            if (request == null)
            {
                return HttpNotFound();
            }
            return View(request);
        }

        // POST: Nurse/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Request request = db.Requests.Find(id);
            db.Requests.Remove(request);
            db.SaveChanges();
            return RedirectToAction("AllRequests");
        }


    }
}