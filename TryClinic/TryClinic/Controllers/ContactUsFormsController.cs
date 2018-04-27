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
    public class ContactUsFormsController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: ContactUsForms
        public ActionResult Index()
        {
            return View(db.ContactUsForms.ToList());
        }

        // GET: ContactUsForms/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ContactUsForm contactUsForm = db.ContactUsForms.Find(id);
            if (contactUsForm == null)
            {
                return HttpNotFound();
            }
            return View(contactUsForm);
        }

        // GET: ContactUsForms/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: ContactUsForms/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Name,MyDate,Message,Email,Status")] ContactUsForm contactUsForm)
        {
            if (ModelState.IsValid)
            {
                db.ContactUsForms.Add(contactUsForm);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(contactUsForm);
        }

        // GET: ContactUsForms/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ContactUsForm contactUsForm = db.ContactUsForms.Find(id);
            if (contactUsForm == null)
            {
                return HttpNotFound();
            }
            return View(contactUsForm);
        }

        // POST: ContactUsForms/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Name,MyDate,Message,Email,Status")] ContactUsForm contactUsForm)
        {
            if (ModelState.IsValid)
            {
                db.Entry(contactUsForm).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(contactUsForm);
        }

        // GET: ContactUsForms/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ContactUsForm contactUsForm = db.ContactUsForms.Find(id);
            if (contactUsForm == null)
            {
                return HttpNotFound();
            }
            return View(contactUsForm);
        }

        // POST: ContactUsForms/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            ContactUsForm contactUsForm = db.ContactUsForms.Find(id);
            db.ContactUsForms.Remove(contactUsForm);
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
