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
    //[Authorize(Roles = "Patient")]

    public class PatientController : Controller
    {
        ApplicationDbContext db = new ApplicationDbContext();
        // GET: Patient
        public ActionResult MyRequests()
        {

            var UserRequests = (from r in db.Requests
                    join u in db.Users
                    on r.PatientID equals u.Id
                    where r.Patient.UserName==User.Identity.Name
                    select r).ToList();


            return View(UserRequests);
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

        // POST: Patient/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Request request = db.Requests.Find(id);
            db.Requests.Remove(request);
            db.SaveChanges();
            return RedirectToAction("MyRequests");
        }

        public ActionResult MakeAppointment()
        {

            return View();
        }
    }
}