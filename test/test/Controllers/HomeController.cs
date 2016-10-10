using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Web;
using System.Web.Mvc;
using test.Models;

namespace test.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Ranking()
        {
            using (ApplicationDbContext db = new ApplicationDbContext())
            {
                var user = db.Users.ToList().OrderByDescending(u => u.TrainingTotal);
                ViewBag.Message = "Your application description page.";
                return View(user);
            }
        }

        public ActionResult Training()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Training(RegisterViewModel updatetraining)
        {
            var sess = (string)Session["UserName"];
            using (ApplicationDbContext db = new ApplicationDbContext())
            {
                var user = db.Users.SingleOrDefault(u => u.UserName == sess);
                user.TrainingTotal = user.TrainingTotal + updatetraining.TrainingTotal;
                db.SaveChanges();
            }
            return RedirectToAction("Avatar");
        }

        public ActionResult Gains()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Gains(RegisterViewModel weightloss)
        {
            var sess = (string)Session["UserName"];
            using (ApplicationDbContext db = new ApplicationDbContext())
            {
                var user = db.Users.SingleOrDefault(u => u.UserName == sess);
                user.CurrentWeight = weightloss.CurrentWeight;
                db.SaveChanges();
            }
            return RedirectToAction("Avatar");
        }

        public ActionResult Avatar()
        {
            using (ApplicationDbContext db = new ApplicationDbContext())
            {
                var sess = (string)Session["UserName"];
                var user = db.Users.SingleOrDefault(u => u.UserName == sess);
                ViewBag.Message = "Your application description page.";
                return View(user);
            }
        }
    }
}