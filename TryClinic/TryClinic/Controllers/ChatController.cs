using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TryClinic.Models;

namespace TryClinic.Controllers
{
    public class ChatController : Controller
    {
        ApplicationDbContext db = new ApplicationDbContext();
        // GET: Chat
        public ActionResult Chat()
        {

          
            return View(db.Users.ToList());
        }
    }
}