using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TryClinic.Models;

namespace TryClinic.Controllers
{
    public class HomeController : Controller
    {
        ApplicationDbContext db = new ApplicationDbContext();
        public ActionResult Index()
        {
            ViewBag.OurDoctors = General.getUsers("Doctor");
            ViewBag.OurNurses = General.getUsers("Nurse");

            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }

        public PartialViewResult ContactForm(ContactUsForm ContactUsForm)
        {

            if (ModelState.IsValid)
            {

                string message=Request.Form.Get("Message");
                ContactUsForm.MyDate = DateTime.Now;
                ContactUsForm.Status = Status.UnSeen;
                db.ContactUsForms.Add(ContactUsForm);
                db.SaveChanges();
            }
            else
            {

                return PartialView("ContactUsForm");
            }

            return PartialView("DisplayContactMessage");

        }
    }
}